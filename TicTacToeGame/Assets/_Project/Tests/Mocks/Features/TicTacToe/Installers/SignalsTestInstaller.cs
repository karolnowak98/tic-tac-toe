using GlassyCode.TTT.Game.TicTacToe.Logic.Signals;
using Zenject;

namespace GlassyCode.TTT.Tests.Mocks.Features.TicTacToe.Installers
{
    public class SignalsTestInstaller : Installer<SignalsTestInstaller>
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<MakeMoveSignal>();
            Container.DeclareSignal<WinGameByTimeSignal>();
        }
    }
}