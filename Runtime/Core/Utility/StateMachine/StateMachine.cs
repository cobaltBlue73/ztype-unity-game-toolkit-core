using System;
using System.Collections.Generic;
using UniRx;

namespace ZType.Core.Utility.StateMachine
{
    public class StateMachine<T>: IDisposable where T : struct, IConvertible
    {
        private class StateBehaviour: IDisposable
        {
            public readonly Subject<T> OnEnter = new();
            public readonly Subject<T> OnExit = new();
            public readonly Subject<T> OnUpdate = new();

            public void Dispose()
            {
                OnEnter?.Dispose();
                OnExit?.Dispose();
                OnUpdate?.Dispose();
            }
        }

        private readonly Dictionary<int, StateBehaviour> _states = new();

        private T _state;
        private StateBehaviour _curBehaviour;
        
        public StateMachine() => InitStates();
        
        public StateMachine(T initialState)
        {
            InitStates();
            State = initialState;
        }

        private void InitStates()
        {
            foreach (var state in Enum.GetValues(typeof(T)))
                _states.Add(Convert.ToInt32(state), new StateBehaviour());
        }

        public T State
        {
            get => _state;
            set
            {
                if (_state.Equals(value)) return;

                // _curBehaviour?.OnExit.OnNext(_state);

                PreviousState = _state;
                _state = value;
                _curBehaviour = _states[Convert.ToInt32(value)];
                
                // _curBehaviour.OnEnter.OnNext(_state);
            }
        }

        public T PreviousState { get; private set; }

        // public void Update() => _curBehaviour?.OnUpdate.OnNext(_state);

        // public IObservable<T> GetEnterStateAsObservable(T state) =>   
            // _states[Convert.ToInt32(state)].OnEnter.AsObservable();
        
        // public IObservable<T> GetExitStateAsObservable(T state) =>   
            // _states[Convert.ToInt32(state)].OnExit.AsObservable();
        
        // public IObservable<T> GetUpdateStateAsObservable(T state) => 
            // _states[Convert.ToInt32(state)].OnUpdate.AsObservable();

        public void Dispose()
        {
            foreach (var stateBehaviour in _states.Values)
            {
                stateBehaviour.Dispose();
            }
        }
    }
}

