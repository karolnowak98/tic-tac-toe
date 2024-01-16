using System.Collections.Generic;
using ModestTree;

namespace GlassyCode.TTT.Game.TicTacToe.Logic.Movement
{
    public readonly struct MoveHistory : IMoveHistory
    {
        private readonly Stack<Move> _moves;

        public MoveHistory(Stack<Move> moves)
        {
            _moves = moves;
        }

        public bool IsEmpty => _moves.IsEmpty();

        public Move? UndoMove()
        {
            if (!IsEmpty)
            {
                return _moves.Pop();
            }
            
            return null;
        } 
        
        public void AddMove(Move move) => _moves.Push(move);
        public void Clear() => _moves.Clear();
    }
}