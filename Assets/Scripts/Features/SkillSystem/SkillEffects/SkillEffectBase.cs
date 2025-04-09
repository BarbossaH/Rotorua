//each effect has its own method to cast skill effect
public abstract class SkillEffectBase
{
    public string ID;
    public string Name;
    public ElementEnum elementEnum;
    public SkillEffectBase(EffectDataBase effectData)
    {
        ID = effectData.ID;
        Name = effectData.Name;
        elementEnum = effectData.GetElementType();
    }
    public abstract void ApplyEffect();
}