using System.Collections.Generic;
using Common.Repositories;
using Game.Character.Enemy;
using UnityEngine;

namespace Common.Factories
{
    public interface IEnemyFactory
    {
        Enemy CreateEnemy(string enemyId);
    }

    [AutoRegisterFactory(typeof(IEnemyFactory))]
    public class EnemyFactory : IEnemyFactory
    {
        public Enemy CreateEnemy(string enemyId)
        {
            var repo = IoCContainer.GetService<EnemyRepository>();
            var data = repo.GetById(enemyId);
            Debug.Log(data.Name);

            //create intent objects and inject into the Enemy class

            var intentFactory = IoCContainer.GetService<IIntentFactory>();
            if (data.IntentIDs == null || data.IntentIDs.Count == 0)
            {
                Debug.LogError("Enemy has no intent");
            }
            List<EnemyIntent> intents = new();
            foreach (var i in data.IntentIDs)
            {
                var instance = intentFactory.CreateIntent(i);
                intents.Add(instance);
            }


            // IIntentPicker picker = IoCContainer.GetService<IPickerFactory>();

            //TODO: tomorrow, I will do this create a real monster and its member variables.
            Enemy enemy = new(data, intents);
            return enemy;
        }
    }
}