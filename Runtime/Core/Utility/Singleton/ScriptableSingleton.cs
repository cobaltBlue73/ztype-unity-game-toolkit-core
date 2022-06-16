using UnityEngine;

namespace ZType.Utility.Singleton
{
    public abstract class ScriptableSingleton<T> : ScriptableObject where T: ScriptableSingleton<T>
    {
        #region Static

        public static T Instance
        {
            get
            
            {
                if (!_instance)
                    _instance = Resources.Load<T>(nameof(T));

                if (_instance) return _instance;
                
                _instance = CreateInstance<T>();
                Debug.LogWarning($"A new instance of {typeof(T)} was created because no instance was found", _instance);
                
                return _instance;
            }
        }
        
        private static T _instance;
        
        #endregion

        
    }
}