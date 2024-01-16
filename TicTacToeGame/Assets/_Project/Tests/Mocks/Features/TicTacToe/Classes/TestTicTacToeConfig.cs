using UnityEngine;
using GlassyCode.TTT.Game.TicTacToe.Data;
using GlassyCode.TTT.Game.TicTacToe.Data.Enums;

namespace GlassyCode.TTT.Tests.Mocks.Features.TicTacToe.Classes
{
    public class TestTicTacToeConfig : ITicTacToeConfig
    {
        public Vector2Int BoardSize => new Vector2Int(3, 3);
        public int SecondsToMakeMove => 5;
        public int ComputerMinThinkSeconds  => 1;
        public int ComputerMaxThinkSeconds  => 5;
        public float TurnTimerUIRefreshInterval  => 0.1f;
        public Symbol FirstMoveSymbol  => Symbol.X;
        public string FirstPlayerName  => "First player test name";
        public string SecondPlayerName  => "Second player test name";
        public string FirstComputerName  => "Chicken test";
        public string SecondComputerName  => "Monkey test";
    }
}