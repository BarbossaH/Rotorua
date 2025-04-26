namespace Common
{
    using System.Collections.Generic;
    using Game.SkillSystem;
    using UnityEngine;

    public interface IBuffFactory
    {
        List<SkillBuffBase> CreateSkillBuff(string[] buffIDs);
    }

    [AutoRegisterFactory(typeof(IBuffFactory))]
    public class BuffFactory : IBuffFactory
    {
        //here, through buffIds, we can create the objects of the corresponding buffs.
        //through id to get the buff name, and use the buff name to get the buff class name,
        // then use reflection to create the buff class

        public List<SkillBuffBase> CreateSkillBuff(string[] buffIDs)
        {
            if (buffIDs == null || buffIDs.Length == 0)
            {
                Debug.LogError("buff ids are invalid!");
                return null;
            }

            List<SkillBuffBase> skillBuffBases = new();
            foreach (var id in buffIDs)
            {
                Debug.Log(id + " : I am a buff id");
            }
            return null;
        }
    }
}