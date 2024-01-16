using UnityEngine;

namespace GlassyCode.TTT.Game.TicTacToe.Logic.Signals
{
    public struct MakeMoveSignal
    {
        public MakeMoveSignal(Vector2Int cellPos)
        {
            CellPos = cellPos;
        }
        
        public Vector2Int CellPos { get; }
    }
}
