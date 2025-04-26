namespace Common
{
    using System.Collections.Generic;
    using System.Linq;
    using Common;
    using Cysharp.Threading.Tasks;
    using Managers;
    using Models;
    using UnityEngine;

    [AutoRegisterRepository(typeof(ISkillRepository))]
    public class SkillDataRepository : ISkillRepository
    {
        private readonly Dictionary<string, SkillData> _skillDataDict = new();
        private List<SkillData> _skillDataList = new();
        // public bool IsSkillDataLoaded { get; private set; }

        public bool IsLoaded { get; private set; }

        // public SkillData GetSkillDataById(string id)
        // {
        //     return _SkillDataDict.TryGetValue(id, out var skillData) ? skillData : null;
        // }

        public string GetNameById(string id)
        {
            if (_skillDataDict.TryGetValue(id, out var skillData))
            {
                return skillData.Name;
            }

            Debug.LogError("Skill data not exists");
            return null;

        }

        public SkillLevel[] GetSkillLevels(string id)
        {
            SkillData skill = GetById(id);
            if (skill == null)
            {
                return null;
            }
            SkillLevel[] skillLevels = skill.LevelsData;
            if (skillLevels != null && skillLevels.Length > 0)
            {
                return skillLevels;
            }

            Debug.LogError("Skill level data  not exists");
            return null;

        }
        public string[] GetSkillEffectIDs(string id, int level)
        {
            SkillLevel[] skillLevels = GetSkillLevels(id);
            if (skillLevels != null)
            {
                string[] effectIDs = skillLevels[level].EffectIDs;
                if (effectIDs != null && effectIDs.Length > 0)
                {
                    return effectIDs;
                }
            }
            return null;
        }
        public string[] GetSkillBuffIDs(string id, int level)
        {
            SkillLevel[] skillLevels = GetSkillLevels(id);
            if (skillLevels != null)
            {
                string[] buffIDs = skillLevels[level].BuffIDs;
                if (buffIDs != null && buffIDs.Length > 0)
                {
                    return buffIDs;
                }
            }
            return null;
        }

        public async UniTask LoadDataAsync()
        {
            if (IsLoaded) return;

            await JsonManager.Instance.WaitForJsonLoad(); // 确保 JSON 数据已加载

            _skillDataList = JsonManager.Instance.GetData<SkillJsonConfig>(typeof(SkillJsonConfig).Name)?.Skills;

            if (_skillDataList == null)
            {
                Debug.LogError("技能数据解析失败: skillDataArray 为空！");
                return;
            }
            if (_skillDataList.Count == 0)
            {
                Debug.LogError("技能数据为空，没有任何技能！");
                return;
            }

            foreach (var s in _skillDataList)
            {
                if (s == null)
                {
                    Debug.LogError("发现空技能对象，跳过！");
                    continue;
                }
                if (string.IsNullOrEmpty(s.ID))
                {
                    Debug.LogError($"技能 {s.Name} 的 ID 为空，跳过！");
                    continue;
                }

                // Debug.Log($"加载技能: {s.ID} - {s.Name}");
                _skillDataDict[s.ID] = s;

                if (s.LevelsData == null || s.LevelsData.Length == 0)
                {
                    Debug.LogError($"技能 {s.ID} ({s.Name}) 的 LevelsData 为空，可能数据有误！");
                }
            }

            Debug.Log("SkillData has been loaded");
            IsLoaded = true;
        }

        public SkillData GetById(string id)
        {
            return _skillDataDict.TryGetValue(id, out var skillData) ? skillData : null;
        }

        public List<SkillData> GetAll()
        {
            return _skillDataList;
        }
    }
}