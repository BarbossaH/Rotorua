

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
    public SkillLevel[] Levels;
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

        if (UtilTools.ValidateArray(Levels, "SkillLevels", Name))
        {
            for (int i = 0; i < Levels.Length; i++)
            {
                Levels[i].Validate(Name);
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
    public EffectData[] Effects;

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

        if (UtilTools.ValidateArray(Effects, nameof(Effects), skillName))
        {
            for (int i = 0; i < Effects.Length; i++)
            {
                if (!Effects[i].Validate(skillName))
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
    public EffectParameterBase Parameters;

    public EffectTypeEnum GetEffectType() =>
    Enum.TryParse(EffectType, out EffectTypeEnum result) ? result : EffectTypeEnum.None;
    public TriggerConditionEnum GetTriggerConditionEnum() =>
    Enum.TryParse(TriggerCondition, out TriggerConditionEnum result) ? result : TriggerConditionEnum.None;
    public TargetEnum GetTargetEnum() =>
    Enum.TryParse(Target, out TargetEnum result) ? result : TargetEnum.None;

    public bool Validate(string ownerName)
    {
        if (!UtilTools.ValidateEnum(EffectType, out EffectTypeEnum resultEffect, nameof(EffectType), ownerName)) return false;
        if (!UtilTools.ValidateEnum(TriggerCondition, out TriggerConditionEnum resultTrigger, nameof(TriggerCondition), ownerName)) return false;
        if (!UtilTools.ValidateEnum(Target, out TargetEnum resultTarget, nameof(Target), ownerName)) return false;
        if (UtilTools.ValidateObject(Parameters, nameof(Parameters), ownerName))
        {
            if (!Parameters.Validate(ownerName)) return false;
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
public abstract class EffectParameterBase
{
    public abstract bool Validate(string ownerName);
}

public class DamageParameterEffect : EffectParameterBase
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

public class HealParameterEffect : EffectParameterBase
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

public class MultipleDamageEffect : EffectParameterBase
{
    public float Multiplier;

    public override bool Validate(string ownerName)
    {
        if (!UtilTools.ValidatePositiveValue(Multiplier, nameof(Multiplier), ownerName)) return false;
        return true;
    }
}

public class MultipleDefenseEffect : EffectParameterBase
{
    public float Multiplier;

    public override bool Validate(string ownerName)
    {
        if (!UtilTools.ValidatePositiveValue(Multiplier, nameof(Multiplier), ownerName)) return false;
        return true;
    }
}

public class ShieldEffect : EffectParameterBase
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

public class SpeedReductionEffect : EffectParameterBase
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

public class FrozenEffect : EffectParameterBase
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


public class DrawCardEffect : EffectParameterBase
{
    public int DrawAmount;

    public override bool Validate(string ownerName)
    {
        if (!UtilTools.ValidatePositiveValue(DrawAmount, nameof(DrawAmount), ownerName)) return false;
        return true;
    }
}

public class LifeStealEffect : EffectParameterBase
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
        var effectBaseType = typeof(EffectParameterBase);
        var subclasses = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => effectBaseType.IsAssignableFrom(t) && !t.IsAbstract);

        foreach (var subclass in subclasses)
        {
            EffectTypeMap[subclass.Name] = subclass; // 使用类名作为 EffectType
        }
    }
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(EffectParameterBase);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        JObject jsonObject = JObject.Load(reader);
        string typeName = jsonObject["$type"]?.ToString(); // 直接读取 "$type" 作为类名

        if (string.IsNullOrEmpty(typeName))
        {
            throw new Exception("Missing or invalid $type field in JSON.");
        }

        // 反射获取对应的类型（确保类型是 EffectParameterBase 的子类）
        Type effectType = Type.GetType(typeName);
        if (effectType == null || !typeof(EffectParameterBase).IsAssignableFrom(effectType))
        {
            throw new Exception($"Unknown or invalid effect type: {typeName}");
        }

        var effect = Activator.CreateInstance(effectType); // 反射创建对象
        serializer.Populate(jsonObject.CreateReader(), effect); // 反序列化填充对象
        return effect;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        JObject jsonObject = JObject.FromObject(value);
        jsonObject["$type"] = value.GetType().Name; // 或者完整名称
        jsonObject.WriteTo(writer);
    }
}

#endregion