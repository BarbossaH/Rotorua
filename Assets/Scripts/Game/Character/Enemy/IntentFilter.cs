using Game.Battle;
using Models;

namespace Game.Character.Monster
{
    public abstract class IntentFilterBase
    {
        public string FilterID { get; set; }
        public string ClassName { get; set; }
        public abstract bool Check(BattleContext context);

        public virtual void InitializeData(FilterDataBase dataBase)
        {
            FilterID = dataBase.ID;
            ClassName = dataBase.ClassName;
        }
    }

    public class CoolDownFilter : IntentFilterBase
    {
        public int CoolDownTurns { get; set; }
        public override void InitializeData(FilterDataBase dataBase)
        {
            base.InitializeData(dataBase);
            if (dataBase is CoolDownFilterData data)
            {
                CoolDownTurns = data.CoolDownTurns;
            }
        }
        public override bool Check(BattleContext context)
        {
            return false;
        }
    }

    public class FirstTurnFilter : IntentFilterBase
    {
        public override bool Check(BattleContext context)
        {
            return false;
        }
    }

    public class NotRepeatedFilter : IntentFilterBase
    {
        public override bool Check(BattleContext context)
        {
            return false;
        }
    }
}