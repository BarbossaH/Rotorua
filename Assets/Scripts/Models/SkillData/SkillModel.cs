namespace Models
{

    using System;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// this class is a kind of model, it represents or corresponds to data read from a JSON configuration file
    /// </summary>
    /// 
    // [Serializable]
    public class SkillJsonConfig : IGameData
    {
        public List<SkillData> Skills;
        public string GetKey()
        {
            return typeof(SkillJsonConfig).Name;
        }
    }

    // [Serializable]
    public class SkillData
    {
        public string ID;
        public string Name;
        public string SkillType;
        public string Description;
        public string Target;
        public int TargetAmount;
        // public int Cost; //we don't need cost because skill does not care about cost, cards should care about cost
        public SkillLevel[] LevelsData;

        public TargetTypeEnum GetTargetEnum() =>
           Enum.TryParse(Target, out TargetTypeEnum result) ? result : TargetTypeEnum.None;

        public SkillTypeEnum GetSkillTypeEnum() =>
        Enum.TryParse(SkillType, true, out SkillTypeEnum result) ? result : SkillTypeEnum.None;


#if UNITY_EDITOR
        public bool Validate()
        {
            if (string.IsNullOrEmpty(ID))
            {
                Debug.LogError("Id is not valid");
            }
            UtilTools.ValidateString(ID, nameof(ID), Name);
            UtilTools.ValidateString(Name, nameof(Name), Name);
            if (!UtilTools.ValidateEnum(Target, out TargetTypeEnum resultTarget, nameof(Target), Name)) return false;

            UtilTools.ValidateString(Description, nameof(Description), Name);

            if (UtilTools.ValidateEnum(SkillType, out SkillTypeEnum result, nameof(SkillType), Name))
            {
                Debug.Log($"Skill {Name} has valid skill type: {SkillType}");
            }

            if (UtilTools.ValidateArray(LevelsData, "SkillLevels", Name))
            {
                for (int i = 0; i < LevelsData.Length; i++)
                {
                    LevelsData[i].Validate(Name);
                }
            }
            return true;
        }
#endif
    }


    // [Serializable]
    public class SkillLevel
    {
        public int Level;
        public int CoolDown;
        public string[] EffectIDs;
        public string[] BuffIDs;

#if UNITY_EDITOR
        public bool Validate(string skillName)
        {
            if (!UtilTools.ValidatePositiveValue(Level, nameof(Level), skillName)) return false;
            if (!UtilTools.ValidatePositiveValue(CoolDown, nameof(CoolDown), skillName)) return false;

            #region if there is an cost array, it should check the value is more than 0

            // if (UtilTools.ValidateArray(EffectIDs, nameof(EffectIDs), skillName))
            // {
            //     foreach (var id in EffectIDs)
            //     {
            //         if (!UtilTools.ValidateString(id, nameof(id), skillName))
            //         {
            //             return false;
            //         }
            //     }
            // }
            // else
            // {
            //     return false;
            // }

            // if (UtilTools.ValidateArray(BuffIDs, nameof(BuffIDs), skillName))
            // {
            //     foreach (var id in BuffIDs)
            //     {
            //         if (!UtilTools.ValidateString(id, nameof(id), skillName))
            //         {
            //             return false;
            //         }
            //     }
            // }
            // else
            // {
            //     return false;
            // }
            #endregion

            return true;
        }
#endif
    }

}