using GlassyCode.TTT.Game.TicTacToe.Logic.Players;
using GlassyCode.TTT.Tests.Mocks.Features.TicTacToe.Classes;
using GlassyCode.TTT.Tests.Mocks.Interfaces;
using Zenject;

namespace GlassyCode.TTT.Tests.Mocks.Features.TicTacToe.Installers
{
    public class PlayersControllerTestInstaller : Installer<PlayersControllerTestInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(TestPlayersController), typeof(IPlayersController),
                    typeof(IInitializableTest), typeof(ITickableTest))
                .To<TestPlayersController>()
                .FromSubContainerResolve()
                .ByMethod(InstallPlayersController)
                .AsSingle();
        }
        
        private static void InstallPlayersController(DiContainer subContainer)
        {
            subContainer.Bind(typeof(TestPlayersController), typeof(IPlayersController),
                    typeof(IInitializableTest), typeof(ITickableTest))
                .To<TestPlayersController>()
                .AsSingle();
        }
    }
}