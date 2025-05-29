
using Game.Character;
using Models;
using UnityEngine;

namespace Game.CardSystem
{
    public class IceArrowEffect : EffectBase
    {
        public float Damage { get; private set; }

        public override void InitializeData(EffectDataBase effectData)
        {
            base.InitializeData(effectData);
            if (effectData is IceArrowData iceArrowData)
            {
                Damage = iceArrowData.Damage;
            }
        }
        public override void ApplyEffect(CharacterBase caster, CharacterBase target)
        {
            Debug.Log("Ice arrow makes you cannot move fast");
        }

    }
}
