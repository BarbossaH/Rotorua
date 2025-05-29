using System.Collections.Generic;
using System.Linq;
using Game.Battle;
using Models;

namespace Game.Character.Monster
{
    public class Monster : CharacterBase
    {
        private readonly List<MonsterIntent> _enemyIntentList = new();
        private readonly Dictionary<string, MonsterIntent> _intentDict = new();
        private IIntentPicker _intentPicker;

        private string DefaultIntentId { get; set; }
        public Monster(MonsterData data, List<MonsterIntent> intents, IIntentPicker picker)
        {
            ID = data.ID;
            Name = data.Name;
            MaxHP = data.MaxHP;
            CurrentHP = MaxHP;
            Defense = 0;
            _enemyIntentList = intents;
            _intentDict = intents.ToDictionary(i => i.IntentId);
            _intentPicker = picker;
            DefaultIntentId = data.DefaultIntentID;
        }



        public MonsterIntent GetEnemyIntent(string id) => _intentDict[id];
        public void ApplyIntent(BattleContext context)
        {
            string intentId = _intentPicker.PickIntent(context, _enemyIntentList, DefaultIntentId);
            var intent = _intentDict[intentId];
            intent.ApplyIntent();
        }
    }
}