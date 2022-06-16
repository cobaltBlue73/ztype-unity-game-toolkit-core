using UnityEngine;

namespace ZType.Core.Utility.Singleton
{
    public class MonoSingletonPersistent<T> : MonoSingleton<T> where T: MonoSingletonPersistent<T>
    {
        protected override void Awake()
        {
            base.Awake();
            
            if (Instance == this) 
                DontDestroyOnLoad(gameObject);
        }
    }
}