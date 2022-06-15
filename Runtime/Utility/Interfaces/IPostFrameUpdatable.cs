using System;

namespace ZType.Utility.Interfaces
{
    public interface IPostFrameUpdatable: IComparable<IPostFrameUpdatable>
    {
        int PostFrameUpdatePriority { get; }

        void OnPostFrameUpdate();
    }
}