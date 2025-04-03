public class DamageParams : EffectBase, ISkillEffect
{
    public float Damage { private set; get; }
    public float CriticalChance { private set; get; }
    public string Element { private set; get; }
    public DamageParams(EffectData effectData, DamageParameterData damageParameter) : base(effectData)
    {
        Damage = damageParameter.Damage;
        CriticalChance = damageParameter.CriticalChance;
        Element = damageParameter.Element;
    }

    public void ApplyEffect()
    {
        throw new System.NotImplementedException();
    }
}