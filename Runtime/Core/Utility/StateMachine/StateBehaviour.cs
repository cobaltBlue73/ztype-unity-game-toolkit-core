using UnityEngine;

namespace ZType.Core.Utility.StateMachine
{
    public abstract class StateBehaviour : MonoBehaviour
    {
        public abstract void OnStateEnter();

        public abstract void OnExitState();

        public abstract void OnUpdateState();
    }
}