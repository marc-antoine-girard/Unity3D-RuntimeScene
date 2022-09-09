using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ShackLab
{
    [CustomPropertyDrawer(typeof(SceneAttribute))]
    public class SceneAttributeDrawer : PropertyDrawer
    {
        public override bool CanCacheInspectorGUI(SerializedProperty property) => false;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.String)
            {
                StringGUI(position, property, label);
            }
        }

        private void StringGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            bool isInBuild = true;
            var propertyStringValue = property.stringValue;

            List<string> cachedScenesKeys =
                RuntimeSceneUtility.CachedScenes.Keys.Select(source => source.name).ToList();

            var indexOf = cachedScenesKeys.IndexOf(propertyStringValue);

            if (indexOf == -1)
            {
                isInBuild = false;
                cachedScenesKeys.Add(propertyStringValue);
                indexOf = cachedScenesKeys.IndexOf(propertyStringValue);
            }

            var color = GUI.color;

            if (!isInBuild) GUI.color = Color.red;

            var popup = EditorGUI.Popup(position, label.text, indexOf, cachedScenesKeys.ToArray());

            GUI.color = color;

            if (indexOf != popup)
            {
                property.stringValue = cachedScenesKeys[popup];
            }
        }
    }
}