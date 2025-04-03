

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

/// <summary>
/// this class is a kind of model, it represents or corresponds to data read from a JSON configuration file
/// </summary>
/// 
[Serializable]
public class SkillDatabase : IGameData
{
    public SkillData[] Skills;
    public string GetKey()
    {
        return typeof(SkillDatabase).Name;
    }
}

[Serializable]
public class SkillData
{
    public string ID;
    public string Name;
    public string SkillType;
    public string Description;
    public SkillLevel[] LevelsData;
    public bool Stackable;

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
        // UtilTools.ValidateString(SkillType, nameof(SkillType), Name);
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


[Serializable]
public class SkillLevel
{
    public int Level;
    public int CoolDown;
    public CostData[] Cost;
    public EffectData[] EffectsData;

#if UNITY_EDITOR
    public bool Validate(string skillName)
    {
        if (!UtilTools.ValidatePositiveValue(Level, nameof(Level), skillName)) return false;
        if (!UtilTools.ValidatePositiveValue(CoolDown, nameof(CoolDown), skillName)) return false;

        #region if there is an cost array, it should check the value is more than 0
        // if (UtilTools.ValidateArray(Cost, nameof(Cost), skillName))
        // {
        //     for (int i = 0; i < Cost.Length; i++)
        //     {
        //         if (!Cost[i].Validate(skillName))
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

        if (UtilTools.ValidateArray(EffectsData, nameof(EffectsData), skillName))
        {
            for (int i = 0; i < EffectsData.Length; i++)
            {
                if (!EffectsData[i].Validate(skillName))
                {
                    return false;
                }
            }
        }
        else
        {
            return false;
        }
        return true;
    }
#endif
}

[Serializable]
public class CostData
{
    public string Type;
    public int Value;

    public CostTypeEnum GetCostType() => Enum.TryParse(Type, out CostTypeEnum result) ? result : CostTypeEnum.Mana;

    public bool Validate(string skillName)
    {
        if (!UtilTools.ValidatePositiveValue(Value, nameof(Value), skillName))
        {
            return false;
        }
        if (!UtilTools.ValidateEnum(Type, out CostTypeEnum result, nameof(Type), skillName))
        {
            return false;
        }
        return true;
    }
}

[Serializable]
public class EffectData
{
    public string EffectType;
    public string TriggerCondition;
    public string Target;
    public EffectParameterDataBase ParametersData;

    public EffectTypeEnum GetEffectType() =>
    Enum.TryParse(EffectType, out EffectTypeEnum result) ? result : EffectTypeEnum.None;
    public TriggerConditionEnum GetTriggerConditionEnum() =>
    Enum.TryParse(TriggerCondition, out TriggerConditionEnum result) ? result : TriggerConditionEnum.None;
    public TargetTypeEnum GetTargetEnum() =>
    Enum.TryParse(Target, out TargetTypeEnum result) ? result : TargetTypeEnum.None;

    public bool Validate(string ownerName)
    {
        if (!UtilTools.ValidateEnum(EffectType, out EffectTypeEnum resultEffect, nameof(EffectType), ownerName)) return false;
        if (!UtilTools.ValidateEnum(TriggerCondition, out TriggerConditionEnum resultTrigger, nameof(TriggerCondition), ownerName)) return false;
        if (!UtilTools.ValidateEnum(Target, out TargetTypeEnum resultTarget, nameof(Target), ownerName)) return false;
        if (UtilTools.ValidateObject(ParametersData, nameof(ParametersData), ownerName))
        {
            if (!ParametersData.Validate(ownerName)) return false;
        }
        else
        {
            return false;
        }

        return true;
    }

}

#region Skill Effect Parameter Classes

[JsonConverter(typeof(EffectParameterConverter))]
public abstract class EffectParameterDataBase
{
    public string SubclassName;
    public abstract bool Validate(string ownerName);
}

public class DamageParameterData : EffectParameterDataBase
{
    public float Damage;
    public float CriticalChance = 0;
    public string Element;

    public ElementEnum GetElementEnum() => Enum.TryParse(Element, out ElementEnum result) ? result : ElementEnum.None;

    public override bool Validate(string ownerName)
    {
        if (!UtilTools.ValidatePositiveValue(Damage, nameof(Damage), ownerName)) return false;
        if (!UtilTools.ValidateEnum(Element, out ElementEnum result, nameof(Damage), ownerName)) return false;

        return true;
    }
}

public class HealParameterData : EffectParameterDataBase
{
    public float HealAmount;
    public int Duration;

