using UnityEngine;

namespace ZType.Utility.Singleton
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : Component
    {
        #region Static

        public static T Instance
        {
            get
            {
                if (instance) return instance;

                instance = FindObjectOfType<T>();
                if (instance) return instance;

                instance = LoadInstance();
                if (instance) return instance;

                instance = CreateInstance();
                return instance;
            }
        }
        
        protected static T instance;

        private static T LoadInstance()
        {
            var prefab = Resources.Load<GameObject>(nameof(T));
            return !prefab ? null : Instantiate(prefab)
                .GetComponent<T>();
        }

        private static T CreateInstance() =>
            new GameObject(nameof(T), typeof(T))
                .GetComponent<T>();

        #endregion

        #region Event Methods

        protected virtual void Awake()
        {
            if (!Application.isPlaying) return;

            if (instance == null)
            {
                instance = this as T;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        #endregion
    }
}