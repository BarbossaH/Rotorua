using UnityEngine;

public class DamageEffect : EffectBase, ISkillEffect
{
    public float Damage { private set; get; }
    public float CriticalChance { private set; get; }
    public string Element { private set; get; }

    public DamageEffect(EffectData effectData, DamageParameterData damageParameter) : base(effectData)
    {
        Damage = damageParameter.Damage;
        CriticalChance = damageParameter.CriticalChance;
        Element = damageParameter.Element;
    }

    public void ApplyEffect()
    {
        Debug.Log($"attack{Damage}");
    }
}