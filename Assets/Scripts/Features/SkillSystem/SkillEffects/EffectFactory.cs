public static class EffectFactory
{
    //TODO: 我需要获得effect的程序集
    public static EffectBase CreateSkillEffect()
    {
        return new DamageEffect();
    }
}