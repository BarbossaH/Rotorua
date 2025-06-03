using System.Collections.Generic;
using Common.Repositories;
using Game.Character.Monster;
using UnityEngine;

namespace Common.Factories
{
    public interface IMonsterFactory
    {
        Monster CreateMonster(string enemyId);
    }

    [AutoRegisterFactory(typeof(IMonsterFactory))]
    public class MonsterFactory : IMonsterFactory
    {
        public Monster CreateMonster(string enemyId)
        {
            //prepare the basic data
            var repo = IoCContainer.GetService<EnemyRepository>();
            var data = repo.GetById(enemyId);
            Debug.Log(data.Name);

            //prepare intents: create intent objects and inject into the Enemy class
            var intentFactory = IoCContainer.GetService<IIntentFactory>();
            if (data.IntentIDs == null || data.IntentIDs.Count == 0)
            {
                Debug.LogError("Enemy has no intent");
            }
            List<MonsterIntent> intents = new();
            foreach (var i in data.IntentIDs)
            {
                var instance = intentFactory.CreateIntent(i);
                intents.Add(instance);
            }

            //prepare picker: create picker class to pick an intent
            var pickerFactory = IoCContainer.GetService<IPickerFactory>();
            IIntentPicker picker = pickerFactory.CreatePicker(data.IntentPicker);

            //TODO: tomorrow, I will do this create a real monster and its member variables.
            Monster enemy = new(data, intents, picker);
            return enemy;
        }
    }
}