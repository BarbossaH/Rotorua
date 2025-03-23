using System.Collections.Generic;
using UnityEngine;

public class SkillManager : Singleton<SkillManager>
{
    private readonly Dictionary<string, SkillData> _SkillDataDict = new();
    private readonly Dictionary<string, Skill> _SkillDict = new();

    public void LoadSkillData()
    {
        SkillData[] skillDatas = JsonManager.Instance.GetData<SkillDatabase>(typeof(SkillDatabase).Name).Skills;

        Debug.Log(skillDatas.Length);
        foreach (var s in skillDatas)
        {
            _SkillDataDict[s.ID] = s;
        }

        Debug.Log("SkillData has been loaded");

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

    //TODO: create skill and skill effect, before this, thinking how to design the effect trigger type, including active, passive, and buff/debuff.
}