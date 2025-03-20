using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// this class is for storing and reading some changeable data, like game setting, user save, and so on
/// </summary>
public class PersistentDataManager : Singleton<PersistentDataManager>
{
    private readonly string saveDirectory = Application.persistentDataPath;
    public async UniTask SaveData<T>(T data, string fileName) where T : class
    {
        string filePath = Path.Combine(saveDirectory, fileName);
        UtilTools.EnsureDictionary(saveDirectory);

        string json = JsonUtility.ToJson(data, true);

        await File.WriteAllTextAsync(filePath, json);

        Debug.Log("Player data saved successfully");
    }

    public async UniTask<T> LoadData<T>(string fileName) where T : new()
    {
        string filePath = Path.Combine(saveDirectory, fileName);

        if (!File.Exists(filePath))
        {
            Debug.Log("No save file found. Creating new player data.");
            return new T();
        }

        try
        {
            string json = await File.ReadAllTextAsync(filePath);
            T data = JsonUtility.FromJson<T>(json);
            Debug.Log("Player data loaded successfully");
            return data;
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error loading player data: {e.Message}");
            return new T();
        }
    }

}