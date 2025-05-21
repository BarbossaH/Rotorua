using System.Collections.Generic;
using Game.Battle;

namespace Game.Character
{
    public interface IIntentPicker
    {
        string PickIntent(BattleContext context, List<EnemyIntent> enemyIntents);
    }

    public class WeightPicker : IIntentPicker
    {
        public string PickIntent(BattleContext context, List<EnemyIntent> enemyIntents)
        {
            throw new System.NotImplementedException();
        }
    }
}