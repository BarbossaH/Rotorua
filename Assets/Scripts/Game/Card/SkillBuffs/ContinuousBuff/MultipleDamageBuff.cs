using Common.Defs;
using Game.Character;
using Models;
using UnityEngine;

namespace Game.CardSystem
{
    public class MultipleDamageBuff : BuffBase, IMultipleDamage
    {

        public BuffStatusTypeEnum BuffStatusTypeEnum { get; private set; }

        public float Multiplier { get; private set; }

        public override void InitializeData(BuffDataBase buffData)
        {
            base.InitializeData(buffData);
            if (buffData is MultipleDamageData damageData)
            {
                Multiplier = damageData.Multiplier;
                BuffStatusTypeEnum = damageData.GetBuffStatusTypeEnum();
            }
        }

        public override void OnAdd(CharacterBase character)
        {
            Debug.Log($"Take my {Multiplier} damage and status type is {BuffStatusTypeEnum} and is expire is {IsExpired()} , is permanent is {IsPermanent()}");
        }

        public override void OnRemove(CharacterBase character)
        {

        }

        public override void OnTick()
        {
            if (IsPermanent() || IsExpired()) return;
            CurrentTurn--;
        }


    }
}