using Models;
using UnityEngine;

namespace Game.SkillSystem
{
    public class MultipleDamageBuff : SkillBuffBase, IMultipleDamage
    {
        public float Multiplier { get; private set; }
        public BuffStatusTypeEnum BuffStatusTypeEnum { get; private set; }
        public override void InitializeData(BuffDataBase buffData)
        {
            base.InitializeData(buffData);
            if (buffData is MultipleDamageData damageData)
            {
                Multiplier = damageData.Multiplier;
                BuffStatusTypeEnum = damageData.GetBuffStatusTypeEnum();
            }
        }
        public void ApplyMultipleDamage()
        {
            throw new System.NotImplementedException();
        }

        public float GetMultipleDamage()
        {
            throw new System.NotImplementedException();
        }

        public override void OnAdd()
        {
            Debug.Log($"Take my {Multiplier} damage and status type is {BuffStatusTypeEnum}");
        }

        public override void OnRemove()
        {
            throw new System.NotImplementedException();
        }

        public override void OnTick()
        {
            throw new System.NotImplementedException();
        }
    }
}