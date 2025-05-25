using System.Collections.Generic;
using Models;

namespace Game.Character.Enemy
{
    public class Enemy : CharacterBase
    {
        private List<EnemyIntent> _enemyIntentList = new();
        private IntentSelector _intentSelector;
        public Enemy(EnemyData data, List<EnemyIntent> intents)
        {
            ID = data.ID;
            Name = data.Name;
            MaxHP = data.MaxHP;
            CurrentHP = MaxHP;
            Defense = 0;
            _enemyIntentList = intents;
        }

        public void SelectIntent()
        {



        }
    }
}