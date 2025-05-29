using System.Collections.Generic;
using Models;
using UnityEngine;

namespace Game.Character.Monster
{
    public class MonsterIntent
    {
        private List<IntentFilterBase> _iIntentFilters;
        private List<string> _buffIds = new();
        private List<string> _effectIds = new();
        public string IntentId { get; set; }
        public MonsterIntent(MonsterIntentData intentData, List<IntentFilterBase> filters)
        {
            _iIntentFilters = filters;
            _buffIds = intentData.Buffs;
            _effectIds = intentData.Effects;
            IntentId = intentData.ID;
            Debug.Log(_iIntentFilters[0].ClassName + " and its id is " + IntentId);
#if UNITY_EDITOR
            if (filters[0] is CoolDownFilter coolDown)
            {
                Debug.Log(coolDown.CoolDownTurns + " I get the cool down turns count");
            }
#endif
        }

        public void ApplyIntent()
        {
            // var effect = 
        }
    }
}