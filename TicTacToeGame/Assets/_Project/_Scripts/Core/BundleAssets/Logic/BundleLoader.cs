using System;
using System.Collections;
using System.IO;
using UnityEngine;
using GlassyCode.TTT.Core.BundleAssets.Data;

namespace GlassyCode.TTT.Core.BundleAssets.Logic
{
    public class BundleLoader : IBundleLoader
    {
        private AssetBundle _bundle;

        public event Action<string> OnBundleLoad;
        public bool IsBundleLoaded { get; private set; }

        public void LoadDefaultBundle()
        {
            LoadBundle(AssetBundleNames.DefaultBundle);
        }

        public IEnumerator LoadDefaultBundleAsync()
        {
            yield return LoadBundleAsyncCoroutine(AssetBundleNames.DefaultBundle);
        }

        private IEnumerator LoadBundleAsyncCoroutine(string bundleName)
        {
            var asyncOperation = LoadBundleAsync(bundleName);

            yield return new WaitUntil(() => asyncOperation.isDone);

            if (IsBundleLoaded)
            {
                OnBundleLoad?.Invoke(bundleName);
            }
        }

        private AsyncOperation LoadBundleAsync(string bundleName)
        {
            var path = $"{Application.streamingAssetsPath}/{bundleName}";

            if (!File.Exists(path))
            {
                Debug.LogWarning($"Bundle with name '{bundleName}' not found.");
                Debug.Log("Trying to load default bundle...");
                LoadDefaultBundle();
                return null;
            }

            UnloadBundle();

            var asyncOperation = AssetBundle.LoadFromFileAsync(path);

            asyncOperation.completed += operation =>
            {
                _bundle = ((AssetBundleCreateRequest)operation).assetBundle;
                IsBundleLoaded = true;
                Debug.Log($"Successfully loaded bundle '{bundleName}'.");
            };

            return asyncOperation;
        }

        public bool LoadBundle(string bundleName)
        {
            var path = $"{Application.streamingAssetsPath}/{bundleName}";

            if (!File.Exists(path))
            {
                Debug.LogWarning($"Bundle with name '{bundleName}' not found.");
                Debug.Log("Trying to load default bundle...");
                LoadDefaultBundle();
                return false;
            }

            UnloadBundle();

            var newBundle = AssetBundle.LoadFromFile(path);

            if (!newBundle)
            {
                Debug.LogWarning($"Couldn't load bundle '{bundleName}'.");
                Debug.Log("Trying to load default bundle...");
                LoadDefaultBundle();
                return false;
            }

            _bundle = newBundle;
            IsBundleLoaded = true;
            OnBundleLoad?.Invoke(bundleName);
            Debug.Log($"Successfully loaded bundle '{bundleName}'.");
            return true;
        }

        public void UnloadBundle()
        {
            if (_bundle)
            {
                _bundle.Unload(true);
                IsBundleLoaded = false;
            }
        }

        public Sprite GetSprite(string name)
        {
            if (!IsBundleLoaded)
            {
                Debug.LogError("Bundle not loaded!");
                return null;
            }

            var loadedObject = _bundle.LoadAsset<Sprite>(name);

            if (!loadedObject)
            {
                Debug.LogError($"There is no asset '{name}' in bundle '{_bundle.name}'!");
            }

            return loadedObject;
        }
    }
}