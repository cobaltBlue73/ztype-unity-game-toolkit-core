using System;
using Cysharp.Threading.Tasks;

namespace ZType.Core.Systems.Interfaces
{
    public interface ILevelInitializable : IComparable<ILevelInitializable>
    {
        int LevelInitializePriority { get; }

        UniTask OnLevelInitializeAsync();
    }
}