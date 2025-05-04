
using Models;

namespace Game.Character
{
    /// <summary>
    /// this class is for managing the logic of hero, and the view of hero is managed by hero view class
    /// </summary>
    public class Hero : CharacterBase
    {
        public int MaxEnergy { get; private set; }
        public int CurrentEnergy { get; private set; }
        public OccupationEnum Occupation { get; }

        public Hero(HeroData heroData) : base()
        {
            //actually this is wrong because we also need to load the hero save data, so that we can get the ultimate value.
            ID = heroData.ID;
            Name = heroData.Name;
            MaxHP = heroData.MaxHP;
            CurrentHP = MaxHP;
            MaxEnergy = heroData.Energy;
            CurrentEnergy = MaxEnergy;
        }
        public override void Heal(float amount)
        {
            throw new System.NotImplementedException();
        }

        public override void TakeDamager(float amount)
        {
            throw new System.NotImplementedException();
        }

        public void DrawCard() { }
        public void ResetActionPoint()
        {
            CurrentEnergy = MaxEnergy;
        }

        public bool UseActionPoint(int amount)
        {
            if (CurrentEnergy < amount)
            {
                return false;
            }
            CurrentEnergy -= amount;
            return true;
        }
    }
}
