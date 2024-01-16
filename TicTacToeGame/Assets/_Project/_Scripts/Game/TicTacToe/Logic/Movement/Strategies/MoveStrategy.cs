using System;
using UnityEngine;
using GlassyCode.TTT.Game.TicTacToe.Logic.Boards;

namespace GlassyCode.TTT.Game.TicTacToe.Logic.Movement.Strategies
{
    public abstract class MoveStrategy : IMoveStrategy
    {
        private readonly IBoard _board;
        private Vector2Int _fieldPosition;

        public event Action<Vector2Int> OnMadeMove;
        
        protected MoveStrategy(IBoard board)
        {
            _board = board;
        }

        public virtual void Tick(float deltaTime)
        {
        }
        
        public virtual void PrepareMove(Vector2Int? cellPos = null)
        {
            SetUpCellPosition(cellPos);
        }

        public virtual void MakeMove()
        {
            OnMadeMove?.Invoke(_fieldPosition);
        }

        private void SetUpCellPosition(Vector2Int? cellPos)
        {
            if (cellPos.HasValue)
            {
                _fieldPosition = cellPos.Value;
            }
            
            else
            {
                var randomEmptyCell = _board.GetRandomEmptyField();

                if (randomEmptyCell == null)
                    return;
                
                _fieldPosition = randomEmptyCell.Value;
            }
        }
    }
}