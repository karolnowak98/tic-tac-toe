using UnityEngine;
using GlassyCode.TTT.Game.TicTacToe.Data.Enums;

namespace GlassyCode.TTT.Game.TicTacToe.Logic
{
    public static class TicTacToeExtensions
    {
        public static Symbol GetRandomSymbol => Random.Range(0, 2) == 0 ? Symbol.O : Symbol.X;
    }
}
