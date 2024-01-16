using System.Collections.Generic;
using UnityEngine;
using Zenject;
using GlassyCode.TTT.Game.TicTacToe.Data;
using GlassyCode.TTT.Game.TicTacToe.Logic.Boards;
using GlassyCode.TTT.Game.TicTacToe.Logic.Movement;
using GlassyCode.TTT.Game.TicTacToe.Logic.Players;
using GlassyCode.TTT.Game.TicTacToe.Logic.Signals;
using GlassyCode.TTT.Game.TicTacToe.Logic.Timers;

namespace GlassyCode.TTT.Game.TicTacToe.Logic
{
    public class TicTacToeInstaller : MonoInstaller
    {
        [SerializeField] private TicTacToeConfig _config;
        
        public override void InstallBindings()
        {
            DeclareSignals(Container);
            
            Container.Bind(typeof(TicTacToeManager), typeof(ITicTacToeManager),
                    typeof(IInitializable))
                .To<TicTacToeManager>()
                .FromSubContainerResolve()
                .ByMethod(InstallTicTacToeManager)
                .AsSingle();
            
            Container.Bind(typeof(PlayersController), typeof(IPlayersController),
                    typeof(IInitializable), typeof(ITickable))
                .To<PlayersController>()
                .FromSubContainerResolve()
                .ByMethod(InstallPlayersController)
                .AsSingle();
            
            Container.Bind<ITurnTimer>().To<TurnTimer>().AsSingle();

            BindInstances(Container, _config);
        }

        private static void DeclareSignals(DiContainer container)
        {
            SignalBusInstaller.Install(container);

            container.DeclareSignal<MakeMoveSignal>();
            container.DeclareSignal<WinGameByTimeSignal>();
        }

        private static void InstallTicTacToeManager(DiContainer subContainer)
        {
            subContainer.Bind(typeof(TicTacToeManager), typeof(ITicTacToeManager), 
                    typeof(IInitializable))
                .To<TicTacToeManager>()
                .AsSingle();
            
            subContainer.Bind<IMoveHistory>().To<MoveHistory>().FromInstance(new MoveHistory(new Stack<Move>())).AsSingle();
        }

        private static void InstallPlayersController(DiContainer subContainer)
        {
            subContainer.Bind(typeof(PlayersController), typeof(IPlayersController),
                    typeof(IInitializable), typeof(ITickable))
                .To<PlayersController>()
                .AsSingle();
        }

        private static void BindInstances(DiContainer container, TicTacToeConfig config)
        {
            container.Bind<ITicTacToeConfig>().To<TicTacToeConfig>().FromInstance(config).AsSingle();
            container.Bind<IBoard>().To<Board>().FromInstance(new Board(config.BoardSize)).AsSingle();
        }
    }
}