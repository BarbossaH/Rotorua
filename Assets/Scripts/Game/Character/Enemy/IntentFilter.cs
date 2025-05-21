using Game.Battle;

namespace Game.Character
{
    public interface IItentFilter
    {
        bool Check(BattleContext context);
    }
    public class TargetHpBelowFilter { }
}