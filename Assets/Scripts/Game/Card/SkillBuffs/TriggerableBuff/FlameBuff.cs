using Common.Defs;
using Game.Character;
using Models;
using UnityEngine;

namespace Game.CardSystem
{
    public class FlameBuff : BuffBase, IPeriodDamageBuff
    {
        public float Damage { get; private set; }
        public TriggerTimingEnum TriggerTiming { get; private set; }
        public override void InitializeData(BuffDataBase buffData)
        {
            base.InitializeData(buffData);
            if (buffData is FlameData flameData)
            {
                CurrentTurn = buffData.Duration;
                Damage = flameData.Damage;
                TriggerTiming = flameData.GetTriggerTimingEnum();
            }

        }
        public void ApplyDamageEffect()
        {
        }

        public float GetTickDamage()
        {
            return 0;
        }

        public override void OnAdd(CharacterBase character)
        {
            Debug.Log($"I am on fire, and take {Damage} points of damage and trigger is {TriggerTiming} and duration is {Duration} and isExpired is {IsExpired()}, is permanent is {IsPermanent()}");
        }

        public override void OnRemove(CharacterBase character)
        {
        }

        public override void OnTick()
        {
            //-1 means it is a permanent buff
            if (IsPermanent()) return;
            if (!IsExpired())
            {
                CurrentTurn--;
            }

        }
    }
}