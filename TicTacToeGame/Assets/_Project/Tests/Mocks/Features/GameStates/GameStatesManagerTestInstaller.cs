using Zenject;
using GlassyCode.TTT.Game.States.Logic.Interfaces;
using GlassyCode.TTT.Tests.Mocks.Interfaces;

namespace GlassyCode.TTT.Tests.Mocks.Features.GameStates
{
    public class GameStatesManagerTestInstaller : Installer<GameStatesManagerTestInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(TestGameStatesManager), typeof(IGameStatesManager),
                    typeof(IInitializableTest))
                .To<TestGameStatesManager>()
                .FromSubContainerResolve()
                .ByMethod(InstallGameStateMachine)
                .AsSingle()
                .NonLazy();
        }

        private static void InstallGameStateMachine(DiContainer subContainer)
        {
            subContainer.Bind(typeof(TestGameStatesManager), typeof(IGameStatesManager),
                    typeof(IInitializableTest))
                .To<TestGameStatesManager>()
                .AsSingle()
                .NonLazy();
        }
    }
}