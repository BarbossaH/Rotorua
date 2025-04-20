namespace Game.SkillSystem
{

    using System;
    using Common;

    [AutoRegisterManager]
    public class SkillManager : Singleton<SkillManager>
    {
        // private readonly Dictionary<string, Type> _EffectParamsDict = new();
        // private readonly Dictionary<string, Skill> _SkillDict = new();

        #region SkillEffects

        #endregion
        //TODO: create skill and skill effect, before this, thinking how to design the effect trigger type, including active, passive, and buff/debuff.
        public Skill CreateSkill(string skillId, int level = 0)
        {
            var skillRepo = IoCContainer.GetService<ISkillRepository>();
            if (!skillRepo.IsLoaded)
            {
                throw new InvalidOperationException("Skill repository is not loaded yet!");
            }
            var skillFactory = IoCContainer.GetService<ISkillFactory>();
            return skillFactory.CreateSkill(skillRepo.GetById(skillId), level);
        }
    }
}