namespace Common.Factories
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
            //error: json configuration is easy to write wrong words, so i need to think about this later.

            if (buffIDs == null || buffIDs.Length == 0)
            {
                Debug.LogError("buff ids are invalid!");
                return null;
            }

            List<SkillBuffBase> skillBuffList = new();
            var repository = IoCContainer.GetService<SkillBuffRepository>();
            for (int i = 0; i < buffIDs.Length; i++)
            {
                Debug.Log(buffIDs[i]);
                var buffData = repository.GetById(buffIDs[i]);
                string buffClassName = repository.GetNameById(buffIDs[i]) + "Buff";
                // Debug.Log(buffData.Name);
                //create the buff classes by using reflection
                SkillBuffBase skillBuff = GameTypeCache.CreateInstanceByClassName<SkillBuffBase>(buffClassName, true);
                if (skillBuff != null)
                {
                    skillBuff.InitializeData(buffData);
                    skillBuffList.Add(skillBuff);
                }
                else
                {
                    Debug.LogError("There is no buff data");
                }
            }
            return skillBuffList;
        }
    }
}