using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Common.FSM
{

    public class StateMachine<TKey, TController>
    {
        private readonly Dictionary<TKey, StateBase<TController>> _stateMap = new();
        // private TController _controller;
        private StateBase<TController> _currentState;

        public StateMachine()
        {
            // _controller = controller;
        }

        public void RegisterState(TKey key, StateBase<TController> state) => _stateMap[key] = state;

        public async UniTask ChangeStateAsync(TKey newKey)
        {
            if (_stateMap.TryGetValue(newKey, out var state))
            {
                if (_currentState != null)
                {
                    await _currentState.ExitAsync();
                }
                _currentState = _stateMap[newKey];
                await _currentState.EnterAsync();
            }
        }

        public async UniTask UpdateState()
        {
            if (_currentState != null)
            {
                await _currentState.UpdateAsync();
            }
        }
    }
}