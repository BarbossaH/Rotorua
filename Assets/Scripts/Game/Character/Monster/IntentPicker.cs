using System.Collections.Generic;
using Game.Battle;
using UnityEngine;

namespace Game.Character.Monster
{
    public interface IIntentPicker
    {
        string PickIntent(BattleContext context, List<string> intentIds, string defaultIntentId);
    }

    public class WeightPicker : IIntentPicker
    {
        public string PickIntent(BattleContext context, List<string> intentIds, string defaultIntentId)
        {
            Debug.Log($"{typeof(WeightPicker).Name} is create for now---------------.");
            return defaultIntentId;
        }
    }

    public class RandomPicker : IIntentPicker
    {
        public string PickIntent(BattleContext context, List<string> intentIds, string defaultIntentId)
        {
            if (intentIds != null && intentIds.Count > 0)
            {
                int index = Random.Range(0, intentIds.Count);
                return intentIds[index];
            }
            return defaultIntentId;
        }
    }
}