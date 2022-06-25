using System;
using System.Linq;
using Sirenix.Utilities;
using UnityEngine;
using ZType.Core.Utility.StateMachine.Interfaces;

namespace ZType.Core.Utility.StateMachine
{
    public class StateMachineComponent : MonoBehaviour
    {
        #region Internal

        [System.Serializable]
        private class SwitchStateDecision
        {
            public SwitchStateConditionBehaviour switchCondition;
            public string NewState;
        }

        [System.Serializable]
        private class StateData: IStateSwitchDecision<string>
        {
            public string State;
            public StateBehaviour[] StateBehaviours;
            public SwitchStateDecision[] SwitchDecisions;

            public void OnEnter()
            {
                foreach (var behaviour in StateBehaviours)
                    if (behaviour) behaviour.OnStateEnter();
            }

            public void OnExit()
            {
                foreach (var behaviour in StateBehaviours)
                    if (behaviour) behaviour.OnExitState();
            }

            public void OnUpdate()
            {
                for (var i = 0; i < StateBehaviours.Length; ++i) 
                    StateBehaviours[i]?.OnUpdateState();
            }
            public bool TrySwitchState(out string newState)
            {
                newState = default;

                var validDecision = SwitchDecisions.FirstOrDefault(decision => 
                        decision.switchCondition.ConditionValid());

                if (validDecision == null) return false;
                
                newState = validDecision.NewState;
                return true;

            }
        }
        
        [System.Serializable]
        private struct StateMachineData
        {
            public StateData[] StateData;
        }

        #endregion

        #region Inspector

        [SerializeField] private StateMachineData stateMachineData;

        #endregion

        #region Variables

        private readonly StateMachine<string> _stateMachine = new StateMachine<string>();

        #endregion

        #region Methods

        #region Unity Events

        private void Awake()
        {
            foreach (var stateData in stateMachineData.StateData)
            {
                _stateMachine.SetEnterAction(stateData.State, stateData.OnEnter);
                _stateMachine.SetExitAction(stateData.State, stateData.OnExit);
                _stateMachine.SetUpdateAction(stateData.State, stateData.OnUpdate);
                _stateMachine.SetSwitchDecision(stateData.State, stateData);
            }
        }

        private void Update() => _stateMachine.Update();

        #endregion

        #endregion
    }
}