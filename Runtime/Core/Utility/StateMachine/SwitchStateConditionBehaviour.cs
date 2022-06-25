using UnityEngine;
using ZType.Core.Utility.StateMachine.Interfaces;

namespace ZType.Core.Utility.StateMachine
{
    public abstract class SwitchStateConditionBehaviour : MonoBehaviour
    {
        #region Methods

        public abstract bool ConditionValid();

        #endregion
    }
}