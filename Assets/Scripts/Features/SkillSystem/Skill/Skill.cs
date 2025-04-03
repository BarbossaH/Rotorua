public class Skill : ISkill
{
    //传入data可以创建Skill的基础数据
    public SkillData skillData;
    public ISkillEffect[] skillEffects;

    public int SkillID { get; private set; }
    public string SkillName { get; private set; }
    public float CoolDown { get; private set; }
    public SkillTypeEnum skillType { get; private set; }

    public Skill(SkillData data, ISkillEffect[] effects)
    {
        skillData = data;
        skillEffects = effects;
    }
    public void ApplySkill()
    {
    }
}