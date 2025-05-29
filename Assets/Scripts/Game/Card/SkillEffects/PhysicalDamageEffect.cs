namespace Game.CardSystem
{
    using Game.Character;
    using Models;
    using UnityEngine;

    public class PhysicalDamageEffect : EffectBase
    {
        public float Damage { private set; get; }

        // public PhysicalDamageEffect(PhysicalDamageData effectData) : base(effectData)
        // {
        //     Damage = effectData.Damage;
        // }

        public override void InitializeData(EffectDataBase effectData)
        {
            base.InitializeData(effectData);
            if (effectData is PhysicalDamageData physical)
            {
                Damage = physical.Damage;
            }

        }

        public override void ApplyEffect(CharacterBase caster, CharacterBase target)
        {
            Debug.Log($"看看我的铁拳{Damage}");
        }
    }
}