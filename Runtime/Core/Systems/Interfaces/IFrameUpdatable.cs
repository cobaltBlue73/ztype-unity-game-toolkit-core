using System;

namespace ZType.Core.Systems.Interfaces
{
    public interface IFrameUpdatable : IComparable<IFrameUpdatable>
    {
        int FrameUpdatePriority { get; }

        void OnFrameUpdate();
    }
}