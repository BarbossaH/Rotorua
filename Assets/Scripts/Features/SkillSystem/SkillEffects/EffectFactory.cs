using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public static class EffectFactory
{
    private static readonly Dictionary<string, Type> _EffectParamsDict = new();

    //when initialize the skill manager, register 
    public static void RegisterEffectParameters()
    {
        var parameters = Assembly.GetExecutingAssembly().GetTypes()
        .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(EffectBase)));

        foreach (var p in parameters)
        {
            _EffectParamsDict[p.Name] = p;
            Debug.Log($"注册 Effect Param:{p.Name}");
        }
    }

    public static List<ISkillEffect> CreateSkillEffect(EffectData[] effectDatas)
    {
        List<ISkillEffect> effects = new();
        // 解析 ParametersData

        foreach (var data in effectDatas)
        {
            if (_EffectParamsDict.TryGetValue(data.ParametersData.SubclassName, out var t))
            {
                EffectParameterDataBase effectParams = Activator.CreateInstance(t) as EffectParameterDataBase;
                // EffectBase effect = new EffectBase(data, effectParams);
                // effects.Add(effect);
            }

        }
        return effects;
    }


}