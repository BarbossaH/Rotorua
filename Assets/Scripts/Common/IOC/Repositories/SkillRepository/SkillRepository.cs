namespace Common
{
    using System.Collections.Generic;
    using Cysharp.Threading.Tasks;
    using Managers;
    using Models;
    using UnityEngine;


    [AutoRegisterRepository(typeof(SkillDataRepository))]
    public class SkillDataRepository : JsonRepositoryBase<SkillData, SkillJsonConfig>
    {

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

        protected override string GetName(SkillData data)
        {
            return data.Name;
        }

        protected override string GetId(SkillData data)
        {
            return data.ID;
        }

        protected override List<SkillData> ExtractList(SkillJsonConfig config)
        {
            return config.Skills;
        }
    }
}