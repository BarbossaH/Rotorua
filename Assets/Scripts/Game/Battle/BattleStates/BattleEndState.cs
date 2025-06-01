using Common.FSM;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Battle
{
    public class BattleEndState : StateBase<BattleController>
    {
        public BattleEndState(BattleController controller) : base(controller)
        {
        }

        public override async UniTask EnterAsync()
        {
            Debug.Log("End state entered");
            await UniTask.CompletedTask;
        }

        public override async UniTask ExitAsync()
        {
            await UniTask.CompletedTask;
        }

        public override async UniTask UpdateAsync()
        {
            await UniTask.CompletedTask;
        }
    }
}