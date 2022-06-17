using System;

namespace Core.Utility.Singleton
{
    public abstract class LazySingleton<T> where T : new()
    {
        private static readonly Lazy<T> LazyInstance = new(()=> new T());

        public static T Instance => LazyInstance.Value;
    }
}