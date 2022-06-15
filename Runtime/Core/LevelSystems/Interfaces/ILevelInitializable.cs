using System;
using Cysharp.Threading.Tasks;

namespace ZType.Core.LevelSystems.Interfaces
{
    public interface ILevelInitializable: IComparable<ILevelInitializable>
    {
        int LevelInitializePriority { get; }

        UniTask OnLevelInitializeAsync();
    }
}