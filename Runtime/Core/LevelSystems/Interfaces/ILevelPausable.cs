using System;

namespace ZType.Core.LevelSystems.Interfaces
{
    public interface ILevelPausable: IComparable<ILevelPausable>
    {
        int LevelPausePriority { get; }

        void OnLevelPaused(bool paused);
    }
}