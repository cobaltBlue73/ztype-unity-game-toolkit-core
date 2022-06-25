using System;

namespace ZType.Core.Utility.Singleton
{
    public abstract class Singleton<T> where T : new()
    {
        private static readonly Lazy<T> LazyInstance = new(()=> new T());

        public static T Instance => LazyInstance.Value;
    }
}