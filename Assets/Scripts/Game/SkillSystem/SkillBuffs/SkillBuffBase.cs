using Models;

namespace Game.SkillSystem
{
    /// <summary>
    /// The base class of the buff is designed to provide data for the buff objects, while their interfaces provide behavioral patterns for the buff, effectively acting as a capability component.
    /// </summary>
    #region buff base is for providing the data 
    public abstract class SkillBuffBase
    {
        public string ID { get; private set; }
        public string Name { get; private set; }
        public float Duration { get; private set; }
        public ElementEnum ElementEnum { get; private set; }
        //when buff being added
        public abstract void OnApply();
        //when buff expiring or being dispelled
        public abstract void OnRemove();

        public virtual void InitializeData(BuffDataBase buffData)
        {
            ID = buffData.ID;
            Name = buffData.Name;
            Duration = buffData.Duration;
            ElementEnum = buffData.GetElementType();
        }
    }
    #endregion

    #region interfaces are for behavior of buffs
    public interface IPeriodDamage
    {
        float GetTickDamage();
        void OnTick();
    }

    public interface IPeriodHeal
    {
        float GetTickHeal();
        void OnTick();
    }

    public interface IPeriodControl
    {
        float GetRestTurn();
        void OnTick();
    }
    public interface IMultipleDamage
    {
        float GetMultipleDamage();
        void OnTick();
    }
    #endregion
}