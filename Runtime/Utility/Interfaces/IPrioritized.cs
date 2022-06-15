using System;

namespace ZType.Utility.Interfaces
{
    public interface IPrioritized: IComparable<IPrioritized>
    {
        int Priority { get; }
    }
}