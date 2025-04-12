using System.Collections.Generic;
using UnityEngine;

public static class SkillFactory
{
    public static Skill CreateSkill(SkillData data, int level)
    {
        Debug.Log(data);
        Debug.Log(data.Name);
        Debug.Log(data.LevelsData[level]);
        //创建配置里面的effect
        List<SkillEffectBase> effects = EffectFactory.CreateSkillEffect(data.LevelsData[level].EffectIDs);
        //TODO：创建配置里面的buff
        // List<SkillBuffBase> buffs = BuffFactory.CreateSkillBuff(data.LevelsData[level].BuffIDs);
        // return new Skill(data, level, effects);
        return null;

    }
}