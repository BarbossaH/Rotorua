

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
    public string Description;
    public string SkillType;
    public SkillLevel[] Levels;

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
    public string TriggerCondition;//TODO:cast to enum
    public string Target;//TODO:cast to enum
    public EffectParameters Parameters;

    public EffectTypeEnum GetEffectType() =>
    Enum.TryParse(EffectType, out EffectTypeEnum result) ? result : EffectTypeEnum.Damage;
}

[Serializable]
public class EffectParameters
{
    public int Duration;
    public float Damage;
    public string Element;
    public float Multiplier;
    public float SpeedReduction;
    public float Chance;

    public ElementEnum GetElementEnum() => Enum.TryParse(Element, out ElementEnum result) ? result : ElementEnum.None;
}