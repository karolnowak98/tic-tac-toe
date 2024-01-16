using UnityEngine;
using GlassyCode.TTT.Game.TicTacToe.Data.Enums;

namespace GlassyCode.TTT.Game.TicTacToe.Data
{
    [CreateAssetMenu(menuName = "Configs/Tic Tac Toe Config", fileName = "Tic Tac Toe Config")]
    public class TicTacToeConfig : ScriptableObject, ITicTacToeConfig
    {
        //Whole look of config would be better in Odin of course. Using groups, enumPaging etc.
        [Header("Logic")]
        [SerializeField, Tooltip("Grid sizes for the Tic Tac Toe board.")]
        private Vector2Int _boardSize;

        [SerializeField, Range(1, 60), Tooltip("Seconds allowed to make a move.")]
        private int _secondsToMakeMove;

        [SerializeField, Range(1, 30), Tooltip("Minimum seconds for the computer to think.")]
        private int _computerMinThinkSeconds;

        [SerializeField, Range(1, 60), Tooltip("Maximum seconds for the computer to think.")]
        private int _computerMaxThinkSeconds;

        [SerializeField, Tooltip("Turn timer text refresh Interval")]
        private float _turnTimerUIRefreshInterval;
        
        [SerializeField, Tooltip("Symbol for the first turn.")]
        private Symbol _firstTurn; 

        //Those strings are optional, in a better way strings could be declared for instance in I2Loc. But we at least avoid some magic strings. 
        [SerializeField, Tooltip("Default name for the first player.")]
        private string _player1Name;

        [SerializeField, Tooltip("Default name for the second player.")]
        private string _player2Name;

        [SerializeField, Tooltip("Default name for the first computer.")]
        private string _firstComputerName;

        [SerializeField, Tooltip("Default name for the second computer.")]
        private string _secondComputerName;

        public Vector2Int BoardSize => _boardSize;
        public int SecondsToMakeMove => _secondsToMakeMove;
        public int ComputerMinThinkSeconds => _computerMinThinkSeconds;
        public int ComputerMaxThinkSeconds => _computerMaxThinkSeconds;
        public float TurnTimerUIRefreshInterval => _turnTimerUIRefreshInterval;
        public Symbol FirstMoveSymbol => _firstTurn;

        public string FirstPlayerName => _player1Name;
        public string SecondPlayerName => _player2Name;
        public string FirstComputerName => _firstComputerName;
        public string SecondComputerName => _secondComputerName;
    }
}