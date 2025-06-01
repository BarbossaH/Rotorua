using Cysharp.Threading.Tasks;

namespace Common.FSM
{
    public abstract class StateBase<TController>
    {
        protected TController _controller;

        //get current state enum value
        public StateBase(TController controller)
        {
            _controller = controller;
        }
        public abstract UniTask EnterAsync();
        public abstract UniTask ExitAsync();
        public abstract UniTask UpdateAsync();
    }
}