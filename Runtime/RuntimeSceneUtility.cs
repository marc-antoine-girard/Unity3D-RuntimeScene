using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ShackLab
{
    public static class RuntimeSceneUtility
    {
        public static Dictionary<SceneAsset, int> cachedScenes = new Dictionary<SceneAsset, int>();

        [InitializeOnLoadMethod]
        private static void OnEditorInitializeOnLoad()
        {
            OnBuildListChanged();

            EditorBuildSettings.sceneListChanged -= OnBuildListChanged;
            EditorBuildSettings.sceneListChanged += OnBuildListChanged;
            
            EditorApplication.contextualPropertyMenu -= OnPropertyContextMenu;
            EditorApplication.contextualPropertyMenu += OnPropertyContextMenu;
        }

        private static void OnBuildListChanged()
        {
            cachedScenes.Clear();
            int buildIndex = -1;
            foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
            {
                SceneAsset sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(scene.path);

                if (sceneAsset != null)
                    cachedScenes.Add(sceneAsset, scene.enabled ? ++buildIndex : -1);
            }
        }
        
        private static void OnPropertyContextMenu(GenericMenu menu, SerializedProperty property)
        {
            if (property.propertyType != SerializedPropertyType.ObjectReference) return;
            if (property.objectReferenceValue is not SceneAsset sceneAsset) return;

            bool on = RuntimeSceneUtility.cachedScenes.ContainsKey(sceneAsset);
            string scenePath = AssetDatabase.GetAssetPath(sceneAsset);

            menu.AddItem(new GUIContent("Add to Build Settings"), on, () =>
            {
                List<EditorBuildSettingsScene> editorBuildSettingsScenes = EditorBuildSettings.scenes.ToList();

                if (!on)
                {
                    editorBuildSettingsScenes.Add(new EditorBuildSettingsScene(scenePath, true));
                }
                else
                {
                    for (int i = editorBuildSettingsScenes.Count - 1; i >= 0; i--)
                    {
                        if (editorBuildSettingsScenes[i].path.Equals(scenePath))
                        {
                            editorBuildSettingsScenes.Remove(editorBuildSettingsScenes[i]);
                            break;
                        }
                    }
                }
                EditorBuildSettings.scenes = editorBuildSettingsScenes.ToArray();
            });
        }
    }
}