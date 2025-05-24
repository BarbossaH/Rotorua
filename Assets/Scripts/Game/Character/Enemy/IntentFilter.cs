using Game.Battle;

namespace Game.Character.Enemy
{
    public interface IItentFilter
    {
        bool Check(BattleContext context);
    }
    public class TargetHpBelowFilter { }
}