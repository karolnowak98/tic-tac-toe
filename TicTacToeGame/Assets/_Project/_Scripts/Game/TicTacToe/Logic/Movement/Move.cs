using UnityEngine;

namespace GlassyCode.TTT.Game.TicTacToe.Logic.Movement
{
    public struct Move
    {
        public Vector2Int CellPos { get; }
        
        public Move(Vector2Int cellPos)
        {
            CellPos = cellPos;
        }
    }
}