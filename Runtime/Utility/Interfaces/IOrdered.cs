using System;

namespace ZType.Utility.Interfaces
{
    public interface IOrdered: IComparable<IOrdered>
    {
        int Order { get; }
    }
}