using System.Collections.Generic;
using Common;
using Common.Factories;
using Game.Battle;
using Models;
using UnityEngine;

namespace Game.Character.Monster
{
    public class MonsterIntent
    {
        private List<IntentFilterBase> _iIntentFilters;
        private List<string> _buffIds;
        private List<string> _effectIds;
        public string IntentId { get; set; }
        public MonsterIntent(MonsterIntentData intentData, List<IntentFilterBase> filters)
        {
            _iIntentFilters = filters;
            _buffIds = intentData.Buffs ?? new();
            _effectIds = intentData.Effects ?? new();
            IntentId = intentData.ID;
            Debug.Log(_iIntentFilters[0].ClassName + " and its id is " + IntentId);
#if UNITY_EDITOR
            if (filters[0] is CoolDownFilter coolDown)
            {
                Debug.Log(coolDown.CoolDownTurns + " I get the cool down turns count");
            }
#endif
        }

        public void ApplyIntent(BattleContext context)
        {
            //create effect and buff via factories
            ApplyEffect(context);
            ApplyBuff(context);
        }

        private void ApplyEffect(BattleContext context)
        {
            var effectFactory = IoCContainer.GetService<IEffectFactory>();
            var effects = effectFactory.CreateSkillEffects(_effectIds);
            if (effects != null)
            {
                foreach (var e in effects)
                {
                    e.ApplyEffect(null, null);
                }
            }
        }

        private void ApplyBuff(BattleContext context)
        {
            var buffFactory = IoCContainer.GetService<IBuffFactory>();
            var buffs = buffFactory.CreateBuff(_buffIds);
            if (buffs != null)
            {
                foreach (var b in buffs)
                {
                    Debug.Log($"Add buff to the target {b.Name}");
                }
            }
        }
    }
}