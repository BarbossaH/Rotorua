namespace Common.Factories
{
    using System.Collections.Generic;
    using Game.CardSystem;
    using UnityEngine;

    public interface IEffectFactory
    {
        List<EffectBase> CreateSkillEffects(List<string> effectIDs);
    }

    [AutoRegisterFactory(typeof(IEffectFactory))]
    public class EffectFactory : IEffectFactory
    {

        public List<EffectBase> CreateSkillEffects(List<string> effectIDs)
        {

            if (effectIDs == null || effectIDs.Count == 0)
            {
                return null;
            }
            List<EffectBase> effectList = new();
            var repository = IoCContainer.GetService<SkillEffectRepository>();
            for (int i = 0; i < effectIDs.Count; i++)
            {
                var effectData = repository.GetById(effectIDs[i]);
                string effectClassName = repository.GetNameById(effectIDs[i]) + "Effect";
                //  effectIDs[i] + "Data";
                //here I need to use id to get the name of effect data class
                EffectBase effect = GameTypeCache.CreateInstanceByClassName<EffectBase>(effectClassName, true);
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