public abstract class EffectBase
{
    // public EffectTypeEnum effectType;
    protected EffectParameters effectParameters;

    //when creating a concrete subclass, the subclass needs to pass parameters to the constructor of the parent class
    public EffectBase(EffectParameters effect)
    {
        effectParameters = effect;
    }

    public abstract void ApplyEffect(CharacterBase target);
}