using UnityEngine;
using GlassyCode.TTT.Game.TicTacToe.Logic.Boards;

namespace GlassyCode.TTT.Game.TicTacToe.Logic.Movement.Strategies
{
    public class ComputerMoveStrategy : MoveStrategy
    {
        private float _moveTime;

        private readonly int _minThinkSeconds;
        private readonly int _maxThinkSeconds;
        
        private bool IsThinking => _moveTime > 0f;

        public ComputerMoveStrategy(IBoard board, int minThinkSeconds, int maxThinkSeconds) : base(board)
        {
            _minThinkSeconds = minThinkSeconds;
            _maxThinkSeconds = maxThinkSeconds;
        }

        public override void Tick(float deltaTime)
        {
            if (IsThinking)
            {
                _moveTime -= deltaTime;

                if (_moveTime <= 0f)
                {
                    MakeMove();
                }
            }
        }

        public override void PrepareMove(Vector2Int? cellPos = null)
        {
            base.PrepareMove(cellPos);
            _moveTime = Random.Range(_minThinkSeconds, _maxThinkSeconds);
        }
    }
}