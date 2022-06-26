using UnityAtoms.SceneMgmt;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ZType.Core.Integrations.UnityAtoms
{
    public static class Extensions
    {
        public static void Load(this SceneField sceneField, LoadSceneMode mode = LoadSceneMode.Single)
        {
            SceneManager.LoadScene(sceneField.SceneName, mode);
        }

        public static AsyncOperation LoadAsync(this SceneField sceneField, LoadSceneMode mode = LoadSceneMode.Single)
        {
            return SceneManager.LoadSceneAsync(sceneField.SceneName, mode);
        }

        public static AsyncOperation UnloadAsync(this SceneField sceneField,
            UnloadSceneOptions options = UnloadSceneOptions.None)
        {
            return SceneManager.UnloadSceneAsync(sceneField.SceneName, options);
        }

        public static Scene ToScene(this SceneField sceneField)
        {
            return SceneManager.GetSceneByName(sceneField.SceneName);
        }

        public static bool IsActive(this SceneField sceneField)
        {
            return SceneManager.GetActiveScene().name == sceneField.SceneName;
        }

        public static void SetActive(this SceneField sceneField)
        {
            SceneManager.SetActiveScene(sceneField.ToScene());
        }
    }
}