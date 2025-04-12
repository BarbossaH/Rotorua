using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public static class EffectFactory
{
    //store all effect class types to create the corresponding effect class based on the effect ID, note: the effect ID must match the effect class name and effect classes must inherit from the EffectDataBase class;
    private static readonly Dictionary<string, Type> _EffectsDict = new();

    //when initialize the skill manager, register 
    public static void RegisterEffects()
    {
        var effects = Assembly.GetExecutingAssembly().GetTypes()
        .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(EffectDataBase)));

        foreach (var e in effects)
        {
            _EffectsDict[e.Name] = e;
            Debug.Log($"注册 Effect Param:{e.Name}");
        }
    }

    public static List<SkillEffectBase> CreateSkillEffect(string[] effectIDs)
    {
        //这里的逻辑要大改，因为技能数据的结构调整，所有的effect数据需要从另外的内存数据中读取，类似skill data，所以应该有一个effect data base 类，而所有的effect数据都在effect data base 类里，这个类可以通过 effect id去获取 effect data，现在我要去确认skill也是这么做的 
        //TODO：检查skill data 类
        List<EffectDataBase> effectDatas = new();
        List<SkillEffectBase> effects = new();
        // 解析 ParametersData
        if (effectIDs == null || effectIDs.Length == 0)
        {
            Debug.LogError("Effect ID Array is null");
            return null;
        }

        for (int i = 0; i < effectIDs.Length; i++)
        {
            string typeName = effectIDs[i] + "Data";
            //TODO:下一步就要去SkillManager里获得对应的数据，因为读取出来effect的json数据后，需要存起来，这里获得对应的effect的id，所以可以获得effect的数据，获得数据后就去创建逻辑类，所以~我现在有点混乱就是因为没有完全梳理出整个游戏的流程，数据读取，数据存储，数据的获得和使用，这是明天要做的事。第一步，先梳理数据的读取。
        }
        foreach (var data in effectDatas)
        {
            // string typeName = data.ParametersData.SubclassName.Replace("ParameterData", "Effect");
            string typeName = data.Name + "Effect";
            Debug.Log(typeName);
            if (_EffectsDict.TryGetValue(typeName, out var t))
            {
                SkillEffectBase effect = Activator.CreateInstance(t, new object[] { data }) as SkillEffectBase;

                effects.Add(effect);
            }

        }
        return effects;
    }


}