using GlassyCode.TTT.Core.SceneLoader;
using GlassyCode.TTT.Core.Time;
using Zenject;

namespace GlassyCode.TTT.Tests.Mocks.Features.TicTacToe.Installers
{
    public class CoreTestInstaller : Installer<CoreTestInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle().NonLazy();
            Container.Bind<ITimeController>().To<TimeController>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        }
    }
}