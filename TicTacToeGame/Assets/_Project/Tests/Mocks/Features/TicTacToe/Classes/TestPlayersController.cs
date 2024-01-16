using System;
using UnityEngine;
using Zenject;
using GlassyCode.TTT.Core.Time;
using GlassyCode.TTT.Game.States.Data;
using GlassyCode.TTT.Game.States.Logic.Interfaces;
using GlassyCode.TTT.Game.TicTacToe.Data;
using GlassyCode.TTT.Game.TicTacToe.Data.Enums;
using GlassyCode.TTT.Game.TicTacToe.Logic;
using GlassyCode.TTT.Game.TicTacToe.Logic.Boards;
using GlassyCode.TTT.Game.TicTacToe.Logic.Movement.Strategies;
using GlassyCode.TTT.Game.TicTacToe.Logic.Players;
using GlassyCode.TTT.Game.TicTacToe.Logic.Signals;
using GlassyCode.TTT.Game.TicTacToe.Logic.Timers;
using GlassyCode.TTT.Tests.Mocks.Interfaces;

namespace GlassyCode.TTT.Tests.Mocks.Features.TicTacToe.Classes
{
    public class TestPlayersController : IPlayersController, IInitializableTest, IDisposable, ITickableTest
    {
        private SignalBus _signalBus;
        private IGameStatesManager _gameStatesManager;
        private ITimeController _timeController;
        private ITurnTimer _turnTimer;
        private ITicTacToeConfig _config;
        private IBoard _board;
        private IPlayer _firstPlayer;
        private IPlayer _secondPlayer;

        public IPlayer CurrentPlayer { get; private set; }
        public Symbol CurrentPlayerSymbol => CurrentPlayer.Symbol;
        public IPlayer NextPlayer => CurrentPlayer == _firstPlayer ? _secondPlayer : _firstPlayer;
        public bool IsComputerTurn => CurrentPlayer.Type == PlayerType.Computer;
        
        public event Action<IPlayer> OnPlayersSwapped;
        
        [Inject]
        public void Construct(SignalBus signalBus, IGameStatesManager gameStatesManager,
            ITimeController timeController, ITurnTimer turnTimer, ITicTacToeConfig config, IBoard board)
        {
            _signalBus = signalBus;
            _gameStatesManager = gameStatesManager;
            _timeController = timeController;
            _turnTimer = turnTimer;
            _config = config;
            _board = board;
        }
        
        public void Initialize()
        {
            _turnTimer.OnTimePassed += FireWinGameByTimeSignal;
        }

        public void Dispose()
        {
            _turnTimer.OnTimePassed -= FireWinGameByTimeSignal;
        }
        
        public void Tick()
        {
            CurrentPlayer.Tick(_timeController.DeltaTime);
            _turnTimer.Tick(_timeController.DeltaTime);
        }
        
        public void InitPlayers()
        {
            var symbolForFirstPlayer = TicTacToeExtensions.GetRandomSymbol;
            var symbolForSecondPlayer = symbolForFirstPlayer == Symbol.O ? Symbol.X : Symbol.O;

            switch (_gameStatesManager.GameMode)
            {
                case GameMode.PlayerVsComputer:
                    _firstPlayer = new Player(PlayerType.Human, _config.FirstPlayerName, symbolForFirstPlayer, new HumanMoveStrategy(_board));
                    _secondPlayer = new Player(PlayerType.Computer, _config.FirstComputerName, symbolForSecondPlayer, 
                        new ComputerMoveStrategy(_board, _config.ComputerMinThinkSeconds, _config.ComputerMaxThinkSeconds));
                    break;
                case GameMode.PlayerVsPlayer:
                    _firstPlayer = new Player(PlayerType.Human, _config.FirstPlayerName, symbolForFirstPlayer, new HumanMoveStrategy(_board));
                    _secondPlayer = new Player(PlayerType.Human, _config.SecondPlayerName, symbolForSecondPlayer, new HumanMoveStrategy(_board));
                    break;
                case GameMode.ComputerVsComputer:
                    _firstPlayer = new Player(PlayerType.Computer, _config.FirstComputerName, 
                        symbolForFirstPlayer, new ComputerMoveStrategy(_board, _config.ComputerMinThinkSeconds, _config.ComputerMaxThinkSeconds));
                    _secondPlayer = new Player(PlayerType.Computer, _config.SecondComputerName, 
                        symbolForSecondPlayer, new ComputerMoveStrategy(_board, _config.ComputerMinThinkSeconds, _config.ComputerMaxThinkSeconds));
                    break;
            }

            SetUpPlayer(_config.FirstMoveSymbol == _firstPlayer.Symbol ? _firstPlayer : _secondPlayer);
        }

        public void SwapPlayers()
        {
            CurrentPlayer.OnMadeMove -= FireMadeMoveSignal;
            CurrentPlayer = NextPlayer;
            SetUpPlayer(CurrentPlayer);
        }
        
        public IPlayer PlayerBySymbol(Symbol symbol)
        {
            return _firstPlayer.Symbol == symbol ? _firstPlayer : _secondPlayer;
        }

        private void FireMadeMoveSignal(Vector2Int cellPos)
        {
            _signalBus.TryFire(new MakeMoveSignal(cellPos));
        }

        private void FireWinGameByTimeSignal()
        {
            _signalBus.TryFire(new WinGameByTimeSignal {Player = NextPlayer});
        }
        
        private void SetUpPlayer(IPlayer player)
        {
            CurrentPlayer = player;
            CurrentPlayer.OnMadeMove += FireMadeMoveSignal;
            OnPlayersSwapped?.Invoke(CurrentPlayer);
            CurrentPlayer.PrepareMove();
            _turnTimer.Start();
        }
    }
}