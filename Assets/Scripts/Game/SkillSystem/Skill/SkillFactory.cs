namespace Game.SkillSystem
{
    using System.Collections.Generic;
    using Common;
    using Models;
    using UnityEngine;

    public interface ISkillFactory
    {
        Skill CreateSkill(SkillData data, int level);
    }

    [AutoRegisterFactory(typeof(ISkillFactory))]
    public class SkillFactory : ISkillFactory
    {
        public Skill CreateSkill(SkillData data, int level)
        {
            // Debug.Log(data.Name);
            // Debug.Log(data.LevelsData[level]);
            //创建配置里面的effect
            // List<SkillEffectBase> effects = EffectFactory.CreateSkillEffect(data.LevelsData[level].EffectIDs);
            var effectFactory = IoCContainer.GetService<IEffectFactory>();
            List<SkillEffectBase> effects = effectFactory.CreateSkillEffect(data.LevelsData[level].EffectIDs);

            var buffFactory = IoCContainer.GetService<IBuffFactory>();
            //TODO：创建配置里面的buff
            List<SkillBuffBase> buffs = buffFactory.CreateSkillBuff(data.LevelsData[level].BuffIDs);
            // return new Skill(data, level, effects);
            return null;

        }
    }
}