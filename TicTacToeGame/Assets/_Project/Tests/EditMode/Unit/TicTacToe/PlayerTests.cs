using UnityEngine;
using NUnit.Framework;
using GlassyCode.TTT.Game.TicTacToe.Data.Enums;
using GlassyCode.TTT.Game.TicTacToe.Logic.Boards;
using GlassyCode.TTT.Game.TicTacToe.Logic.Movement.Strategies;
using GlassyCode.TTT.Game.TicTacToe.Logic.Players;

namespace GlassyCode.TTT.Tests.EditMode.Unit.TicTacToe
{
    [TestFixture]
    public class PlayerTests
    {
        private readonly Vector2Int _fieldSizes = new Vector2Int(3, 3);
        private IPlayer _player;
        private IBoard _board;
        private IMoveStrategy _playerMoveStrategy;
        
        [SetUp]
        public void InitPlayer()
        {
            _board = new Board(_fieldSizes);
            _playerMoveStrategy = new HumanMoveStrategy(_board);
            _player = new Player(PlayerType.Human, "testName", Symbol.X, _playerMoveStrategy);
        }

        [Test]
        public void IsComputer_False()
        {
            Assert.IsFalse(_player.IsComputer);
        }

        [Test]
        public void MakeMove_Prepared_Success()
        {
            var madeMove = false;
            
            _player.PrepareMove();
            _player.OnMadeMove += PlayerOnOnMadeMove;
            _playerMoveStrategy.MakeMove();
            _player.OnMadeMove -= PlayerOnOnMadeMove;
            
            Assert.IsTrue(madeMove);
            return;

            void PlayerOnOnMadeMove(Vector2Int cellPos) => madeMove = true;
        }
        
        [Test]
        public void MakeMove_NotPrepared_Failure()
        {
            var madeMove = false;

            _player.OnMadeMove += PlayerOnOnMadeMove;
            _playerMoveStrategy.MakeMove();
            _player.OnMadeMove -= PlayerOnOnMadeMove;

            Assert.IsFalse(madeMove);
            return;
            
            void PlayerOnOnMadeMove(Vector2Int cellPos) => madeMove = true;
        }
    }
}