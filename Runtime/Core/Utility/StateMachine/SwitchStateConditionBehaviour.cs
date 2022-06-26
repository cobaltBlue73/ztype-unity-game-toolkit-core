using UnityEngine;

namespace ZType.Core.Utility.StateMachine
{
    public abstract class SwitchStateConditionBehaviour : MonoBehaviour
    {
        #region Methods

        public abstract bool ConditionValid();

        #endregion
    }
}