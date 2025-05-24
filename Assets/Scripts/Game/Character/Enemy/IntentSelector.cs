using System.Collections.Generic;
using Game.Battle;

namespace Game.Character.Enemy
{
    public class IntentSelector
    {
        private IIntentPicker _picker;

        public IntentSelector(IIntentPicker picker)
        {
            _picker = picker;
        }

        //TODO:这里需要返回值，现在先用void测试一下
        public void PickerIntent(BattleContext context, List<EnemyIntent> intents)
        {
            _picker.PickIntent(context, intents);
        }
    }
}