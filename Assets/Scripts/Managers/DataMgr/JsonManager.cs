namespace Managers
{

    using System;
    using System.Collections.Generic;
    using Common;
    using Cysharp.Threading.Tasks;
    using Models;
    using Newtonsoft.Json;
    using UnityEngine;

    /// <summary>
    /// This class is responsible for reading the json files that contain the static data,like skill, quest and map
    /// </summary>
    /// 

    [AutoRegisterManager]
    public class JsonManager : Singleton<JsonManager>
    {
        private readonly Dictionary<string, IGameData> loadData = new();
        private UniTaskCompletionSource jsonLoadCompletion = new UniTaskCompletionSource();


        public async UniTask LoadJsonFile()
        {
            //数据反序列化
            await LoadAllJsonByLabelAsync("json_data");
            jsonLoadCompletion.TrySetResult(); // 标记 JSON 加载完成

        }

        public async UniTask WaitForJsonLoad()
        {
            await jsonLoadCompletion.Task;
        }
        public async UniTask<T> LoadJsonAsync<T>(string address)
        {
            TextAsset jsonText = await ResourceManager.Instance.LoadAssetByAddressAsync<TextAsset>(address);
            if (jsonText == null) return default;
            // return JsonUtility.FromJson<T>(jsonText.text);
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };

            return JsonConvert.DeserializeObject<T>(jsonText.text, settings);
        }

        private async UniTask LoadAllJsonByLabelAsync(string label)
        {
            var jsonFiles = await ResourceManager.Instance.LoadAssetsByLabelAsync<TextAsset>(label);

            // there is a rule that the json filename must match the name of the corresponding class because the json filename need to be converted into class name by reflection。
            foreach (var json in jsonFiles)
            {
                string className = json.name;
                Type type = GameTypeCache.GetTypeByName(className);
                if (type != null)
                {
                    Debug.Log(type.FullName);

                    // var data = JsonUtility.FromJson(json.text, type) as IGameData;
                    // var settings = new JsonSerializerSettings
                    // {
                    //     TypeNameHandling = TypeNameHandling.Auto
                    // };

                    // var data = JsonConvert.DeserializeObject(json.text, type, settings) as IGameData;
                    var data = JsonConvert.DeserializeObject(json.text, type) as IGameData;
                    loadData[className] = data;
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

            if (loadData.TryGetValue(key, out var data))
            {
                return data as T;
            }
            return null;
        }

    }

}