
using System.Collections.Generic;
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
        public OccupationEnum Occupation { get; private set; }

        //if there is no save relic id list, then assign the original relic to it.
        public List<string> RelicIDs = new();

        public Hero(HeroData heroData, HeroSaveData heroSaveData) : base()
        {
            //actually this is wrong because we also need to load the hero save data, so that we can get the ultimate value.
            ID = heroData.ID;
            Name = heroData.Name;
            Occupation = heroData.GetOccupationEnum();
            InitHP(heroData.MaxHP, heroSaveData?.StatusSaveData?.MaxHp, heroSaveData?.StatusSaveData?.CurrentHP);
            InitEnergy(heroData.Energy, heroSaveData?.StatusSaveData?.MaxEnergy, heroSaveData?.StatusSaveData?.CurrentEnergy);

        }
        private void InitHP(float originalHp, float? saveHp, float? current)
        {
            if (saveHp != null)
            {
                MaxHP = (float)(originalHp < saveHp ? saveHp : originalHp);

            }
            CurrentHP = (float)(current != null ? current : MaxHP);
        }
        private void InitEnergy(int original, int? save, int? current)
        {
            if (save != null)
            {
                MaxEnergy = (int)(original < save ? save : original);

            }
            CurrentEnergy = (int)(current != null ? current : MaxEnergy); ;
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
