using UnityEditor;
using UnityEngine;

namespace ShackLab
{
    [CustomPropertyDrawer(typeof(RuntimeScene))]
    public class RuntimeSceneDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty sceneAssetProp = property.FindPropertyRelative("sceneAsset");

            using (new EditorGUI.PropertyScope(position, label, sceneAssetProp))
            {
                EditorGUI.PropertyField(position, sceneAssetProp, label);
            }
        }
    }
}
