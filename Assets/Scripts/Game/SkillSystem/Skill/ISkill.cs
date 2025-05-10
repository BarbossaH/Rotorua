using Game.Character;

namespace Game.SkillSystem
{
    public interface ISkill
    {
        void ApplySkill(CharacterBase caster, CharacterBase target);
    }
}
