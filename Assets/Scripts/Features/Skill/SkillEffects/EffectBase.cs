public abstract class EffectBase
{
    public string EffectName { get; protected set; }
    public float Value { get; protected set; }
    public int RemainingTurns { get; protected set; }

    public abstract void ApplyEffect(CharacterBase character);
}