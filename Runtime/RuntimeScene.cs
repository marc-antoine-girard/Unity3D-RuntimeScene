using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShackLab
{
#if UNITY_EDITOR
    using UnityEditor;
    using UnityEditor.SceneManagement;

    public partial class RuntimeScene : ISerializationCallbackReceiver
    {
        [SerializeField] private SceneAsset sceneAsset;

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            buildIndex = GetBuildIndex(sceneAsset);
            name = GetName(sceneAsset);
        }

        private static string GetScenePath(SceneAsset scene)
        {
            if (scene == null) return string.Empty;
            return AssetDatabase.GetAssetPath(scene);
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize() { }

        private static int GetBuildIndex(SceneAsset sceneAsset)
        {
            if (sceneAsset == null || !RuntimeSceneUtility.CachedScenes.TryGetValue(sceneAsset, out int buildIndex))
                return -1;

            return buildIndex;
        }

        private static string GetName(SceneAsset sceneAsset)
        {
            if (sceneAsset == null)
                return string.Empty;

            return sceneAsset.name;
        }

        private void LoadSceneEditor(LoadSceneParameters parameters)
        {
            if (!IsInBuild())
            {
                Log(
                    $"Scene {sceneAsset.name} is not in the build settings. Consider adding it if you plan on using it in a build",
                    Color.cyan);
            }

            EditorSceneManager.LoadSceneInPlayMode(AssetDatabase.GetAssetPath(sceneAsset), parameters);
        }

        private AsyncOperation LoadSceneAsyncEditor(LoadSceneParameters parameters, bool allowSceneActivation = true)
        {
            if (!IsInBuild())
            {
                Log(
                    $"Scene {sceneAsset.name} is not in the build settings. Consider adding it if you plan on using it in a build",
                    Color.cyan);
            }

            AsyncOperation loadSceneAsync =
                EditorSceneManager.LoadSceneAsyncInPlayMode(AssetDatabase.GetAssetPath(sceneAsset), parameters);
            loadSceneAsync.allowSceneActivation = allowSceneActivation;
            return loadSceneAsync;
        }

        private void Log(object message, Color color)
        {
            Debug.LogWarning(
                $"<color=#{(byte)(color.r * 255f):X2}{(byte)(color.g * 255f):X2}{(byte)(color.b * 255f):X2}>{message}</color>");
        }
    }
#endif

    [Serializable]
    public partial class RuntimeScene
    {
        [SerializeField] private string name;
        [SerializeField] private int buildIndex;

        public string Name => name;
        public int BuildIndex => buildIndex;

        public bool IsInBuild() => buildIndex <= 0;

        public void LoadScene()
        {
            LoadSceneInternal(new LoadSceneParameters());
        }

        public void LoadScene(LoadSceneMode mode)
        {
            LoadSceneInternal(new LoadSceneParameters(mode));
        }

        public void LoadScene(LoadSceneParameters parameters)
        {
            LoadSceneInternal(parameters);
        }

        public AsyncOperation LoadSceneAsync(bool allowSceneActivation = true)
        {
            return LoadSceneAsyncInternal(new LoadSceneParameters(), allowSceneActivation);
        }

        public AsyncOperation LoadSceneAsync(LoadSceneMode mode, bool allowSceneActivation = true)
        {
            return LoadSceneAsyncInternal(new LoadSceneParameters(mode), allowSceneActivation);
        }

        public AsyncOperation LoadSceneAsync(LoadSceneParameters parameters, bool allowSceneActivation = true)
        {
            return LoadSceneAsyncInternal(parameters, allowSceneActivation);
        }

        private void LoadSceneInternal(LoadSceneParameters parameters)
        {
#if UNITY_EDITOR && !DISABLE_LOAD_EDITOR
            LoadSceneEditor(parameters);
#else
            SceneManager.LoadScene(buildIndex, parameters);
#endif
        }

        private AsyncOperation LoadSceneAsyncInternal(LoadSceneParameters parameters, bool allowSceneActivation = true)
        {
#if UNITY_EDITOR && !DISABLE_LOAD_EDITOR
            return LoadSceneAsyncEditor(parameters, allowSceneActivation);
#else
            AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync(buildIndex, parameters);
            loadSceneAsync.allowSceneActivation = allowSceneActivation;
            return loadSceneAsync;
#endif
        }
    }
}
