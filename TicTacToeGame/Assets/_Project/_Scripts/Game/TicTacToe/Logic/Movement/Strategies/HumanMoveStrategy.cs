using UnityEngine;
using GlassyCode.TTT.Game.TicTacToe.Logic.Boards;

namespace GlassyCode.TTT.Game.TicTacToe.Logic.Movement.Strategies
{
    public class HumanMoveStrategy : MoveStrategy
    {
        private bool _canMove;
        
        public HumanMoveStrategy(IBoard board) : base(board)
        {
        }

        public override void MakeMove()
        {
            if (_canMove)
            {
                base.MakeMove();
                _canMove = false;
            }
        }

        public override void PrepareMove(Vector2Int? cellPos = null)
        {
            base.PrepareMove(cellPos);
            _canMove = true;
        }
    }
}
