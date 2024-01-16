using System;
using UnityEngine;
using Zenject;
using GlassyCode.TTT.Core.Time;
using GlassyCode.TTT.Game.States.Data;
using GlassyCode.TTT.Game.TicTacToe.Data.Enums;
using GlassyCode.TTT.Game.TicTacToe.Logic;
using GlassyCode.TTT.Game.TicTacToe.Logic.Boards;
using GlassyCode.TTT.Game.TicTacToe.Logic.Movement;
using GlassyCode.TTT.Game.TicTacToe.Logic.Players;
using GlassyCode.TTT.Game.TicTacToe.Logic.Signals;
using GlassyCode.TTT.Tests.Mocks.Interfaces;

namespace GlassyCode.TTT.Tests.Mocks.Features.TicTacToe.Classes
{
    public class TestTicTacToeManager : ITicTacToeManager, IInitializableTest, IDisposable
    {
        private SignalBus _signalBus;
        private IBoard _board;
        private IMoveHistory _moveHistory;
        private ITimeController _timeController;
        private IPlayersController _playersController;
        private GameMode _currentGameMode;

        public Vector2Int? GetHint => _playersController.IsComputerTurn ? null : _board.GetRandomEmptyField();

        public event Action<IPlayer, Vector2Int> OnMoveMade;
        public event Action<Vector2Int> OnMoveUndid; 
        public event Action<IPlayer, Vector2Int[]> OnGameFinished;
        public event Action OnGameReset;
        
        [Inject]
        private void Construct(SignalBus signalBus, IPlayersController playersController, 
            IMoveHistory moveHistory, IBoard board)
        {
            _signalBus = signalBus;
            _playersController = playersController;
            _moveHistory = moveHistory;
            _board = board;
        }
        
        public void Initialize()
        {
            _signalBus.Subscribe<WinGameByTimeSignal>(x => WinGame(x.Player));
            _signalBus.Subscribe<MakeMoveSignal>(x => MakeMove(x.CellPos));
            
            ResetGame();
        }
        
        public void Dispose()
        {
            _signalBus.TryUnsubscribe<WinGameByTimeSignal>(x => WinGame(x.Player));
            _signalBus.TryUnsubscribe<MakeMoveSignal>(x => MakeMove(x.CellPos));
        }
        
        public void MakeMove(Vector2Int cellPos) 
        {
            if (_board.IsFieldOccupiedAtPosition(cellPos)) 
                return;

            _board.SetSymbolAtPosition(cellPos, _playersController.CurrentPlayerSymbol);
            _moveHistory.AddMove(new Move(cellPos));
            OnMoveMade?.Invoke(_playersController.CurrentPlayer, cellPos);

            var winningSymbol = _board.CheckWinForPlayers();

            if (winningSymbol != Symbol.None)
            {
                WinGame(_playersController.PlayerBySymbol(winningSymbol), _board.GetWinningCoords(winningSymbol));
                return;
            }

            if (_board.IsFull())
            {
                WinGame();
                return;
            }

            _playersController.SwapPlayers();
        }
        
        public void UndoMove()
        {
            if (_moveHistory.IsEmpty) 
                return;
            
            var lastMove = _moveHistory.UndoMove();
            if (!lastMove.HasValue) 
                return;

            _board.MarkAsEmptyAtPosition(lastMove.Value.CellPos);
            _playersController.SwapPlayers();
            OnMoveUndid?.Invoke(lastMove.Value.CellPos);
        }

        public void ResetGame()
        {
            _moveHistory.Clear();
            _board.Reset();
            _playersController.InitPlayers();
            OnGameReset?.Invoke();
            TimeController.Unpause();
        }

        private void WinGame(IPlayer player = null, Vector2Int[] winningIndexes = null)
        {
            TimeController.Pause();
            OnGameFinished?.Invoke(player, winningIndexes);
        }
    }
}