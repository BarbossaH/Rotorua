using System;
using System.Collections.Generic;
using Game.Battle;

namespace Common.FSM
{

    public class StateMachine<TKey, TController>
    {
        private readonly Dictionary<TKey, StateBase<TController>> _states = new();
        // private TController _controller;
        private StateBase<TController> _currentState;

        public StateMachine()
        {
            // _controller = controller;
        }

        public void AddState(TKey key, StateBase<TController> state) => _states[key] = state;

        public void ChangeState(TKey newKey)
        {
            _currentState?.OnExit();
            _currentState = _states[newKey];
            _currentState.OnEnter();
        }
    }
}