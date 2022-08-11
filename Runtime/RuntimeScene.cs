using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShackLab
{
    #if UNITY_EDITOR
    using UnityEditor;

    public partial class RuntimeScene : ISerializationCallbackReceiver
    {
        [SerializeField] private SceneAsset sceneAsset;
        private static Dictionary<SceneAsset, int> cachedBuildIndexes = new Dictionary<SceneAsset, int>();

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

        void ISerializationCallbackReceiver.OnAfterDeserialize() { }
        
        private static void OnBuildListChanged()
        {
            cachedBuildIndexes.Clear();

            foreach (var scene in EditorBuildSettings.scenes)
            {
                if (!scene.enabled) continue;

                SceneAsset sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(scene.path);
                if (sceneAsset != null)
                    cachedBuildIndexes.Add(sceneAsset, cachedBuildIndexes.Count);
            }
        }
        
        private static int GetBuildIndex(SceneAsset sceneAsset)
        {
            if (sceneAsset == null || !cachedBuildIndexes.TryGetValue(sceneAsset, out int buildIndex))
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
        [SerializeField, HideInInspector] private string name;
        [SerializeField, HideInInspector] private int buildIndex;

        public string Name => name;
        public int BuildIndex => buildIndex;
    }
}