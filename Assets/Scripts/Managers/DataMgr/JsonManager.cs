
using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// This class is responsible for reading the json files that contain the static data,like skill, quest and map
/// </summary>
public class JsonManager : Singleton<JsonManager>
{
    private readonly Dictionary<string, IGameData> loadData = new();
    public async UniTask<T> LoadJsonAsync<T>(string address)
    {
        TextAsset jsonText = await ResourceManager.Instance.LoadAssetByAddressAsync<TextAsset>(address);
        if (jsonText == null) return default;
        return JsonUtility.FromJson<T>(jsonText.text);
    }

    public async UniTask LoadAllJsonByLabelAsync(string label)
    {
        var jsonFiles = await ResourceManager.Instance.LoadAssetsByLabelAsync<TextAsset>(label);

        // there is a rule that the json filename must match the name of the corresponding class because the json filename need to be converted into class name by reflection。
        foreach (var json in jsonFiles)
        {
            string className = json.name;
            Type type = Type.GetType(className);
            if (type != null)
            {
                var data = JsonUtility.FromJson(json.text, type) as IGameData;
                loadData[className] = data;
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
