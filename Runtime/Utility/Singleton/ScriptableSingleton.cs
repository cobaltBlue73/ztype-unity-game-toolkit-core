using UnityEngine;

namespace ZType.Utility.Singleton
{
    public abstract class ScriptableSingleton<T> : ScriptableObject where T: ScriptableObject
    {
        #region Static

        public static T Instance
        {
            get
            {
                if (!_instance)
                    _instance = Resources.Load<T>(nameof(T));
                return _instance;
            }
        }
        
        private static T _instance;
        
        #endregion

        
    }
}