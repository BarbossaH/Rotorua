namespace Common
{
    using System.Collections.Generic;
    using Game.SkillSystem;
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

            //TODO:在创建effect之前或者在创建技能之前，要判断这个技能能不能被施放，当然这个是在创建技能之前，这个地方是不管的
            var effectFactory = IoCContainer.GetService<IEffectFactory>();
            Debug.Log(data);
            List<SkillEffectBase> effects = effectFactory.CreateSkillEffect(data.LevelsData[level].EffectIDs);


            var buffFactory = IoCContainer.GetService<IBuffFactory>();
            //TODO：创建配置里面的buff
            List<SkillBuffBase> buffs = buffFactory.CreateSkillBuff(data.LevelsData[level].BuffIDs);
            return new Skill(data, level, effects, buffs);

        }
    }
}