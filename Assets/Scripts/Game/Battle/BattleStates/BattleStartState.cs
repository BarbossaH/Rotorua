using Common.FSM;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Battle
{

    public class BattleStartState : StateBase<BattleController>
    {
        public BattleStartState(BattleController controller) : base(controller)
        {
        }

        public override async UniTask EnterAsync()
        {
            Debug.Log("Battle start state entered 1");

            await _controller.BattleView.PlayBattleStartAnimationAsync();
            //what will the view do? 
            //这里应该使用协程或者异步函数，等待界面执行完
            // await UniTask.CompletedTask;
            await _controller.StartTurnAsync();
            Debug.Log("Battle start state entered 2");
        }

        public override async UniTask ExitAsync()
        {
            await UniTask.CompletedTask;
            Debug.Log("Battle start state exit");
        }

        public override async UniTask UpdateAsync()
        {
            await UniTask.CompletedTask;
        }
    }
}