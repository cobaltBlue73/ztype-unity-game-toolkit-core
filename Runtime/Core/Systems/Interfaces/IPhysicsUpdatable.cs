using System;

namespace ZType.Core.Systems.Interfaces
{
    public interface IPhysicsUpdatable: IComparable<IPhysicsUpdatable>
    {
        int PhysicsUpdatePriority { get; }

        void OnPhysicsUpdate();
    }
}