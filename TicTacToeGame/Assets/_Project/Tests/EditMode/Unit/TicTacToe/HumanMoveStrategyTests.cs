using UnityEngine;
using NUnit.Framework;
using GlassyCode.TTT.Game.TicTacToe.Logic.Boards;
using GlassyCode.TTT.Game.TicTacToe.Logic.Movement.Strategies;

namespace GlassyCode.TTT.Tests.EditMode.Unit.TicTacToe
{
    [TestFixture]
    public class HumanMoveStrategyTests
    {
        private readonly Vector2Int _fieldSizes = new Vector2Int(3, 3);
        private IBoard _board;
        private IMoveStrategy _humanMoveStrategy;

        [SetUp]
        public void InitMoveStrategies()
        {
            _board = new Board(_fieldSizes);
            _humanMoveStrategy = new HumanMoveStrategy(_board);
        }

        [Test]
        public void HumanStrategy_MakeMoveTest()
        {
            var madeMove = false;
            var cellPosition = new Vector2Int(0, 0);
            
            _humanMoveStrategy.OnMadeMove += (cellPos) =>
            {
                madeMove = true;
            };
            _humanMoveStrategy.PrepareMove(cellPosition);
            _humanMoveStrategy.MakeMove();
            
            Assert.IsTrue(madeMove);
        }
    }
}