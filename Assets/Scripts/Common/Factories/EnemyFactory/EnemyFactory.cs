using System.Collections.Generic;
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

            Enemy enemy = new(data);
            return enemy;
        }
    }
}