using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using GlassyCode.TTT.Game.TicTacToe.Logic.Boards;
using GlassyCode.TTT.Game.TicTacToe.Logic.Movement.Strategies;

namespace GlassyCode.TTT.Tests.PlayMode.Unit.TicTacToe
{
    [TestFixture]
    public class ComputerMoveStrategyTests
    {
        private const int MinComputerThinkSeconds = 1;
        private const int MaxComputerThinkSeconds = 5;
        private readonly Vector2Int _fieldSizes = new Vector2Int(3, 3);
        
        private IBoard _board;
        private IMoveStrategy _computerMoveStrategy;
        
        [SetUp]
        public void InitMoveStrategies()
        {
            _board = new Board(_fieldSizes);
            _computerMoveStrategy = new ComputerMoveStrategy(_board,MinComputerThinkSeconds, MaxComputerThinkSeconds);
        }
        
        [UnityTest]
        public IEnumerator Computer_MakeMoveTest()
        {
            var madeMove = false;
            
            _computerMoveStrategy.OnMadeMove += (cellPos) =>
            {
                madeMove = true;
            };
            
            _computerMoveStrategy.PrepareMove();
            
            yield return new WaitForSeconds(MaxComputerThinkSeconds);

            _computerMoveStrategy.Tick(MaxComputerThinkSeconds);
            
            yield return new WaitForEndOfFrame();
            
            Assert.IsTrue(madeMove);
        }
    }
}