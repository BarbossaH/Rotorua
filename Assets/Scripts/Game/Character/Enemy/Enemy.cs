using System.Collections.Generic;
using Models;

namespace Game.Character.Enemy
{
    public class Enemy : CharacterBase
    {
        private List<EnemyIntent> _enemyIntentList = new();
        private IntentSelector _intentSelector;
        public Enemy(EnemyData data)
        {
            ID = data.ID;
            Name = data.Name;
            MaxHP = data.MaxHP;
            CurrentHP = MaxHP;
            Defense = 0;

        }

        public void SelectIntent()
        {
            //   var intentFactory = IoCContainer.GetService<IIntentFactory>();
            // var intentIds = data.IntentIDs;
            // if (data.IntentIDs == null || data.IntentIDs.Count == 0)
            // {
            //     Debug.LogError("Enemy has no intent");
            // }
            // List<EnemyIntent> intents = new();
            // foreach (var i in data.IntentIDs)
            // {
            //     var instance = intentFactory.CreateIntent(i);
            //     intents.Add(instance);
            // }
            // IIntentPicker picker = IoCContainer.GetService<IPickerFactory>();


        }
    }
}