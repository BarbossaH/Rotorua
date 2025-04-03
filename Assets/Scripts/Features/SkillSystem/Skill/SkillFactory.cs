using System.Collections.Generic;
using UnityEngine;

public static class SkillFactory
{
    public static Skill CreateSkill(SkillData data, int level)
    {
        Debug.Log(data);
        Debug.Log(data.Name);
        Debug.Log(data.LevelsData[level]);
        List<ISkillEffect> effects = EffectFactory.CreateSkillEffect(data.LevelsData[level].EffectsData);

        return new Skill(data, effects.ToArray());
    }
}