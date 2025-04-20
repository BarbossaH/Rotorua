//this class is for retrieving effect data and other methods to get specific data
//the data is stored in the IocContainer
using System.Collections.Generic;
using Common;
using Cysharp.Threading.Tasks;
using Managers;
using Models;
using UnityEngine;

namespace Game.SkillSystem
{
    [AutoRegisterRepository(typeof(ISkillEffectRepository))]
    public class SkillEffectRepository : ISkillEffectRepository
    {
        private readonly Dictionary<string, EffectDataBase> _effectDataDict = new();
        private List<EffectDataBase> _effectDataList = new();
        public bool IsLoaded { get; private set; }

        public List<EffectDataBase> GetAll()
        {
            return _effectDataList;
        }

        public EffectDataBase GetById(string id)
        {
            return _effectDataDict.TryGetValue(id, out var effectData) ? effectData : null;
        }

        public string GetNameById(string id)
        {
            if (_effectDataDict.TryGetValue(id, out var effectData))
            {
                return effectData.Name;
            }
            Debug.LogError("Effect Name not exists");
            return null;
        }

        public async UniTask LoadDataAsync()
        {
            if (IsLoaded) return;
            await JsonManager.Instance.WaitForJsonLoad(); // 确保 JSON 数据已加载
            _effectDataList = JsonManager.Instance.GetData<EffectJsonConfig>(typeof(EffectJsonConfig).Name)?.Effects;

            if (_effectDataList == null)
            {
                Debug.LogError("技能数据解析失败: skillDataArray 为空！");
                return;
            }
            if (_effectDataList.Count == 0)
            {
                Debug.LogError("技能数据为空，没有任何技能！");
                return;
            }

            // Debug.Log(effectDataBases.Count);

            foreach (var e in _effectDataList)
            {
                if (e == null)
                {
                    Debug.LogError("发现空技能对象，跳过！");
                    continue;
                }
                if (string.IsNullOrEmpty(e.ID))
                {
                    Debug.LogError($"技能 {e.Name} 的 ID 为空，跳过！");
                    continue;
                }

                Debug.Log($"加载技能Effect: {e.ID} - {e.Name}");
                _effectDataDict[e.ID] = e;
            }
            IsLoaded = true;
        }


    }
}