using UnityEngine;

namespace ZType.Core.Utility.Singleton
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        #region Static

        public static T Instance
        {
            get
            {
                if (_instance) return _instance;

                _instance = FindObjectOfType<T>();
                if (_instance) return _instance;

                _instance = LoadInstance();
                if (_instance) return _instance;

                _instance = CreateInstance();
                return _instance;
            }
        }
        
        private static T _instance;

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
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        #endregion
    }
}