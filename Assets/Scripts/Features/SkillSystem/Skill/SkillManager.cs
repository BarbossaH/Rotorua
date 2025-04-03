using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SkillManager : Singleton<SkillManager>
{
    public bool IsSkillDataLoaded { get; private set; }
    private readonly Dictionary<string, SkillData> _SkillDataDict = new();
    // private readonly Dictionary<string, Type> _EffectParamsDict = new();
    // private readonly Dictionary<string, Skill> _SkillDict = new();

    public override void Init()
    {
        base.Init();

        //register effect parameter classes
        EffectFactory.RegisterEffectParameters();
    }

    //load data from json
    public async UniTask LoadSkillDataAsync()
    {
        if (IsSkillDataLoaded) return;

        await JsonManager.Instance.WaitForJsonLoad(); // 确保 JSON 数据已加载

        SkillData[] skillDataArray = JsonManager.Instance.GetData<SkillDatabase>(typeof(SkillDatabase).Name)?.Skills;

        if (skillDataArray == null)
        {
            Debug.LogError("技能数据解析失败: skillDataArray 为空！");
            return;
        }
        if (skillDataArray.Length == 0)
        {
            Debug.LogError("技能数据为空，没有任何技能！");
            return;
        }

        foreach (var s in skillDataArray)
        {
            if (s == null)
            {
                Debug.LogError("发现空技能对象，跳过！");
                continue;
            }
            if (string.IsNullOrEmpty(s.ID))
            {
                Debug.LogError($"技能 {s.Name} 的 ID 为空，跳过！");
                continue;
            }

            Debug.Log($"加载技能: {s.ID} - {s.Name}");
            _SkillDataDict[s.ID] = s;

            if (s.LevelsData == null || s.LevelsData.Length == 0)
            {
                Debug.LogError($"技能 {s.ID} ({s.Name}) 的 LevelsData 为空，可能数据有误！");
            }
        }

        Debug.Log("SkillData has been loaded");
        IsSkillDataLoaded = true;
    }

    public SkillData GetSkillData(string id)
    {
        return _SkillDataDict.TryGetValue(id, out var skillData) ? skillData : null;
    }

    public SkillLevel GetSkillLevelEffect(string id, int level)
    {
        return _SkillDataDict[id].LevelsData[level];
    }

    #region SkillEffects



    // private void RegisterParameters()
    // {
    //     var parameters = Assembly.GetExecutingAssembly().GetTypes()
    //     .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(EffectParameterBase)));

    //     foreach (var p in parameters)
    //     {
    //         _EffectParamsDict[p.Name] = p;
    //         // Debug.Log($"注册 Effect Param:{p.Name}");
    //     }
    // }
    #endregion
    //TODO: create skill and skill effect, before this, thinking how to design the effect trigger type, including active, passive, and buff/debuff.
}