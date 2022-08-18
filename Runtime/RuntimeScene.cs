using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShackLab
{
#if UNITY_EDITOR
    using UnityEditor;

    public partial class RuntimeScene : ISerializationCallbackReceiver
    {
        [SerializeField] private SceneAsset sceneAsset;
        private static Dictionary<SceneAsset, int> cachedScenes = new Dictionary<SceneAsset, int>();

        [InitializeOnLoadMethod]
        private static void OnEditorInitializeOnLoad()
        {
            OnBuildListChanged();

            EditorBuildSettings.sceneListChanged -= OnBuildListChanged;
            EditorBuildSettings.sceneListChanged += OnBuildListChanged;
        }

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

        private static void OnBuildListChanged()
        {
            int buildIndex = -1;
            foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
            {
                SceneAsset sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(scene.path);

                if (sceneAsset != null)
                {
                    cachedScenes.Add(sceneAsset, scene.enabled ? ++buildIndex : -1);
                }
            }
        }

        private static int GetBuildIndex(SceneAsset sceneAsset)
        {
            if (sceneAsset == null || !cachedScenes.TryGetValue(sceneAsset, out var buildIndex))
                return -1;

            return buildIndex;
        }

        private static string GetName(SceneAsset sceneAsset)
        {
            if (sceneAsset == null)
                return string.Empty;

            return sceneAsset.name;
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
        
        public void LoadScene()
        {
            SceneManager.LoadScene(buildIndex);
        }
        
        public void LoadScene(LoadSceneMode mode)
        {
            SceneManager.LoadScene(buildIndex, mode);
        }
        
        public void LoadScene(LoadSceneParameters parameters)
        {
            SceneManager.LoadScene(buildIndex, parameters);
        }

        public AsyncOperation LoadSceneAsync(bool allowSceneActivation = true)
        {
            var loadSceneAsync = SceneManager.LoadSceneAsync(buildIndex);
            loadSceneAsync.allowSceneActivation = allowSceneActivation;
            return loadSceneAsync;
        }
        
        public AsyncOperation LoadSceneAsync(LoadSceneMode mode, bool allowSceneActivation = true)
        {
            var loadSceneAsync = SceneManager.LoadSceneAsync(buildIndex, mode);
            loadSceneAsync.allowSceneActivation = allowSceneActivation;
            return loadSceneAsync;
        }
        
        public AsyncOperation LoadSceneAsync(LoadSceneParameters parameters, bool allowSceneActivation = true)
        {
            var loadSceneAsync = SceneManager.LoadSceneAsync(buildIndex, parameters);
            loadSceneAsync.allowSceneActivation = allowSceneActivation;
            return loadSceneAsync;
        }
    }
}
