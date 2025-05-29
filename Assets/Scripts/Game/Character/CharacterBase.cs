using Game.CardSystem;
using Models;
using UnityEngine;

namespace Game.Character
{
    public abstract class CharacterBase
    {

        public string ID { get; protected set; }
        public string Name { get; protected set; }
        public float MaxHP { get; protected set; }
        public float CurrentHP { get; protected set; }
        public float Defense { get; set; }
        // public float Attack { get; set; }
        public BuffManager BuffManager { get; private set; }

        public CharacterBase()
        {
            Debug.Log("CharacterBase was called.");
            BuffManager = new();
        }


        public virtual void Heal(float amount)
        {

        }
        public virtual void TakeDamage(float amount)
        {
            float effectiveDamage = Mathf.Max(amount - Defense, 0);
            CurrentHP -= effectiveDamage;
            Defense = Mathf.Max(Defense - amount, 0);
            if (CurrentHP <= 0)
            {
                Die();
            }
        }

        public virtual void AddBuffFromCard(BuffBase buff)
        {
            buff.OnAdd(this);
            BuffManager.AddBuffFromCard(buff);
        }

        public virtual void AddBuffFromRelic() { }

        public virtual void RemoveBuffFromCard(BuffBase buff)
        {
            buff.OnRemove(this);
            BuffManager.RemoveBuffFromCard(buff);
        }

        protected virtual void Die()
        {

        }
    }
}