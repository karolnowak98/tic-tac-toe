using System;
using System.Collections;
using UnityEngine;

namespace GlassyCode.TTT.Core.BundleAssets.Logic
{
    public interface IBundleLoader
    {
        bool IsBundleLoaded { get; }
        event Action<string> OnBundleLoad;
        void LoadDefaultBundle();
        IEnumerator LoadDefaultBundleAsync();
        bool LoadBundle(string bundleName);
        void UnloadBundle();
        Sprite GetSprite(string name);
    }
}
