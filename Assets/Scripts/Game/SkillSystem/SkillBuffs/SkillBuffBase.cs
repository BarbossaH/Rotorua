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
        public int CurrentTurn => currentTurn;
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


        public abstract void OnAdd();
        public abstract void OnRemove();
        public abstract void OnTick();


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
        float GetMultipleDamage();
        void ApplyMultipleDamage();
    }
    #endregion
}