using System;
using Cysharp.Threading.Tasks;

namespace ZType.Core.Systems.Interfaces
{
    public interface ILevelStartable : IComparable<ILevelStartable>
    {
        int LevelStartPriority { get; }

        UniTask OnLevelStartAsync();
    }
}