namespace Managers
{

    using Common;
    using Common.Factories;
    using Game.CardSystem;

    [AutoRegisterManager]
    public class SkillManager : Singleton<SkillManager>
    {

        //TODO: create skill and skill effect, before this, thinking how to design the effect trigger type, including active, passive, and buff/debuff.

        public Skill CreateSkill(string skillId, int level = 0)
        {
            ISkillFactory skillFactory = IoCContainer.GetService<ISkillFactory>();
            return skillFactory.CreateSkill(skillId, level);
        }
    }
}