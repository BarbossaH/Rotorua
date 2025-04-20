namespace Game.SkillSystem
{
    using System.Collections.Generic;
    using Common;

    public interface IBuffFactory
    {
        List<SkillBuffBase> CreateSkillBuff(string[] buffIDs);
    }

    [AutoRegisterFactory(typeof(IBuffFactory))]
    public class BuffFactory : IBuffFactory
    {
        public List<SkillBuffBase> CreateSkillBuff(string[] buffIDs)
        {
            return null;
        }
    }
}