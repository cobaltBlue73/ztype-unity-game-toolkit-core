using System;
using UnityEngine;

namespace ZType.Utility.Singleton
{
    public abstract class ScriptableSingleton<T> : ScriptableObject where T: ScriptableSingleton<T>
    {
        #region Static
        public static T Instance => LazyInstance.Value;

        private static readonly Lazy<T> LazyInstance = new(() =>
        {
            var results = Resources.LoadAll<T>("");
            T instance = null;
            
            if (results.Length > 0)
            {
                instance = results[0];
                
                if (results.Length > 1)
                    Debug.LogWarning($"More that one instance of {typeof(T)} was found.");
            }
            else
            {
                instance = CreateInstance<T>();
                Debug.LogWarning($"A new instance of {typeof(T)} was created because no instance was found", instance);
            }
            
            instance.hideFlags = HideFlags.DontUnloadUnusedAsset;
            Resources.UnloadUnusedAssets();
            
            return instance;
        });
        
        #endregion
        
    }
}