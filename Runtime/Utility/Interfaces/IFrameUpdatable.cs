using System;

namespace ZType.Utility.Interfaces
{
    public interface IFrameUpdatable: IComparable<IFrameUpdatable>
    {
        int FrameUpdatePriority { get; }

        void OnFrameUpdate();
    }
}