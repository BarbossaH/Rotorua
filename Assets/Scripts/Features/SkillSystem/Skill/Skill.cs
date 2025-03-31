public class Skill : ISkill
{
    public SkillData skillData;
    public EffectBase effect;

    public Skill(SkillData data, EffectBase effect)
    {
        skillData = data;
        this.effect = effect;
    }
    public void ApplySkill()
    {
    }
}