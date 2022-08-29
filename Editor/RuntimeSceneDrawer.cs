using UnityEditor;
using UnityEngine;

namespace ShackLab
{
    [CustomPropertyDrawer(typeof(RuntimeScene))]
    public class RuntimeSceneDrawer : PropertyDrawer
    {
        private SerializedProperty serializedProperty;
        private SerializedProperty sceneAssetProp;
        private SerializedProperty buildIndex;

        public override bool CanCacheInspectorGUI(SerializedProperty property) => false;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            Initialize(property);
            return GetHeight();
        }

        private float GetHeight()
        {
            return buildIndex.intValue < 0 && sceneAssetProp.objectReferenceValue != null ? 54 : 18;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Initialize(property);

            DrawScene(position, label);
            DrawWarning(position);
        }

        private void DrawWarning(Rect position)
        {
            if (sceneAssetProp.objectReferenceValue == null) return;

            if (buildIndex.intValue < 0)
            {
                Rect newPos = new Rect(position)
                {
                    yMin = position.y + 22
                };
                EditorGUI.HelpBox(newPos, "Scene is not in Build Settings", MessageType.Warning);
            }
        }

        private void DrawScene(Rect position, GUIContent label)
        {
            Rect rect = new Rect(position)
            {
                height = EditorGUI.GetPropertyHeight(sceneAssetProp, false)
            };
            EditorGUI.PropertyField(rect, sceneAssetProp, label);
        }

        private void Initialize(SerializedProperty property)
        {
            if (serializedProperty == property)
                return;

            serializedProperty = property;
            sceneAssetProp = property.FindPropertyRelative("sceneAsset");
            buildIndex = property.FindPropertyRelative("buildIndex");
        }
    }
}
