using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace ShackLab
{
    public partial class RuntimeSceneDrawer
    {
        public static class Styles
        {
            private static MethodInfo getHelpIconMethod;
            private static Texture warningIcon;

            private static MethodInfo GetHelpIconMethod
            {
                get
                {
                    if (getHelpIconMethod == null)
                        getHelpIconMethod = typeof(EditorGUIUtility).GetMethod("GetHelpIcon", (BindingFlags)~0);

                    return getHelpIconMethod;
                }
            }

            private static Texture WarningIcon
            {
                get
                {
                    if (warningIcon == null)
                    {
                        warningIcon = (Texture)GetHelpIconMethod.Invoke(null, new object[] { MessageType.Warning });
                    }

                    return warningIcon;
                }
            }

            public static Texture GetHelpIcon(MessageType type)
            {
                switch (type)
                {
                    case MessageType.Info:
                    // return EditorGUIUtility.infoIcon;
                    case MessageType.Warning:
                        return WarningIcon;
                    case MessageType.Error:
                    // return EditorGUIUtility.errorIcon;
                    default:
                        return null;
                }
            }
        }
    }
}
