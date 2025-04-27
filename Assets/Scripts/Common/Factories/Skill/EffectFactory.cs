namespace Common
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
        //store all effect class types to create the corresponding effect class based on the effect ID, note: the effect ID must match the effect class name and effect classes must inherit from the EffectDataBase class;
        // private static readonly Dictionary<string, Type> _EffectsDict = new();

        //when initialize the skill manager, register 
        // public static void RegisterEffects()
        // {
        //     var effects = Assembly.GetExecutingAssembly().GetTypes()
        //     .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(EffectDataBase)));

        //     foreach (var e in effects)
        //     {
        //         _EffectsDict[e.Name] = e;
        //         // Debug.Log($"注册 Effect Param:{e.Name}");
        //     }
        // }

        public List<SkillEffectBase> CreateSkillEffect(string[] effectIDs)
        {


            // 解析 ParametersData
            if (effectIDs == null || effectIDs.Length == 0)
            {
                Debug.LogError("Effect ID Array is null");
                return null;
            }
            List<SkillEffectBase> effectList = new();
            var repository = IoCContainer.GetService<ISkillEffectRepository>();
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