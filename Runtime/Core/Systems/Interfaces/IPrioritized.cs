using System;

namespace ZType.Core.Systems.Interfaces
{
    public interface IPrioritized: IComparable<IPrioritized>
    {
        int Priority { get; }
    }
}