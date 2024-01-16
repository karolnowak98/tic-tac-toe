using GlassyCode.TTT.Game.TicTacToe.Logic.Timers;
using GlassyCode.TTT.Tests.Mocks.Features.TicTacToe.Classes;
using Zenject;

namespace GlassyCode.TTT.Tests.Mocks.Features.TicTacToe.Installers
{
    public class TurnTimerTestInstaller : Installer<TurnTimerTestInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ITurnTimer>().To<TestTurnTimer>().AsSingle();
        }
    }
}