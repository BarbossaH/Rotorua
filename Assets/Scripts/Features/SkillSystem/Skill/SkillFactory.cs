public static class SkillFactory
{
    public static Skill CreateSkill(SkillData data, int level)
    {
        EffectBase effect = EffectFactory.CreateSkillEffect();
        return new Skill(data, effect);
    }
}