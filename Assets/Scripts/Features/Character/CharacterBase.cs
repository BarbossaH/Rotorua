public abstract class CharacterBase
{
    public string CharacterName { get; protected set; }
    public float Health { get; protected set; }
    public float Armor { get; protected set; }

    public abstract void TakeDamager(float amount);
    public abstract void Heal(float amount);
}