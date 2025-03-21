

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
    public SkillLevel[] Levels;
}


[Serializable]
public class SkillLevel
{
    public string Level;
    public string CoolDown;
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
    public EffectParameters Parameters;

    public EffectTypeEnum GetEffectType() =>
    Enum.TryParse(EffectType, out EffectTypeEnum result) ? result : EffectTypeEnum.Damage;
}

[Serializable]
public class EffectParameters
{
    public string Element;
    public int Value;

    public ElementEnum GetElementEnum() => Enum.TryParse(Element, out ElementEnum result) ? result : ElementEnum.None;
}