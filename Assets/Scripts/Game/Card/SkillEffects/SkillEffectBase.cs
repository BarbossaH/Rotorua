using Common.Defs;
using Game.Character;
using Models;

namespace Game.CardSystem
{
    //each effect has its own method to cast skill effect
    public abstract class EffectBase
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
        //TODO:下面的参数要修改，改成BattleContext
        public abstract void ApplyEffect(CharacterBase caster, CharacterBase target);
    }
}