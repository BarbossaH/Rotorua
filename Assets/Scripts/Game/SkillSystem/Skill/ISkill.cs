using Game.Character;

namespace Game.CardSystem
{
    public interface ISkill
    {
        void ApplySkill(CharacterBase caster, CharacterBase target);
    }
}
