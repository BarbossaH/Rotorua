using Game.Character;
using Models;
using UnityEngine;

namespace Game.SkillSystem
{
    /// <summary>
    /// The base class of the buff is designed to provide data for the buff objects, while their interfaces provide behavioral patterns for the buff, effectively acting as a capability component.
    /// </summary>
    #region buff base is for providing the data 
    public abstract class SkillBuffBase
    {
        protected int currentTurn;
        public int CurrentTurn { get; set; }
        public string ID { get; private set; }
        public string Name { get; private set; }
        public float Duration { get; private set; }

        public ElementEnum ElementEnum { get; private set; }
        //when buff being added

        public virtual void InitializeData(BuffDataBase buffData)
        {
            ID = buffData.ID;
            Name = buffData.Name;
            Duration = buffData.Duration;
            ElementEnum = buffData.GetElementType();
        }

        /// <summary>
        /// Called as it is added
        /// </summary>
        public abstract void OnAdd(CharacterBase character);

        /// <summary>
        /// Called as it is removed
        /// </summary>
        public abstract void OnRemove(CharacterBase character);
        /// <summary>
        /// Called on turn refresh
        /// </summary>
        public abstract void OnTick();
        public bool IsPermanent()
        {
            return Duration == -1;
        }
        public bool IsExpired()
        {
            if (IsPermanent()) return false;
            if (Duration > 0) return false;
            return true;
        }

    }
    #endregion

    #region interfaces are for behavior of buffs

    public interface IPeriodDamageBuff
    {
        float GetTickDamage();
        void ApplyDamageEffect();
    }

    public interface IPeriodHeal
    {
        float GetTickHeal();
        void ApplyHealEffect();
    }

    public interface IPeriodControl
    {
        bool IsActionBlocked();
        void ApplyControlEffect();
    }
    public interface IMultipleDamage
    {
        float Multiplier { get; }

    }
    #endregion
}