    public override bool Validate(string ownerName)
    {
        if (!UtilTools.ValidatePositiveValue(HealAmount, nameof(HealAmount), ownerName)) return false;
        if (!UtilTools.ValidatePositiveValue(Duration, nameof(Duration), ownerName)) return false;
        return true;
    }
}

public class MultipleDamageParameterData : EffectParameterDataBase
{
    public float Multiplier;

    public override bool Validate(string ownerName)
    {
        if (!UtilTools.ValidatePositiveValue(Multiplier, nameof(Multiplier), ownerName)) return false;
        return true;
    }
}

public class MultipleDefenseParameterData : EffectParameterDataBase
{
    public float Multiplier;

    public override bool Validate(string ownerName)
    {
        if (!UtilTools.ValidatePositiveValue(Multiplier, nameof(Multiplier), ownerName)) return false;
        return true;
    }
}

public class ShieldParameterData : EffectParameterDataBase
{
    public float ShieldAmount;
    public int Duration;

    public override bool Validate(string ownerName)
    {
        if (!UtilTools.ValidatePositiveValue(ShieldAmount, nameof(ShieldAmount), ownerName)) return false;
        if (!UtilTools.ValidatePositiveValue(Duration, nameof(Duration), ownerName)) return false;
        return true;
    }
}

public class SpeedReductionParameterData : EffectParameterDataBase
{
    public float Reduction;
    public int Duration;
    public float Chance = 100;

    public override bool Validate(string ownerName)
    {
        if (!UtilTools.ValidatePositiveValue(Reduction, nameof(Reduction), ownerName)) return false;
        if (!UtilTools.ValidatePositiveValue(Duration, nameof(Duration), ownerName)) return false;
        if (!UtilTools.ValidatePositiveValue(Chance, nameof(Chance), ownerName)) return false;
        return true;
    }
}

public class FrozenParameterData : EffectParameterDataBase
{
    public int Duration;
    public float Chance = 100;

    public override bool Validate(string ownerName)
    {
        if (!UtilTools.ValidatePositiveValue(Duration, nameof(Duration), ownerName)) return false;
        if (!UtilTools.ValidatePositiveValue(Chance, nameof(Chance), ownerName)) return false;
        return true;
    }
}


public class DrawCardParameterData : EffectParameterDataBase
{
    public int DrawAmount;

    public override bool Validate(string ownerName)
    {
        if (!UtilTools.ValidatePositiveValue(DrawAmount, nameof(DrawAmount), ownerName)) return false;
        return true;
    }
}

public class LifeStealParameterData : EffectParameterDataBase
{
    public int LifeSteal;

    public override bool Validate(string ownerName)
    {
        if (!UtilTools.ValidatePositiveValue(LifeSteal, nameof(LifeSteal), ownerName)) return false;
        return true;
    }
}

#endregion

#region newton soft json convert
public class EffectParameterConverter : JsonConverter
{
    private static readonly Dictionary<string, Type> EffectTypeMap = new();

    static EffectParameterConverter()
    {
        // 获取所有 EffectParameterBase 的子类，并注册它们的类型映射
        var effectBaseType = typeof(EffectParameterDataBase);
        var subclasses = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => effectBaseType.IsAssignableFrom(t) && !t.IsAbstract);

        foreach (var subclass in subclasses)
        {
            EffectTypeMap[subclass.Name] = subclass; // using the name of the class as the key
        }
    }
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(EffectParameterDataBase);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        JObject jsonObject = JObject.Load(reader);
        string typeName = jsonObject["SubclassName"]?.ToString();

        if (string.IsNullOrEmpty(typeName))
        {
            throw new Exception("Missing or invalid SubclassName field in JSON.");
        }

        Type effectType = Type.GetType(typeName);
        if (effectType == null || !typeof(EffectParameterDataBase).IsAssignableFrom(effectType))
        {
            throw new Exception($"Unknown or invalid effect type: {typeName}");
        }
        Debug.Log(typeName);
        var effect = Activator.CreateInstance(effectType);
        serializer.Populate(jsonObject.CreateReader(), effect);
        return effect;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        throw new NotSupportedException("Skill data should not be serialized back to JSON.");
        //because skill json don't need to be changed,so there is no need to write into the json file.
        // JObject jsonObject = JObject.FromObject(value);
        // jsonObject["SubclassName"] = value.GetType().Name;
        // jsonObject.WriteTo(writer);
    }
}

#endregion