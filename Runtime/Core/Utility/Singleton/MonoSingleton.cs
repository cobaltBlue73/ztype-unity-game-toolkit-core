using System;
using System.Linq;
using UnityEngine;

namespace ZType.Core.Utility.Singleton
{
    public class DontDestroySingletonOnLoadAttribute : Attribute { }
    
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        #region Static

        public static T Instance
        {
            get
            {
                if (_instance) return _instance;

                _instance = FindObjectOfType<T>();
                
                if (!_instance)
                {
                    _instance = LoadInstance();
                    
                    if(!_instance)
                        _instance = CreateInstance();
                }
                
                if(DontDestroySingletonOnLoad())
                   DontDestroyOnLoad(_instance.gameObject);

                return _instance;
            }
        }
        
        private static T _instance;

        private static bool DontDestroySingletonOnLoad() => 
            typeof(T).GetCustomAttributes(typeof(DontDestroySingletonOnLoadAttribute), true).Any();

        private static T LoadInstance()
        {
            var prefab = Resources.Load<GameObject>(nameof(T));
            return !prefab ? null : Instantiate(prefab)
                .GetComponent<T>();
        }

        private static T CreateInstance()
        {
            var newInstance = new GameObject(nameof(T), typeof(T))
                .GetComponent<T>();
            
            Debug.LogWarning($"A new instance of {typeof(T)} was created because no instance was found", newInstance);
            
            return newInstance;
        }

        #endregion

        #region Event Methods

        protected virtual void Awake()
        {
            if (!Application.isPlaying) return;

            if (_instance == null)
            {
                _instance = this as T;

                if (DontDestroySingletonOnLoad())
                    DontDestroyOnLoad(gameObject);
            }
            else if (_instance != this)
            {
                Debug.LogWarning($"Another instance of {typeof(T)} was found and destroyed", this);
                Destroy(gameObject);
            }
        }

        #endregion
    }
}