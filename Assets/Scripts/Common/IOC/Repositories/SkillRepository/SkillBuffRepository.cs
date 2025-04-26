using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Managers;
using Models;
using UnityEngine;

namespace Common
{
    public class SkillBuffRepository : ISkillBuffRepository
    {
        private List<BuffDataBase> _buffDataList = new();
        private readonly Dictionary<string /*buff ID*/, BuffDataBase> _buffDataDict = new();
        public bool IsLoaded { get; private set; }

        public List<BuffDataBase> GetAll()
        {
            return _buffDataList;
        }

        public BuffDataBase GetById(string id)
        {
            return _buffDataDict.TryGetValue(id, out var buffData) ? buffData : null;
        }

        public string GetNameById(string id)
        {
            BuffDataBase buffData = GetById(id);
            if (buffData != null)
            {
                return buffData.Name;
            }
            Debug.LogError("Buff Name not exists");
            return null;
        }

        public async UniTask LoadDataAsync()
        {
            if (IsLoaded) return;
            await JsonManager.Instance.WaitForJsonLoad();
            _buffDataList = JsonManager.Instance.GetData<BuffJsonConfig>(typeof(BuffJsonConfig).Name)?.Buffs;
            if (_buffDataList == null)
            {
                Debug.LogError("Skill data parsing failed: buff data list is empty!");
                return;
            }

            if (_buffDataList.Count == 0)
            {
                Debug.LogError("Buff data list is empty!");
                return;
            }
            foreach (var b in _buffDataList)
            {
                if (b == null)
                {
                    Debug.LogError("Find empty buff, skip");
                    continue;
                }
                if (string.IsNullOrEmpty(b.ID))
                {
                    Debug.LogError($"buff {b.Name}  ID is empty, Skip.");
                    continue;
                }

                Debug.Log($"Loaded buff: {b.ID} - {b.Name}");
                _buffDataDict[b.ID] = b;
            }
            IsLoaded = true;
        }
    }
}