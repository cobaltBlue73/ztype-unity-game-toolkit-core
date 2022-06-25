using System;
using System.Linq;
using UnityEngine;

namespace ZType.Core.Utility.Singleton
{
    public class ScriptableSingletonSettingsAttribute : Attribute
    {
        public string ResourcePathName { get; }
        public bool CreateFallBack { get; }

        public ScriptableSingletonSettingsAttribute(string resourcePathName, bool createFallBack = false)
        {
            ResourcePathName = resourcePathName;
            CreateFallBack = createFallBack;
        }
    }
    
    public abstract class ScriptableSingleton<T> : ScriptableObject where T: ScriptableSingleton<T>
    {
        #region Static
        public static T Instance => LazyInstance.Value;

        public static bool HasInstance => LazyInstance.IsValueCreated;

        private static readonly Lazy<T> LazyInstance = new(() =>
        {
            var attrSettings = GetSettingsAttribute();
            
            var instance = Resources.Load<T>(attrSettings.ResourcePathName);

            if(!instance)
            {
                if(!attrSettings.CreateFallBack) throw new Exception($"No instance of {typeof(T)} could be found.");
                
                instance = CreateInstance<T>();
                Debug.LogWarning($"No instance of {typeof(T)} could be found, creating new instance.");
            }
            
            instance.hideFlags = HideFlags.DontUnloadUnusedAsset;
            Resources.UnloadUnusedAssets();
            
            return instance;
        });
        
        private static ScriptableSingletonSettingsAttribute GetSettingsAttribute() =>
            typeof(T).GetCustomAttributes(typeof(ScriptableSingletonSettingsAttribute), true)
                .FirstOrDefault() as ScriptableSingletonSettingsAttribute;
        
        #endregion
    }
}