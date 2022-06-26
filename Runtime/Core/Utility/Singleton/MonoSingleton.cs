using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ZType.Core.Utility.Singleton
{
    public class MonoSingletonSettingsAttribute : Attribute
    {
        public enum SourceType
        {
            Scene,
            Resource,
            Create
        }

        public MonoSingletonSettingsAttribute(SourceType source, string sourcePath, bool isPersistent = false,
            bool createFallBack = false)
        {
            Source = source;
            SourcePath = sourcePath;
            IsPersistent = isPersistent;
            CreateFallBack = createFallBack;
        }

        public SourceType Source { get; }
        public string SourcePath { get; }
        public bool IsPersistent { get; }
        public bool CreateFallBack { get; }
    }

    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        #region Event Methods

        protected virtual void Awake()
        {
            if (!Application.isPlaying) return;

            if (_instance == null)
            {
                _instance = this as T;

                if (IsPersistent())
                    DontDestroyOnLoad(gameObject);
            }
            else if (_instance != this)
            {
                Debug.LogWarning($"Another instance of {typeof(T)} was found and destroyed", this);
                Destroy(gameObject);
            }
        }

        #endregion

        #region Static

        public static T Instance
        {
            get
            {
                if (_instance) return _instance;

                var settingsAttr = GetSettingsAttribute();

                if (settingsAttr == null)
                {
                    _instance = FindObjectOfType<T>();
                    if (!_instance) throw new Exception($"No instance of {typeof(T)} could not be found");
                    return _instance;
                }

                switch (settingsAttr.Source)
                {
                    case MonoSingletonSettingsAttribute.SourceType.Scene:
                        _instance = GetFromScene(settingsAttr.SourcePath);
                        break;
                    case MonoSingletonSettingsAttribute.SourceType.Resource:
                        _instance = GetFromResources(settingsAttr.SourcePath);
                        break;
                    case MonoSingletonSettingsAttribute.SourceType.Create:
                        _instance = CreateInstance();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (!_instance)
                {
                    if (settingsAttr.CreateFallBack)
                    {
                        Debug.LogWarning($"No instance of {typeof(T)} could be found, creating new instance.");
                        _instance = CreateInstance();
                    }

                    if (!_instance) throw new Exception($"No instance of {typeof(T)} could be found.");
                }

                if (_instance && settingsAttr.IsPersistent)
                    DontDestroyOnLoad(_instance.gameObject);

                return _instance;
            }
        }

        public static bool HasInstance => _instance;

        private static T _instance;

        private static MonoSingletonSettingsAttribute GetSettingsAttribute()
        {
            return typeof(T).GetCustomAttributes(typeof(MonoSingletonSettingsAttribute), true)
                .FirstOrDefault() as MonoSingletonSettingsAttribute;
        }

        private static T GetFromScene(string scenePath)
        {
            var scene = SceneManager.GetSceneByName(scenePath);
            if (!scene.isLoaded)
                SceneManager.LoadScene(scene.buildIndex, LoadSceneMode.Additive);

            var activeScene = SceneManager.GetActiveScene();
            if (activeScene != scene)
                SceneManager.SetActiveScene(scene);

            var instance = FindObjectOfType<T>();

            if (activeScene != scene)
                SceneManager.SetActiveScene(activeScene);

            return instance;
        }

        private static T GetFromResources(string resourcePath)
        {
            var instance = Resources.Load<T>(resourcePath);

            instance.hideFlags = HideFlags.DontUnloadUnusedAsset;
            Resources.UnloadUnusedAssets();

            return instance;
        }

        private static T CreateInstance()
        {
            return new GameObject(typeof(T).Name, typeof(T))
                .GetComponent<T>();
        }

        private static bool IsPersistent()
        {
            return GetSettingsAttribute() is { IsPersistent: true };
        }

        #endregion
    }
}