using Cysharp.Threading.Tasks;

namespace Game.Battle
{
    public interface IBattleController
    {
        // 初始化战斗，创建英雄、副本单位、卡牌、圣物等
        // UniTask InitializeBattle(BattleConfig config);

        // 正式开始战斗（可以触发首次回合开始逻辑）
        UniTask StartBattleAsync();

        // 回合控制
        UniTask StartTurnAsync();
        UniTask EndTurnAsync();

        // 玩家回合控制
        UniTask StartPlayerTurnAsync();
        UniTask EndPlayerTurnAsync();

        // 敌人回合控制
        UniTask StartEnemyTurnAsync();
        UniTask EndEnemyTurnAsync();
        // 战斗结束，胜利或失败
        UniTask EndBattleAsync();
    }
}