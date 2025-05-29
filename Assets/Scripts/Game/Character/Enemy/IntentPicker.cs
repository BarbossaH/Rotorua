using System.Collections.Generic;
using Game.Battle;
using UnityEngine;

namespace Game.Character.Monster
{
    public interface IIntentPicker
    {
        string PickIntent(BattleContext context, List<MonsterIntent> enemyIntents, string defaultIntentId);
    }

    public class WeightPicker : IIntentPicker
    {
        public string PickIntent(BattleContext context, List<MonsterIntent> enemyIntents, string defaultIntentId)
        {
            Debug.Log($"{typeof(WeightPicker).Name} is create for now---------------.");
            return defaultIntentId;
        }
    }
}