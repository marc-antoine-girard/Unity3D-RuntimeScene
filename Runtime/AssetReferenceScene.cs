#if ADDRESSABLES
using System;
using UnityEngine.AddressableAssets;

namespace ShackLab
{
#if UNITY_EDITOR
    using UnityEditor;

    [Serializable]
    public class AssetReferenceScene : AssetReferenceT<SceneAsset>
    {
        public AssetReferenceScene(string guid) : base(guid) { }
    }
#else
    [Serializable]
    public class AssetReferenceScene : AssetReference
    {
        public AssetReferenceScene(string guid) : base(guid) {}
    }
#endif
}
#endif
