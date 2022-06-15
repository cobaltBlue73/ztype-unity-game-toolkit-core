using UnityEngine;

namespace ZType.Utility.Singleton
{
    public class MonoSingletonPersistent<T> : MonoSingleton<T> where T: Component
    {
        protected override void Awake()
        {
            base.Awake();
            if (instance != this) return;
            DontDestroyOnLoad(gameObject);
        }
    }
}