using GlassyCode.TTT.Game.TicTacToe.Data;
using GlassyCode.TTT.Tests.Mocks.Features.TicTacToe.Classes;
using Zenject;

namespace GlassyCode.TTT.Tests.Mocks.Features.TicTacToe.Installers
{
    public class TicTacToeConfigTestInstaller : Installer<TicTacToeConfigTestInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ITicTacToeConfig>().To<TestTicTacToeConfig>().AsSingle();
        }
    }
}