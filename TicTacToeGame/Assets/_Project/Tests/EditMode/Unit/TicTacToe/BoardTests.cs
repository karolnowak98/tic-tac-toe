using UnityEngine;
using NUnit.Framework;
using GlassyCode.TTT.Game.TicTacToe.Data.Enums;
using GlassyCode.TTT.Game.TicTacToe.Logic.Boards;

namespace GlassyCode.TTT.Tests.EditMode.Unit.TicTacToe
{
    [TestFixture]
    public class BoardTests
    {
        private Vector2Int _fieldSizes;
        private IBoard _board;
        
        [SetUp]
        public void InitBoard()
        {
            _fieldSizes = new Vector2Int(3, 3);
            _board = new Board(_fieldSizes);
            _board.Reset();
        }

        [Test]
        public void ResetBoard_Success()
        {
            _board.Reset();
        }
        
        [Test]
        public void GetRandomEmptyField_Success()
        {
            var randomEmptyField = _board.GetRandomEmptyField();
            
            Assert.IsNotNull(randomEmptyField);
        }
        
        [Test]
        public void GetRandomEmptyField_NotFound()
        {
            for (var x = 0; x < _fieldSizes.x; x++)
            {
                for (var y = 0; y < _fieldSizes.y; y++)
                {
                    _board.SetSymbolAtPosition(new Vector2Int(x, y), Symbol.O);
                }
            }
            
            var randomEmptyField = _board.GetRandomEmptyField();

            Assert.IsNull(randomEmptyField);
        }

        [Test]
        public void GetWinningCoords_Success()
        {
            const Symbol symbol = Symbol.O;
            var winningCoords = new[] { new Vector2Int(0, 0), 
                new Vector2Int(1, 1), new Vector2Int(2, 2) };
            
            _board.SetSymbolAtPosition(winningCoords[0], symbol);
            _board.SetSymbolAtPosition(winningCoords[1], symbol);
            _board.SetSymbolAtPosition(winningCoords[2], symbol);

            Assert.AreEqual(_board.GetWinningCoords(symbol), winningCoords);
        }
        
        [Test]
        public void GetWinningCoords_NotFound()
        {
            const Symbol symbol = Symbol.O;
            var winningCoords = new[] { new Vector2Int(0, 0), 
                new Vector2Int(1, 1), new Vector2Int(1, 2) };
            
            _board.SetSymbolAtPosition(winningCoords[0], symbol);
            _board.SetSymbolAtPosition(winningCoords[1], symbol);
            _board.SetSymbolAtPosition(winningCoords[2], symbol);

            Assert.IsNull(_board.GetWinningCoords(symbol));
        }
        
        [Test]
        public void CheckWinForPlayers_PlayerWin()
        {
            const Symbol symbol = Symbol.O;
            var winningCoords = new[] { new Vector2Int(0, 0), 
                new Vector2Int(1, 1), new Vector2Int(2, 2) };

            
            _board.SetSymbolAtPosition(winningCoords[0], symbol);
            _board.SetSymbolAtPosition(winningCoords[1], symbol);
            _board.SetSymbolAtPosition(winningCoords[2], symbol);
            
            Assert.AreEqual(_board.CheckWinForPlayers(), symbol);
        }
        
        [Test]
        public void CheckWinForPlayers_PlayerNotWin()
        {
            const Symbol symbol = Symbol.O;
            var winningCoords = new[] { new Vector2Int(0, 0), 
                new Vector2Int(1, 1), new Vector2Int(1, 2) };
            
            _board.SetSymbolAtPosition(winningCoords[0], symbol);
            _board.SetSymbolAtPosition(winningCoords[1], symbol);
            _board.SetSymbolAtPosition(winningCoords[2], symbol);

            Assert.AreEqual(_board.CheckWinForPlayers(), Symbol.None);
        }
        
        [Test]
        public void SetSymbolAtPosition_Success()
        {
            const Symbol symbol = Symbol.O;
            var fieldPos = new Vector2Int(0, 0);
            
            _board.SetSymbolAtPosition(fieldPos, Symbol.None);
            _board.SetSymbolAtPosition(fieldPos, symbol);
            
            Assert.IsTrue(_board.IsFieldOccupiedAtPosition(fieldPos));
        }
        
        [Test]
        public void SetSymbolAtPosition_Occupied()
        {
            const Symbol symbol = Symbol.O;
            var fieldPos = new Vector2Int(0, 0);
            
            _board.SetSymbolAtPosition(fieldPos, Symbol.X);
            _board.SetSymbolAtPosition(fieldPos, symbol);
            
            Assert.AreNotEqual(_board.GetSymbolAtPosition(fieldPos), symbol);
        }
        
        [Test]
        public void MarkAsEmptyAtPosition_Test()
        {
            var fieldPos = new Vector2Int(0, 0);
            
            _board.SetSymbolAtPosition(fieldPos, Symbol.X);
            Assert.AreEqual(_board.GetSymbolAtPosition(fieldPos), Symbol.X);
            _board.MarkAsEmptyAtPosition(fieldPos);
            Assert.AreEqual(_board.GetSymbolAtPosition(fieldPos), Symbol.None);
        }
        
        [Test]
        public void IsFieldOccupiedAtPosition_Test()
        {
            var fieldPos = new Vector2Int(0, 0);
            
            _board.SetSymbolAtPosition(fieldPos, Symbol.None);
            Assert.IsFalse(_board.IsFieldOccupiedAtPosition(fieldPos));
            _board.SetSymbolAtPosition(fieldPos, Symbol.X);
            Assert.IsTrue(_board.IsFieldOccupiedAtPosition(fieldPos));
        }
        
        [Test]
        public void GetSymbolAtPosition_Test()
        {
            const Symbol noneSymbol = Symbol.None;
            const Symbol xSymbol = Symbol.X;
            var fieldPos = new Vector2Int(0, 0);
            
            _board.SetSymbolAtPosition(fieldPos, noneSymbol);
            Assert.AreEqual(_board.GetSymbolAtPosition(fieldPos), noneSymbol);
            _board.SetSymbolAtPosition(fieldPos, xSymbol);
            Assert.AreEqual(_board.GetSymbolAtPosition(fieldPos), xSymbol);
        }
        
        [Test]
        public void IsFull_True()
        {
            for (var x = 0; x < _fieldSizes.x; x++)
            {
                for (var y = 0; y < _fieldSizes.y; y++)
                {
                    _board.SetSymbolAtPosition(new Vector2Int(x, y), Symbol.O);
                }
            }
            
            Assert.IsTrue(_board.IsFull());
        }
        
        [Test]
        public void IsFull_False()
        {
            var fieldPos = new Vector2Int(0, 0);
            
            for (var x = 0; x < _fieldSizes.x; x++)
            {
                for (var y = 0; y < _fieldSizes.y; y++)
                {
                    _board.SetSymbolAtPosition(new Vector2Int(x, y), Symbol.O);
                }
            }
            
            _board.MarkAsEmptyAtPosition(fieldPos);
            Assert.IsFalse(_board.IsFull());
        }
    }
}