using Cysharp.Threading.Tasks;

namespace Common.FSM
{
    public abstract class StateBase<TController>
    {
        private TController _controller;

        //get current state enum value
        public StateBase(TController controller)
        {
            _controller = controller;
        }
        public abstract UniTask OnEnter();
        public abstract UniTask OnExit();
        public abstract UniTask OnUpdate();
    }
}