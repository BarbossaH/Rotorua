using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public static class EffectFactory
{
    private static readonly Dictionary<string, Type> _EffectParamsDict = new();

    //when initialize the skill manager, register 
    public static void RegisterEffects()
    {
        var effects = Assembly.GetExecutingAssembly().GetTypes()
        .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(EffectBase)));

        foreach (var e in effects)
        {
            _EffectParamsDict[e.Name] = e;
            Debug.Log($"注册 Effect Param:{e.Name}");
        }
    }

    public static List<ISkillEffect> CreateSkillEffect(EffectData[] effectDatas)
    {
        List<ISkillEffect> effects = new();
        // 解析 ParametersData

        foreach (var data in effectDatas)
        {
            string typeName = data.ParametersData.SubclassName.Replace("ParameterData", "Effect");
            Debug.Log(typeName);
            if (_EffectParamsDict.TryGetValue(typeName, out var t))
            {
                ISkillEffect effect = Activator.CreateInstance(t, new object[] { data, data.ParametersData }) as ISkillEffect;
                // EffectBase effect = new EffectBase(data, effectParams);
                effects.Add(effect);
            }

        }
        return effects;
    }


}