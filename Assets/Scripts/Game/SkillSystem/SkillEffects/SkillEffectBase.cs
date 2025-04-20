using Models;

namespace Game.SkillSystem
{
    //each effect has its own method to cast skill effect
    public abstract class SkillEffectBase
    {
        public string ID;
        public string Name;
        public ElementEnum elementEnum;
        //TODO:这里不能这么获取effect data，要通过repository去获取
        public virtual void InitializeData(EffectDataBase effectData)
        {
            ID = effectData.ID;
            Name = effectData.Name;
            elementEnum = effectData.GetElementType();
        }
        public abstract void ApplyEffect();
    }
}