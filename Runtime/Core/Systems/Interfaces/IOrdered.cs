using System;

namespace ZType.Core.Systems.Interfaces
{
    public interface IOrdered: IComparable<IOrdered>
    {
        int Order { get; }
    }
}