namespace Managers
{
    using System;
    using System.IO;
    using Common;
    using Cysharp.Threading.Tasks;
    using Newtonsoft.Json;
    using UnityEngine;

    /// <summary>
    /// this class is for storing and reading some changeable data, like game setting, user save, and so on
    /// </summary>

    [AutoRegisterManager]
    public class PersistentDataManager : Singleton<PersistentDataManager>
    {
        private readonly string saveDirectory = Application.persistentDataPath;
        public async UniTask<bool> SaveData<T>(T data, string fileName) where T : class
        {
            try
            {
                string filePath = Path.Combine(saveDirectory, fileName);
                UtilTools.EnsureDictionary(saveDirectory);

                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto, // 保存时包含多态信息
                    Formatting = Formatting.Indented
                };

                string json = JsonConvert.SerializeObject(data, settings);

                await File.WriteAllTextAsync(filePath, json);

                Debug.Log("Player data saved successfully");
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Saving Data failed: {e.Message}");
                return false;
            }
        }

        //Not using reflection to load data because the saving data structure often changes 
        // and there is too many types of data need to be stored and retrieved from the save files.
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
                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    Formatting = Formatting.Indented
                };

                T data = JsonConvert.DeserializeObject<T>(json, settings);

                // Debug.Log("Player data loaded successfully");
                return data;
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error loading player data: {e.Message}");
                return new T();
            }
        }
    }
}