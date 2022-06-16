namespace Core.Utility.Singleton
{
    public abstract class SimpleSingleton<T> where T : new()
    {
        protected static T _instance;

        public static T Instance => _instance ??= new T();
    }
}