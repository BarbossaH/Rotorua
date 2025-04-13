
using System;
using UnityEngine;

public class SkillManager : Singleton<SkillManager>
{
    // private readonly Dictionary<string, Type> _EffectParamsDict = new();
    // private readonly Dictionary<string, Skill> _SkillDict = new();
    public override void Init()
    {
        base.Init();
        //register effect parameter classes
        EffectFactory.RegisterEffects();
    }


    #region SkillEffects

    #endregion
    //TODO: create skill and skill effect, before this, thinking how to design the effect trigger type, including active, passive, and buff/debuff.
    public Skill CreateSkill(string skillId, int level = 0)
    {
        var skillRepo = IoCContainer.GetService<IRepository<SkillData, string>>();
        if (!skillRepo.IsLoaded)
        {
            throw new InvalidOperationException("Skill repository is not loaded yet!");
        }
        return SkillFactory.CreateSkill(skillRepo.GetById(skillId), level);
    }
}