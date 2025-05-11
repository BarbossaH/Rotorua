namespace Game.Battle
{
    public interface IBattleController
    {
        // 初始化战斗，创建英雄、副本单位、卡牌、圣物等
        // void InitializeBattle(BattleConfig config);

        // 正式开始战斗（可以触发首次回合开始逻辑）
        void StartBattle();

        // 回合控制
        void StartTurn();
        void EndTurn();

        // 玩家回合控制
        void StartPlayerTurn();
        void EndPlayerTurn();

        // 敌人回合控制
        void StartEnemyTurn();
        void EndEnemyTurn();

        // 完整一回合结束（含玩家 + 敌人）
        void OnRoundEnd();

        // 战斗结束，胜利或失败
        // void EndBattle(BattleResult result);
    }
}