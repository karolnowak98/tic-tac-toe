using System;
using UnityEngine;
using GlassyCode.TTT.Game.TicTacToe.Data.Enums;
using GlassyCode.TTT.Game.TicTacToe.Logic.Movement.Strategies;

namespace GlassyCode.TTT.Game.TicTacToe.Logic.Players
{
    public class Player : IPlayer, IDisposable
    {
        private readonly IMoveStrategy _moveStrategy;
        
        public PlayerType Type { get; }
        public string Name { get; }
        public Symbol Symbol { get; }
        public bool IsComputer => Type == PlayerType.Computer;
        
        public event Action<Vector2Int> OnMadeMove;

        public Player(PlayerType type, string name, Symbol symbol, IMoveStrategy strategy)
        {
            Type = type;
            Name = name;
            Symbol = symbol;
            _moveStrategy = strategy;

            _moveStrategy.OnMadeMove += InvokeOnMadeMoveEvent;
        }
        
        public void Dispose()
        {
            _moveStrategy.OnMadeMove -= InvokeOnMadeMoveEvent;
        }

        public void PrepareMove(Vector2Int? fieldPosition = null)
        {
            _moveStrategy.PrepareMove(fieldPosition);
        }

        //Instead of passing tick we could Inject ITickable to player created by zenject factory
        //But since we have just 2 players, for me it would be a little bit over-engineering
        public void Tick(float deltaTime)
        {
            _moveStrategy.Tick(deltaTime);
        }
        
        private void InvokeOnMadeMoveEvent(Vector2Int fieldPosition)
        {
            OnMadeMove?.Invoke(fieldPosition);
        }
    }
}