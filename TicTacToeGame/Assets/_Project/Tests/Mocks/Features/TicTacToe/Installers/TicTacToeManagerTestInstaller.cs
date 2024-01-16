using System.Collections.Generic;
using GlassyCode.TTT.Game.TicTacToe.Logic;
using GlassyCode.TTT.Game.TicTacToe.Logic.Movement;
using GlassyCode.TTT.Tests.Mocks.Features.TicTacToe.Classes;
using GlassyCode.TTT.Tests.Mocks.Interfaces;
using Zenject;

namespace GlassyCode.TTT.Tests.Mocks.Features.TicTacToe.Installers
{
    public class TicTacToeManagerTestInstaller : Installer<TicTacToeManagerTestInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(TestTicTacToeManager), typeof(ITicTacToeManager),
                    typeof(IInitializableTest))
                .To<TestTicTacToeManager>()
                .FromSubContainerResolve()
                .ByMethod(InstallTicTacToeManager)
                .AsSingle();
        }
        
        private static void InstallTicTacToeManager(DiContainer subContainer)
        {
            subContainer.Bind(typeof(TestTicTacToeManager), typeof(ITicTacToeManager), 
                    typeof(IInitializableTest))
                .To<TestTicTacToeManager>()
                .AsSingle();
            
            subContainer.Bind<IMoveHistory>().To<MoveHistory>().FromInstance(new MoveHistory(new Stack<Move>())).AsSingle();
        }
    }
}