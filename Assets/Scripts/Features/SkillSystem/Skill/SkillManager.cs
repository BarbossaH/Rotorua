
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
        var skillResp = IoCContainer.GetService<IRepository<SkillData, string>>();
        Debug.Log(skillResp.IsLoaded);
        return SkillFactory.CreateSkill(skillResp.GetById(skillId), level);
    }
}