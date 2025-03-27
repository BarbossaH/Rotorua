using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;

public class SkillManager : Singleton<SkillManager>
{
    public bool IsSkillDataLoaded { get; private set; }
    private readonly Dictionary<string, SkillData> _SkillDataDict = new();
    private readonly Dictionary<string, Type> _EffectsDict = new();
    // private readonly Dictionary<string, Skill> _SkillDict = new();

    public override void Init()
    {
        base.Init();

        //loading skill data json file
        _ = LoadSkillDataAsync();

        //register effect parameter classes

        //register effect skill effect classes
        RegisterEffects();

    }
    private async Task LoadSkillDataAsync()
    {
        if (IsSkillDataLoaded) return;

        // SkillDatabase data = JsonManager.Instance.GetData<SkillDatabase>(typeof(SkillDatabase).Name);
        // if (data == null)
        // {
        //     Debug.Log("Data is null");
        //     return;
        // }
        await JsonManager.Instance.WaitForJsonLoad(); // 确保 JSON 数据已加载
        SkillData[] skillDatas = JsonManager.Instance.GetData<SkillDatabase>(typeof(SkillDatabase).Name).Skills;

        Debug.Log(skillDatas[0].Name);
        foreach (var s in skillDatas)
        {
            _SkillDataDict[s.ID] = s;
        }

        Debug.Log("SkillData has been loaded");
        IsSkillDataLoaded = true;
        #region for test
        // Debug.Log(_SkillDataDict["10001"].Name + " " + _SkillDataDict["10001"].ID + " " + _SkillDataDict["10001"].Levels[0].Level + " " + _SkillDataDict["10001"].Levels[0].CoolDown + " " + _SkillDataDict["10001"].Levels[0].Cost[0].Type + " " + _SkillDataDict["10001"].Levels[0].Effects[0].EffectType + " " + _SkillDataDict["10001"].Levels[0].Effects[0].Parameters.Damage + " " + _SkillDataDict["10001"].Levels[0].Effects[0].Parameters.Element + " " + _SkillDataDict["10001"].Levels[0].Effects[0].Parameters.TurnsAmount);
        #endregion
    }

    public SkillData GetSkillData(string id)
    {
        return _SkillDataDict.TryGetValue(id, out var skillData) ? skillData : null;
    }

    public SkillLevel GetSkillLevelEffect(string id, int level)
    {
        return _SkillDataDict[id].Levels[level];
    }

    #region SkillEffects

    private void RegisterEffects()
    {
        var effectTypes = Assembly.GetExecutingAssembly().GetTypes()
        .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(EffectBase)));
        foreach (var type in effectTypes)
        {
            _EffectsDict[type.Name] = type;
            Debug.Log($"注册 Effect: {type.Name}");
        }
    }
    #endregion
    //TODO: create skill and skill effect, before this, thinking how to design the effect trigger type, including active, passive, and buff/debuff.
}