namespace Common.Factories
{
    using System.Collections.Generic;
    using Game.SkillSystem;
    using UnityEngine;

    public interface IEffectFactory
    {
        List<SkillEffectBase> CreateSkillEffect(string[] effectIDs);
    }

    [AutoRegisterFactory(typeof(IEffectFactory))]
    public class EffectFactory : IEffectFactory
    {

        public List<SkillEffectBase> CreateSkillEffect(string[] effectIDs)
        {

            if (effectIDs == null || effectIDs.Length == 0)
            {
                Debug.LogError("Effect ID Array is null");
                return null;
            }
            List<SkillEffectBase> effectList = new();
            var repository = IoCContainer.GetService<SkillEffectRepository>();
            for (int i = 0; i < effectIDs.Length; i++)
            {
                var effectData = repository.GetById(effectIDs[i]);
                string effectClassName = repository.GetNameById(effectIDs[i]) + "Effect";
                //  effectIDs[i] + "Data";
                //here I need to use id to get the name of effect data class
                SkillEffectBase effect = GameTypeCache.CreateInstanceByClassName<SkillEffectBase>(effectClassName, true);
                // Debug.Log(effect);
                if (effect != null)
                {
                    effect.InitializeData(effectData);
                    effectList.Add(effect);
                }
                else
                {
                    Debug.Log("No effect data");
                }

            }

            return effectList;
        }

    }
}