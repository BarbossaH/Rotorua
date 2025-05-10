namespace Managers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Common;
    using Cysharp.Threading.Tasks;
    using Models;
    using Newtonsoft.Json;
    using UnityEngine;

    /// <summary>
    /// this class is for storing and reading some changeable data, like game setting, user save, and so on
    /// </summary>

    [AutoRegisterManager]
    public class PersistentDataManager : Singleton<PersistentDataManager>
    {
        private readonly Dictionary<string, ISaveData> _gameSaveDict = new();
        private readonly string saveDirectory = Application.persistentDataPath;

        private readonly UniTaskCompletionSource _saveDataLoadCompletion = new();
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
        public async UniTask LoadData<T>(SaveFileEnum saveFile) where T : new()
        {
            string fileName = saveFile.GetFileName();
            string filePath = Path.Combine(saveDirectory, fileName);

            if (!File.Exists(filePath))
            {
                Debug.Log("No save file found. Creating new player data.");
                // return new T();
            }

            try
            {
                string json = await File.ReadAllTextAsync(filePath);
                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    Formatting = Formatting.Indented
                };

                var data = JsonConvert.DeserializeObject<T>(json, settings) as ISaveData;

                // Debug.Log("Player data loaded successfully");
                _gameSaveDict[fileName] = data;
            }
            catch (Exception e)
            {
                Debug.LogError($"Error loading player data: {e.Message}");
            }
        }

        public async UniTask LoadAllSaveData()
        {
            try
            {
                await LoadData<GameSettings>(SaveFileEnum.Settings);
                await LoadData<PlayerDataSave>(SaveFileEnum.PlayerData);
                _saveDataLoadCompletion.TrySetResult();
            }
            catch (Exception e)
            {
                _saveDataLoadCompletion.TrySetException(e);
            }

        }

        public async UniTask WaitForSaveDataLoad()
        {
            await _saveDataLoadCompletion.Task;
        }

        public T GetSaveData<T>(SaveFileEnum fileEnum) where T : class, ISaveData
        {
            if (_gameSaveDict.TryGetValue(fileEnum.GetFileName(), out var saveData))
            {
                return saveData as T;
            }
            Debug.LogError($"Save data file {fileEnum.GetFileName()} doesn't exist");
            return null;
        }
    }
}