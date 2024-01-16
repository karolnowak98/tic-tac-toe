using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GlassyCode.TTT.Game.TicTacToe.Data
{
    //That represents all winning coords, that's more scalable approach
    //But probably faster, with no allocation and more efficient could be checking each and returning if found
    public static class WinningCoords
    {
        // Board Positions
        // [0,0] [0,1] [0,2]
        // [1,0] [1,1] [1,2]
        // [2,0] [2,1] [2,2]
        
        private static readonly Dictionary<Vector2Int[], int[]> LinePositions = new Dictionary<Vector2Int[], int[]>  
        {
            // { winningCoords, {xPos, yPos, zRot} }
            { new [] {new Vector2Int(0, 0), new Vector2Int(0,1), new Vector2Int(0,2)}, new[] {0, 305, 90} },
            { new [] {new Vector2Int(1, 0), new Vector2Int(1,1), new Vector2Int(1,2)}, new[] {0, 0, 90} },
            { new [] {new Vector2Int(2, 0), new Vector2Int(2,1), new Vector2Int(2,2)}, new[] {0, -305, 90} },
            { new [] {new Vector2Int(0, 0), new Vector2Int(1,0), new Vector2Int(2,0)}, new[] {-305, 0, 0} },
            { new [] {new Vector2Int(0, 1), new Vector2Int(1,1), new Vector2Int(2,1)}, new[] {0, 0, 0} },
            { new [] {new Vector2Int(0, 2), new Vector2Int(1,2), new Vector2Int(2,2)}, new[] {305, 0, 0} },
            { new [] {new Vector2Int(0, 0), new Vector2Int(1,1), new Vector2Int(2,2)}, new[] {0, 0, 45} },
            { new [] {new Vector2Int(0, 2), new Vector2Int(1,1), new Vector2Int(2,0)}, new[] {0, 0, -45} },
        };

        public static int[] GetWinningLinePositions(Vector2Int[] coords)
        {
            return (from winningCoords in LinePositions.Keys 
                where coords.SequenceEqual(winningCoords) 
                select LinePositions[winningCoords]).FirstOrDefault();
        }
    }
}