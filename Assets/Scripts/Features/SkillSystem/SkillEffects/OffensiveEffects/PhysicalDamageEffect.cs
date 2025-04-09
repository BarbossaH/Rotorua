using UnityEngine;

public class PhysicalDamageEffect : SkillEffectBase
{
    public float Damage { private set; get; }

    public PhysicalDamageEffect(PhysicalDamageData effectData) : base(effectData)
    {
        Damage = effectData.Damage;
    }

    public override void ApplyEffect()
    {

    }
}