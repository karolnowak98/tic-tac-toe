using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using GlassyCode.TTT.Game.TicTacToe.Logic.Movement;

namespace GlassyCode.TTT.Tests.EditMode.Unit.TicTacToe
{
    [TestFixture]
    public class MoveHistoryTests
    {
        private IMoveHistory _moveHistory;

        [SetUp]
        public void InitMoveHistory()
        {
            _moveHistory = new MoveHistory(new Stack<Move>());
        }

        [Test]
        public void IsEmpty_True()
        {
            Assert.IsTrue(_moveHistory.IsEmpty);
        }

        [Test]
        public void IsEmpty_False()
        {
            _moveHistory.AddMove(new Move());
            Assert.IsFalse(_moveHistory.IsEmpty);
        }

        [Test]
        public void Clear_Test()
        {
            _moveHistory.AddMove(new Move());
            _moveHistory.AddMove(new Move());
            _moveHistory.Clear();
            Assert.IsTrue(_moveHistory.IsEmpty);
        }

        [Test]
        public void AddMove_Test()
        {
            var cellPos = new Vector2Int(1, 1);
            _moveHistory.AddMove(new Move(cellPos));
            Assert.IsFalse(_moveHistory.IsEmpty);
        }
        
        [Test]
        public void UndoMove_Success()
        {
            var cellPos = new Vector2Int(1, 1);
            _moveHistory.AddMove(new Move(cellPos));
            _moveHistory.UndoMove();
            Assert.IsTrue(_moveHistory.IsEmpty);
        }
        
        [Test]
        public void UndoMove_Failure()
        {
            _moveHistory.Clear();
            var lastMove = _moveHistory.UndoMove();
            Assert.IsNull(lastMove);
        }
    }
}