#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;

namespace ShackLab
{
    public static class RuntimeSceneUtility
    {
        public static readonly Dictionary<SceneAsset, int> CachedScenes = new Dictionary<SceneAsset, int>();
    }
}
#endif
