using System;
using System.Collections.Generic;
using ZType.Core.Utility.StateMachine.Interfaces;

namespace ZType.Core.Utility.StateMachine
{
    public class StateMachine<T> where T : IComparable, IConvertible
    {
        #region Inner

        private class StateBehaviour
        {
            public Action EnterAction;
            public Action ExitAction;
            public IStateSwitchDecision<T> SwitchDecision;
            public Action UpdateAction;
        }

        #endregion

        #region Constructor

        public StateMachine()
        {
            _states = new Dictionary<T, StateBehaviour>();
        }

        public StateMachine(T initialState)
        {
            _states = new Dictionary<T, StateBehaviour>();
            CurrentState = initialState;
        }

        #endregion

        #region Variables

        private readonly Dictionary<T, StateBehaviour> _states;
        private T _currentState;
        private StateBehaviour _curBehaviour;

        #endregion

        #region Properties

        public T CurrentState
        {
            get => _currentState;
            set
            {
                if (_currentState.Equals(value)) return;

                _curBehaviour?.ExitAction?.Invoke();

                PreviousState = _currentState;
                _currentState = value;
                _curBehaviour = _states[value];

                _curBehaviour.EnterAction?.Invoke();
            }
        }

        public T PreviousState { get; private set; }

        #endregion

        #region Methods

        public void Update()
        {
            if (_curBehaviour == null) return;

            _curBehaviour.UpdateAction?.Invoke();

            if (_curBehaviour.SwitchDecision == null ||
                !_curBehaviour.SwitchDecision.TrySwitchState(out var newState)) return;

            CurrentState = newState;
        }

        private StateBehaviour GetStateBehaviour(T state)
        {
            if (_states.TryGetValue(state, out var behaviour))
                return behaviour;

            behaviour = new StateBehaviour();
            _states.Add(state, behaviour);

            return behaviour;
        }

        public void SetEnterAction(T state, Action enterAction)
        {
            GetStateBehaviour(state).EnterAction = enterAction;
        }

        public void SetExitAction(T state, Action exitAction)
        {
            GetStateBehaviour(state).ExitAction = exitAction;
        }

        public void SetUpdateAction(T state, Action updateAction)
        {
            GetStateBehaviour(state).UpdateAction = updateAction;
        }

        public void SetSwitchDecision(T state, IStateSwitchDecision<T> switchDecision)
        {
            GetStateBehaviour(state).SwitchDecision = switchDecision;
        }

        #endregion
    }
}