using UnityEngine;
using GlassyCode.TTT.Game.TicTacToe.Data.Enums;

namespace GlassyCode.TTT.Game.TicTacToe.Data
{
    public interface ITicTacToeConfig
    {
        Vector2Int BoardSize { get; }
        int SecondsToMakeMove { get; }
        int ComputerMinThinkSeconds { get; }
        int ComputerMaxThinkSeconds { get; }
        float TurnTimerUIRefreshInterval { get; }
        Symbol FirstMoveSymbol { get; }
        string FirstPlayerName { get; }
        string SecondPlayerName { get; }
        string FirstComputerName { get; }
        string SecondComputerName { get; }
    }
}