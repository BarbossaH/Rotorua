using Common;
using Common.Defs;
using Common.FSM;
using Cysharp.Threading.Tasks;

namespace Game.Battle
{
    [AutoRegisterController(typeof(IBattleController))]
    public class BattleController : IBattleController
    {
        public StateMachine<BattleStateEnum, BattleController> StateMachine { get; private set; }
        public IBattleView BattleView { get; private set; }

        public void InitializeBattle(IBattleView battleView)
        {
            BattleView = battleView;
            StateMachine = new StateMachine<BattleStateEnum, BattleController>();

            //register states
            StateMachine.RegisterState(BattleStateEnum.BattleStart, new BattleStartState(this));
            StateMachine.RegisterState(BattleStateEnum.BattleEnd, new BattleEndState(this));
        }
        public async UniTask StartBattleAsync()
        {
            await StateMachine.ChangeStateAsync(BattleStateEnum.BattleStart);
            await UniTask.CompletedTask;
        }
        public async UniTask StartTurnAsync()
        {
            await StateMachine.ChangeStateAsync(BattleStateEnum.BattleEnd);
            await UniTask.CompletedTask;
        }
        public UniTask StartEnemyTurnAsync()
        {
            throw new System.NotImplementedException();
        }

        public UniTask StartPlayerTurnAsync()
        {
            throw new System.NotImplementedException();
        }

        public UniTask EndEnemyTurnAsync()
        {
            throw new System.NotImplementedException();
        }

        public UniTask EndPlayerTurnAsync()
        {
            throw new System.NotImplementedException();
        }

        public UniTask EndTurnAsync()
        {
            throw new System.NotImplementedException();
        }
        public UniTask EndBattleAsync()
        {
            throw new System.NotImplementedException();
        }




    }
}