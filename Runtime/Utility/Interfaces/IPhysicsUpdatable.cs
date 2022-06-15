using System;

namespace ZType.Utility.Interfaces
{
    public interface IPhysicsUpdatable: IComparable<IPhysicsUpdatable>
    {
        int PhysicsUpdatePriority { get; }

        void OnPhysicsUpdate();
    }
}