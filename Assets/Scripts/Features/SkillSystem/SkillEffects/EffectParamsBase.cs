//思路：通过skill data创建技能类，通过技能类创建effect类，effect是通过有一个effect data参数去创建的，effect继承一个基类，我需要获得对应effect data里面的字段和数值。问题是我在施放技能的时候知道是哪个技能和effect吗？
public class EffectBase
{

    public EffectTypeEnum effectType { get; protected set; }
    public TriggerConditionEnum triggerCondition { get; protected set; }
    public TargetTypeEnum targetType { get; protected set; }
    public EffectBase(EffectData effectData)
    {
        effectType = effectData.GetEffectType();
        triggerCondition = effectData.GetTriggerConditionEnum();
        targetType = effectData.GetTargetEnum();
    }
}