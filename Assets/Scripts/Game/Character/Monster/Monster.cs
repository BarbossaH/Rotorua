using System.Collections.Generic;
using System.Linq;
using Game.Battle;
using Models;
using UnityEngine;

namespace Game.Character.Monster
{
    public class Monster : CharacterBase
    {
        // private readonly List<MonsterIntent> _enemyIntentList;
        private readonly List<string> _intentIds = new();
        private readonly Dictionary<string, MonsterIntent> _intentDict = new();
        private IIntentPicker _intentPicker;

        private string DefaultIntentId { get; set; }
        public Monster(MonsterData data, List<MonsterIntent> intents, IIntentPicker picker)
        {
            ID = data.ID;
            Name = data.Name;
            MaxHP = data.MaxHP;
            CurrentHP = MaxHP;
            _intentIds = data.IntentIDs ?? new();
            Defense = 0;
            // _enemyIntentList = intents ?? new();
            _intentDict = intents.ToDictionary(i => i.IntentId);
            _intentPicker = picker;
            DefaultIntentId = data.DefaultIntentID;
        }



        public MonsterIntent GetEnemyIntent(string id) => _intentDict[id];
        public void ApplyIntent(BattleContext context)
        {
            string intentId = _intentPicker.PickIntent(context, _intentIds, DefaultIntentId);
            Debug.Log($"The picker has pick {intentId} as the intend id to be casted ");
            var intent = _intentDict[intentId];
            intent.ApplyIntent(context);
        }
    }
}