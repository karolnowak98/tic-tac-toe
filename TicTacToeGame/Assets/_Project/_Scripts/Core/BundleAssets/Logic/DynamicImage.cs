using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GlassyCode.TTT.Core.BundleAssets.Logic
{
    //It's an alternative to loading bundles from code
    [RequireComponent(typeof(Image))]
    public class DynamicImage : MonoBehaviour
    {
        [SerializeField] private string _assetName;

        private IBundleLoader _bundleLoader;
        private Image _imageRenderer;

        [Inject]
        private void Construct(IBundleLoader bundleLoader)
        {
            _bundleLoader = bundleLoader;
            
            _bundleLoader.OnBundleLoad += UpdateImage;
        }
        
        private void OnDestroy()
        {
            _bundleLoader.OnBundleLoad -= UpdateImage;
        }

        private void Awake()
        {
            _imageRenderer = GetComponent<Image>();
        }

        private void UpdateImage(string bundleName)
        {
            _imageRenderer.sprite = _bundleLoader.GetSprite(_assetName);
        }
    }
}
