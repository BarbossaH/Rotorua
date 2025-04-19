namespace Managers
{
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
        public async UniTask SaveData<T>(T data, string fileName) where T : class
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
                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto // 读取时解析多态
                };

                T data = JsonConvert.DeserializeObject<T>(json, settings);
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
}