using Zenject;

namespace GlassyCode.TTT.Core.BundleAssets.Logic
{
    public class BundleInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IBundleLoader>().To<BundleLoader>().AsSingle();
        }
    }
}
