using System.Collections.Generic;

public static class SkillFactory
{
    public static Skill CreateSkill(SkillData data, int level)
    {
        List<ISkillEffect> effects = EffectFactory.CreateSkillEffect(data.LevelsData[level].EffectsData);

        return new Skill(data, effects.ToArray());
    }
}