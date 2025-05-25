using System.Collections.Generic;
using Models;
using UnityEngine;

namespace Game.Character.Enemy
{
    public class EnemyIntent
    {
        private List<IntentFilterBase> _iIntentFilters;
        public string IntentId { get; set; }
        public EnemyIntent(EnemyIntentData intentData, List<IntentFilterBase> filters)
        {
            _iIntentFilters = filters;
            IntentId = intentData.ID;
            Debug.Log(_iIntentFilters[0].ClassName + " and its id is " + IntentId);
#if UNITY_EDITOR
            if (filters[0] is CoolDownFilter coolDown)
            {
                Debug.Log(coolDown.CoolDownTurns + " I get the cool down turns count");
            }
#endif
        }
    }
}