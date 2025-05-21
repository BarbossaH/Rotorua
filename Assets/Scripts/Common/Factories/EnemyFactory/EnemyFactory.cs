using Game.Character;

namespace Common.Factories
{
    public interface IEenemyFactory
    {
        Enemy CreateEnemy(string enemyId);
    }

    [AutoRegisterFactory(typeof(IEenemyFactory))]
    public class EnemyFactory : IEenemyFactory
    {
        public Enemy CreateEnemy(string enemyId)
        {
            throw new System.NotImplementedException();
        }
    }
}