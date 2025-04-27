using Models;
using UnityEngine;

namespace Game.SkillSystem
{
    public class FlameBuff : SkillBuffBase, IPeriodDamageBuff
    {
        public float Damage { get; private set; }
        public TriggerTimingEnum TriggerTiming { get; private set; }
        public override void InitializeData(BuffDataBase buffData)
        {
            base.InitializeData(buffData);
            if (buffData is FlameData flameData)
            {
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

        public override void OnAdd()
        {
            Debug.Log($"I am on fire, and take {Damage} points of damage and trigger is {TriggerTiming}");
        }

        public override void OnRemove()
        {
        }

        public override void OnTick()
        {
        }
    }
}