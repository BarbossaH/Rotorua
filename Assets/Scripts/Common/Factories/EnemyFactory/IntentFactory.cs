using Game.Character;

namespace Common.Factories
{
    public interface IIntentFactory
    {
        EnemyIntent CreateIntent(string intentId);
    }

    [AutoRegisterFactory(typeof(IIntentFactory))]
    public class IntentFactory : IIntentFactory
    {
        public EnemyIntent CreateIntent(string intentId)
        {
            throw new System.NotImplementedException();
        }
    }
}