using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Managers;
using Models;
using UnityEngine;

namespace Common
{
    [AutoRegisterRepository(typeof(IPlayerSaveRepository))]
    public class PlayerSaveRepository : IPlayerSaveRepository
    {
        public bool IsLoaded { get; private set; }

        private List<GameSlot> _gameSlots = new();
        private readonly Dictionary<string/*save saveId*/, GameSlot> _gameSlotDict = new();

        public List<GameSlot> GetAll()
        {
            return _gameSlots;
        }

        public GameSlot GetById(string saveId)
        {
            if (_gameSlotDict.TryGetValue(saveId, out var gameSlot))
            {
                return gameSlot;
            }

            return null;
        }

        public string GetNameById(string saveId)
        {
            if (_gameSlotDict.TryGetValue(saveId, out var gameSlot))
            {
                return gameSlot.SaveID;
            }
            Debug.LogError("There is no the save data");
            return null;
        }

        public HeroSaveData GetHeroSaveData(string saveId)
        {
            GameSlot slot = GetById(saveId);
            return slot?.HeroSaveData;
        }

        public List<RelicSaveData> GetRelicSaveData(string saveId)
        {
            GameSlot slot = GetById(saveId);
            return slot?.HeroSaveData?.RelicSaveData;
        }

        public List<CardSaveData> GetCardSaveData(string saveId)
        {
            GameSlot slot = GetById(saveId);
            return slot?.HeroSaveData?.CardInstanceData;
        }

        public HeroStatusSaveData GetHeroStatusSaveData(string saveId)
        {
            GameSlot slot = GetById(saveId);
            return slot?.HeroSaveData?.StatusSaveData;
        }
        public async UniTask LoadDataAsync()
        {
            try
            {
                if (IsLoaded) return;
                await PersistentDataManager.Instance.WaitForSaveDataLoad();
                PlayerDataSave data = PersistentDataManager.Instance.GetSaveData<PlayerDataSave>(SaveFileEnum.PlayerData);
                if (data == null || data.GameSlots == null)
                {
                    Debug.Log("No save data found or empty game slots");
                    IsLoaded = true;  // Mark as loaded even if empty
                    return;
                }

                _gameSlots = new List<GameSlot>();
                _gameSlotDict.Clear();

                foreach (var slot in data.GameSlots)
                {
                    if (slot == null)
                    {
                        Debug.LogWarning("Found empty slot data! Skipping.");
                        continue;
                    }

                    if (string.IsNullOrEmpty(slot.SaveID))
                    {
                        Debug.LogWarning("Found slot with empty ID! Skipping.");
                        continue;
                    }

                    if (_gameSlotDict.ContainsKey(slot.SaveID))
                    {
                        Debug.LogWarning($"Duplicate save ID found: {slot.SaveID}. Using the first occurrence.");
                        continue;
                    }

                    _gameSlots.Add(slot);
                    _gameSlotDict[slot.SaveID] = slot;
                    Debug.Log($"Loaded Game Slot: {slot.SaveID}");
                }

                IsLoaded = true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error loading save data: {ex.Message}");
                // 可能需要进行一些恢复操作
                IsLoaded = false;
                throw; // 或者根据需要处理异常而不是重新抛出
            }
        }
    }
}