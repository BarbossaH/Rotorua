using System;
using Game.SkillSystem;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders.Simulation;

namespace Game.Character
{
    public abstract class CharacterBase
    {
        public string Name { get; }
        public float MaxHealth { get; }
        public float CurrentHealth { get; private set; }
        public float Defense { get; private set; }
        public float Attack { get; }
        public BuffManager BuffManager { get; private set; }
        public abstract void TakeDamager(float amount);
        public abstract void Heal(float amount);
        public CharacterBase()
        {
            BuffManager = new();
        }
        public virtual void TakeDamage(float amount)
        {
            float effectiveDamage = Mathf.Max(amount - Defense, 0);
            CurrentHealth -= effectiveDamage;
            Defense = Mathf.Max(Defense - amount, 0);
            if (CurrentHealth <= 0)
            {
                Die();
            }
        }

        public virtual void AddBuff(SkillBuffBase buff)
        {
            buff.OnAdd(this);
            BuffManager.AddBuff(buff);
        }

        public virtual void RemoveBuff(SkillBuffBase buff)
        {
            buff.OnRemove(this);
            BuffManager.RemoveBuff(buff);
        }

        protected virtual void Die()
        {

        }
    }
}