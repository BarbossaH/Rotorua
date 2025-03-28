

using System;

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

    public SkillTypeEnum GetSkillTypeEnum() => Enum.TryParse(SkillType, out SkillTypeEnum result) ? result : SkillTypeEnum.Active;
}


[Serializable]
public class SkillLevel
{
    public string Level;
    public int CoolDown;
    public CostData[] Cost;
    public EffectData[] Effects;
}

[Serializable]
public class CostData
{
    public string Type;
    public int Value;

    public CostTypeEnum GetCostType() => Enum.TryParse(Type, out CostTypeEnum result) ? result : CostTypeEnum.Mana;
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
}

#region Skill Effect Parameter Classes

[Serializable]
public abstract class EffectParameterBase { }

public class DamageParameterEffect : EffectParameterBase
{
    public float Damage;
    public float CriticalChance = 0;
    public string Element;

    public ElementEnum GetElementEnum() => Enum.TryParse(Element, out ElementEnum result) ? result : ElementEnum.None;
}

public class HealParameterEffect : EffectParameterBase
{
    public float HealAmount;
    public int Duration;
}

public class MultipleDamageEffect : EffectParameterBase
{
    public float Multiplier;
}

public class MultipleDefenseEffect : EffectParameterBase
{
    public float Multiplier;
}

public class ShieldEffect : EffectParameterBase
{
    public float ShieldAmount;
    public int Duration;
}

public class SpeedReductionEffect : EffectParameterBase
{
    public float Reduction;
    public int Duration;
    public float Chance = 100;
}
public class FrozenEffect : EffectParameterBase
{
    public int Duration;
    public float Chance = 100;
}

public class DrawCardEffect : EffectParameterBase
{
    public int DrawAmount { get; set; }
}

public class LifeStealEffect : EffectParameterBase
{
    public int Lifesteal { get; set; }
}

#endregion