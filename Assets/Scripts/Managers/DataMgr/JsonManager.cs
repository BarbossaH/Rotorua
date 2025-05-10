namespace Managers
{
    using System;
    using System.Collections.Generic;
    using Common;
    using Cysharp.Threading.Tasks;
    using Models;
    using Newtonsoft.Json;
    using UnityEngine;

    [AutoRegisterManager]
    public class JsonManager : Singleton<JsonManager>
    {
        private readonly Dictionary<string, IGameData> _loadData = new();
        private readonly UniTaskCompletionSource _jsonLoadCompletion = new();
        private bool _isJsonLoaded = false;
        private bool _isLoading = false;

        public async UniTask LoadJsonFile()
        {
            if (_isJsonLoaded || _isLoading)
            {
                await _jsonLoadCompletion.Task;
                return;
            }

            _isLoading = true;
            await LoadAllJsonByLabelAsync("json_data");
            _isJsonLoaded = true;
            _jsonLoadCompletion.TrySetResult();
        }

        public async UniTask WaitForJsonLoad()
        {
            await _jsonLoadCompletion.Task;
        }

        public async UniTask<T> LoadJsonAsync<T>(string address)
        {
            TextAsset jsonText = await ResourceManager.Instance.LoadAssetByAddressAsync<TextAsset>(address);
            if (jsonText == null) return default;

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };

            return JsonConvert.DeserializeObject<T>(jsonText.text, settings);
        }

        private async UniTask LoadAllJsonByLabelAsync(string label)
        {
            var jsonFiles = await ResourceManager.Instance.LoadAssetsByLabelAsync<TextAsset>(label);

            foreach (var json in jsonFiles)
            {
                string className = json.name;
                Type type = GameTypeCache.GetTypeByName(className);
                if (type != null)
                {
                    Debug.Log(type.FullName);
                    var data = JsonConvert.DeserializeObject(json.text, type) as IGameData;
                    _loadData[className] = data;
                    Debug.Log(className + " has been loaded.");
                }
                else
                {
                    Debug.LogError($"No matching class found for {json.name}");
                }
            }
        }

        public T GetData<T>(string key) where T : class, IGameData
        {
            if (_loadData.TryGetValue(key, out var data))
            {
                return data as T;
            }
            return null;
        }
    }
}
