using System;

namespace ZType.Core.Utility.StateMachine
{
    public class EnumeratedStateMachine<T> : StateMachine<T> where T :
        struct, IComparable, IConvertible, IFormattable
    {
    }
}