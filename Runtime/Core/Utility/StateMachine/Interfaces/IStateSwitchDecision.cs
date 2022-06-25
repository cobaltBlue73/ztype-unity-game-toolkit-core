using System;

namespace ZType.Core.Utility.StateMachine.Interfaces
{
    public interface IStateSwitchDecision<TNewState>
    {
        bool TrySwitchState(out TNewState newState);
    }
}