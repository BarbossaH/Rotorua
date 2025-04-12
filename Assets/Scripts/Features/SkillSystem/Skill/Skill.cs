using System.Collections.Generic;

public class Skill : ISkill
{
    //传入data可以创建Skill的基础数据
    public SkillData SkillData { get; private set; }
    public SkillLevel SkillLevel => SkillData.LevelsData[Level];

    public int SkillID { get; private set; }
    public string SkillName { get; private set; }
    public int Level { get; private set; }
    public float CoolDown => SkillLevel.CoolDown;
    public float CoolDownTimer { get; private set; }
    public SkillTypeEnum skillType { get; private set; }

    //skill effects and buffs
    public List<SkillEffectBase> SkillEffects = new();
    public List<SkillBuffBase> SkillBuffs = new();
    public Skill(SkillData data, int level, List<SkillEffectBase> effects, List<SkillBuffBase> buffs)
    {
        SkillName = data.Name;
        SkillData = data;
        SkillEffects = effects;
        SkillBuffs = buffs;
        Level = level;

    }
    public void ApplySkill()
    {
        foreach (var e in SkillEffects)
        {
            e.ApplyEffect();
        }
    }
}