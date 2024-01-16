using Zenject;
using GlassyCode.TTT.Core.BundleAssets.Logic;

namespace GlassyCode.TTT.Tests.Mocks.Features.BundleAssets
{
    public class BundleLoaderTestInstaller : Installer<BundleLoaderTestInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IBundleLoader>().To<BundleLoader>().AsSingle();
        }
    }
}