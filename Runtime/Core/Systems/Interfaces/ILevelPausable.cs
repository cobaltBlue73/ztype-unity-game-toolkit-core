using System;

namespace ZType.Core.Systems.Interfaces
{
    public interface ILevelPausable : IComparable<ILevelPausable>
    {
        int LevelPausePriority { get; }

        void OnLevelPaused(bool paused);
    }
}