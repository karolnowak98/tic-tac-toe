using System.Collections;
using GlassyCode.TTT.Core.BundleAssets.Data;
using GlassyCode.TTT.Core.BundleAssets.Logic;
using GlassyCode.TTT.Tests.Mocks.Features.BundleAssets;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

namespace GlassyCode.TTT.Tests.PlayMode.Unit.BundleAssets
{
    [TestFixture]
    public class BundleLoaderTests : ZenjectUnitTestFixture
    {
        private IBundleLoader _bundleLoader;
        
        [SetUp]
        public void InitBundleLoader()
        {
            BundleLoaderTestInstaller.Install(Container);
            
            _bundleLoader = Container.Resolve<IBundleLoader>();
        }
        
        [Test]
        public void LoadDefaultBundle_Success()
        {
            const string expectedBundleName = AssetBundleNames.DefaultBundle;
            var resultBundleName = "";
            
            Assert.IsFalse(_bundleLoader.IsBundleLoaded);

            _bundleLoader.OnBundleLoad += OnBundleLoad;
            
            _bundleLoader.LoadDefaultBundle();

            _bundleLoader.OnBundleLoad -= OnBundleLoad;
            
            Assert.IsTrue(_bundleLoader.IsBundleLoaded);
            Assert.AreEqual(resultBundleName, expectedBundleName);
            
            _bundleLoader.UnloadBundle();
            return;

            void OnBundleLoad(string bundleName) => resultBundleName = bundleName;
        }
        
        [UnityTest]
        public IEnumerator LoadDefaultBundleAsync_Success()
        {
            const string expectedBundleName = AssetBundleNames.DefaultBundle;
            var resultBundleName = "";
            
            Assert.IsFalse(_bundleLoader.IsBundleLoaded);

            _bundleLoader.OnBundleLoad += OnBundleLoad;
            
            yield return _bundleLoader.LoadDefaultBundleAsync();
            
            _bundleLoader.OnBundleLoad -= OnBundleLoad;
            
            Assert.IsTrue(_bundleLoader.IsBundleLoaded);
            Assert.AreEqual(resultBundleName, expectedBundleName);
            
            _bundleLoader.UnloadBundle();
            yield break;

            void OnBundleLoad(string bundleName) => resultBundleName = bundleName;
        }
        
        [Test]
        public void LoadBundle_Success()
        {
            const string expectedBundleName = AssetBundleNames.MoonActiveBundle;
            var resultBundleName = "";
            
            Assert.IsFalse(_bundleLoader.IsBundleLoaded);

            _bundleLoader.OnBundleLoad += OnBundleLoad;
            
            _bundleLoader.LoadBundle(expectedBundleName);

            _bundleLoader.OnBundleLoad -= OnBundleLoad;
            
            Assert.IsTrue(_bundleLoader.IsBundleLoaded);
            Assert.AreEqual(resultBundleName, expectedBundleName);
            
            _bundleLoader.UnloadBundle();
            return;

            void OnBundleLoad(string bundleName) => resultBundleName = bundleName;
        }
        
        [Test]
        public void LoadBundle_BundleNotFound()
        {
            const string expectedBundleName = "TestName";
            var resultBundleName = "";
            
            Assert.IsFalse(_bundleLoader.IsBundleLoaded);

            _bundleLoader.OnBundleLoad += OnBundleLoad;
            
            _bundleLoader.LoadBundle(expectedBundleName);

            _bundleLoader.OnBundleLoad -= OnBundleLoad;
            
            Assert.IsTrue(_bundleLoader.IsBundleLoaded);
            Assert.AreNotEqual(resultBundleName, expectedBundleName);
            Assert.AreEqual(resultBundleName, AssetBundleNames.DefaultBundle);
            
            _bundleLoader.UnloadBundle();
            return;

            void OnBundleLoad(string bundleName) => resultBundleName = bundleName;
        }
        
        [Test]
        public void UnloadBundle_Test()
        {
            Assert.IsFalse(_bundleLoader.IsBundleLoaded);
            
            _bundleLoader.LoadDefaultBundle();
            
            Assert.IsTrue(_bundleLoader.IsBundleLoaded);
            
            _bundleLoader.UnloadBundle();
            
            Assert.IsFalse(_bundleLoader.IsBundleLoaded);
        }

        [Test]
        public void GetSprite_BundleNotLoaded()
        { 
            var sprite = _bundleLoader.GetSprite("testName");
            
            LogAssert.Expect(LogType.Error, "Bundle not loaded!");
            
            Assert.IsNull(sprite);
        }
        
        [Test]
        public void GetSprite_BundleLoaded_SpriteNotFound()
        {
            const string testAssetName = "test";
            const string defaultBundleName = AssetBundleNames.DefaultBundle;
            
            LogAssert.Expect(LogType.Error, $"There is no asset '{testAssetName}' in bundle '{defaultBundleName}'!");
            
            _bundleLoader.LoadDefaultBundle();
            
            var sprite = _bundleLoader.GetSprite(testAssetName);
            
            Assert.IsNull(sprite);
        }
        
        [Test]
        public void GetSprite_BundleLoaded_SpriteFound()
        { 
            _bundleLoader.LoadDefaultBundle();
            
            var sprite = _bundleLoader.GetSprite(AssetBundleNames.XSymbolSprite);
            
            Assert.IsNotNull(sprite);
        }

        [TearDown]
        public void TearDown()
        {
            Container.UnbindAll();
            _bundleLoader.UnloadBundle();
        }
    }
}