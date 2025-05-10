using Game.Character;
using Models;
using UnityEngine;

namespace Game.SkillSystem
{
    public class FireballEffect : SkillEffectBase
    {
        public float Damage { get; private set; }

        public override void InitializeData(EffectDataBase effectData)
        {
            base.InitializeData(effectData);
            if (effectData is FireballData fireball)
            {
                Damage = fireball.Damage;
            }
        }
        public override void ApplyEffect(CharacterBase caster, CharacterBase target)
        {
            Debug.Log($"火球术烧死你 {Damage}");
        }
    }
}