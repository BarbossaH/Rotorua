namespace Game.SkillSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Common;
    using Models;
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

            List<EffectDataBase> effectDataList = new();
            List<SkillEffectBase> effectList = new();
            // 解析 ParametersData
            if (effectIDs == null || effectIDs.Length == 0)
            {
                Debug.LogError("Effect ID Array is null");
                return null;
            }

            for (int i = 0; i < effectIDs.Length; i++)
            {
                string typeName = effectIDs[i] + "Data";
                EffectDataBase effectData = GameTypeCache.CreateInstanceByClassName<EffectDataBase>(typeName);
                if (effectData != null)
                {
                    effectDataList.Add(effectData);
                }

            }
            foreach (var data in effectDataList)
            {
                // string typeName = data.ParametersData.SubclassName.Replace("ParameterData", "Effect");
                string typeName = data.Name + "Effect";
                Debug.Log(typeName);

                SkillEffectBase effect = GameTypeCache.CreateInstanceByClassName<SkillEffectBase>(typeName);
                if (effect != null)
                {
                    effectList.Add(effect);
                }

            }
            return effectList;
        }


    }
}