namespace Common
{
    using System;
    using System.Collections.Generic;
    using Game.SkillSystem;
    using Models;
    using UnityEngine;

    public interface ISkillFactory
    {
        Skill CreateSkill(string skillId, int level);
    }

    [AutoRegisterFactory(typeof(ISkillFactory))]
    public class SkillFactory : ISkillFactory
    {


        public Skill CreateSkill(string skillId, int level)
        {
            // Debug.Log(data.Name);
            // Debug.Log(data.LevelsData[level]);
            var skillRepo = IoCContainer.GetService<ISkillRepository>();

            if (!skillRepo.IsLoaded)
            {
                throw new InvalidOperationException("Skill repository is not loaded yet!");
            }
            var data = skillRepo.GetById(skillId);

            Debug.Log(data);
            var effectFactory = IoCContainer.GetService<IEffectFactory>();
            List<SkillEffectBase> effects = effectFactory.CreateSkillEffect(data.LevelsData[level].EffectIDs);

            var buffFactory = IoCContainer.GetService<IBuffFactory>();
            List<SkillBuffBase> buffs = buffFactory.CreateSkillBuff(data.LevelsData[level].BuffIDs);
            return new Skill(data, level, effects, buffs);

        }
    }
}