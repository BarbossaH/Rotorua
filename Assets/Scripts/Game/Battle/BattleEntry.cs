namespace Game.Battle
{
    using UnityEngine;
    using Common;
    // using Game.Battle;

    public class BattleEntry : MonoBehaviour
    {
        private ScopeContainer _battleScope;
        // private IBattleManager _battleManager;

        private void Start()
        {
            // 创建新的战斗生命周期容器
            _battleScope = IoCContainer.CreateScope();

            // 在作用域内解析 BattleManager
            // _battleManager = _battleScope.Resolve<IBattleManager>();

            // _battleManager.StartBattle();
        }

        private void OnDestroy()
        {
            // 战斗结束，销毁作用域，释放所有战斗服务
            _battleScope.Dispose();
        }
    }

}
