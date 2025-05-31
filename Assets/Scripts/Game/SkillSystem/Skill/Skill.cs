namespace Game.CardSystem
{
    using System.Collections.Generic;
    using Common.Defs;
    using Game.Character;
    using Models;

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
        public SkillTypeEnum SkillType { get; private set; }

        //skill effects and buffs
        public List<EffectBase> SkillEffects = new();
        public List<BuffBase> SkillBuffs = new();
        public Skill(SkillData data, int level, List<EffectBase> effects, List<BuffBase> buffs)
        {
            SkillName = data.Name;
            SkillData = data;
            SkillEffects = effects;
            SkillBuffs = buffs;
            Level = level;
        }
        public void ApplySkill(CharacterBase caster, CharacterBase target)
        {
            foreach (var e in SkillEffects)
            {
                e.ApplyEffect(caster, target);
            }

            foreach (var b in SkillBuffs)
            {
                // b.OnAdd();
            }
        }
    }
}