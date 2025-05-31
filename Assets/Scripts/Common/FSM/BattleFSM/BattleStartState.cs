using Cysharp.Threading.Tasks;
using Game.Battle;

namespace Common.FSM.BattleFSM
{

    public class BattleStartState : StateBase<IBattleController>
    {
        public BattleStartState(IBattleController controller) : base(controller)
        {
        }

        public override async UniTask OnEnter()
        {
            //what will the view do? 
            //这里应该使用协程或者异步函数，等待界面执行完
            await UniTask.CompletedTask;
        }

        public override async UniTask OnExit()
        {
            await UniTask.Delay(300);
        }

        public override async UniTask OnUpdate()
        {
            await UniTask.CompletedTask;
        }
    }
}