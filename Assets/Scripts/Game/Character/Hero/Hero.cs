
namespace Game.Character
{

    public class Hero : CharacterBase
    {
        public int MaxActionPoint { get; private set; }
        public int CurrentActionPoint { get; private set; }
        public OccupationEnum Occupation { get; }


        public override void Heal(float amount)
        {
            throw new System.NotImplementedException();
        }

        public override void TakeDamager(float amount)
        {
            throw new System.NotImplementedException();
        }

        public void ResetActionPoint()
        {
            CurrentActionPoint = MaxActionPoint;
        }

        public bool UseActionPoint(int amount)
        {
            if (CurrentActionPoint < amount)
            {
                return false;
            }
            CurrentActionPoint -= amount;
            return true;
        }
    }
}
