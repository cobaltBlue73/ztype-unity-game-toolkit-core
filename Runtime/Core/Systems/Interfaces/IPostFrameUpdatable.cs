using System;

namespace ZType.Core.Systems.Interfaces
{
    public interface IPostFrameUpdatable: IComparable<IPostFrameUpdatable>
    {
        int PostFrameUpdatePriority { get; }

        void OnPostFrameUpdate();
    }
}