using System.Collections.Generic;
using GlassyCode.TTT.Game.States.Data;
using UnityEngine;
using Zenject;
using NUnit.Framework;
using GlassyCode.TTT.Game.TicTacToe.Data;
using GlassyCode.TTT.Game.TicTacToe.Logic;
using GlassyCode.TTT.Game.TicTacToe.Logic.Boards;
using GlassyCode.TTT.Game.TicTacToe.Logic.Players;
using GlassyCode.TTT.Game.TicTacToe.Logic.Timers;
using GlassyCode.TTT.Tests.Mocks.Features.GameStates;
using GlassyCode.TTT.Tests.Mocks.Features.TicTacToe.Installers;
using GlassyCode.TTT.Tests.Mocks.Interfaces;

namespace GlassyCode.TTT.Tests.EditMode.Unit.TicTacToe
{
    [TestFixture]
    public class TicTacToeManagerTests : ZenjectUnitTestFixture
    {
        private ITicTacToeManager _ticTacToeManager;
        private ITicTacToeConfig _config;
        private IPlayersController _playersController;
        private IBoard _board;
        private ITurnTimer _turnTimer;
        
        [SetUp]
        public void InitTicTacToeManager()
        {
            SignalsTestInstaller.Install(Container);
            CoreTestInstaller.Install(Container);
            GameStatesManagerTestInstaller.Install(Container);
            TicTacToeConfigTestInstaller.Install(Container);
            BoardTestInstaller.Install(Container);
            TurnTimerTestInstaller.Install(Container);
            PlayersControllerTestInstaller.Install(Container);
            TicTacToeManagerTestInstaller.Install(Container);

            _ticTacToeManager = Container.Resolve<ITicTacToeManager>();
            _playersController = Container.Resolve<IPlayersController>();
            _config = Container.Resolve<ITicTacToeConfig>();
            _board = Container.Resolve<IBoard>();
            _turnTimer = Container.Resolve<ITurnTimer>();
            

            if (_ticTacToeManager is IInitializableTest ticTacInitializable)
            {
                ticTacInitializable.Initialize();
            }

            if (_playersController is IInitializableTest playersInitializable)
            {
                playersInitializable.Initialize();
            }
        }
        
        [Test]
        [TestCaseSource(nameof(MakeMoveSuccessTestCases))]
        public void MakeMove_Success(Vector2Int firstMove, Vector2Int secondMove, Vector2Int thirdMove)
        {
            Vector2Int? resultCellPos = null;
            IPlayer resultPlayer = null;
            var didMove = false;

            _ticTacToeManager.OnMoveMade += OnMadeMove;
            
            var expectedPlayer = _playersController.CurrentPlayer;
            _ticTacToeManager.MakeMove(firstMove);
            
            Assert.IsNotNull(resultCellPos);
            Assert.AreEqual(resultCellPos, firstMove);
            Assert.IsNotNull(resultPlayer);
            Assert.AreSame(resultPlayer, expectedPlayer);
            Assert.IsTrue(didMove);
            
            didMove = false;
            expectedPlayer = _playersController.CurrentPlayer;
            _ticTacToeManager.MakeMove(secondMove);
            
            Assert.IsNotNull(resultCellPos);
            Assert.AreEqual(resultCellPos, secondMove);
            Assert.IsNotNull(resultPlayer);
            Assert.AreSame(resultPlayer, expectedPlayer);
            Assert.IsTrue(didMove);

            didMove = false;
            expectedPlayer = _playersController.CurrentPlayer;
            _ticTacToeManager.MakeMove(thirdMove);
            
            Assert.IsNotNull(resultCellPos);
            Assert.AreEqual(resultCellPos, thirdMove);
            Assert.IsNotNull(resultPlayer);
            Assert.AreSame(resultPlayer, expectedPlayer);
            Assert.IsTrue(didMove);
            
            _ticTacToeManager.OnMoveMade -= OnMadeMove;
            return;

            void OnMadeMove(IPlayer player, Vector2Int cellPos)
            {
                resultPlayer = player;
                resultCellPos = cellPos;
                didMove = true;
            }
        }
        
        private static IEnumerable<TestCaseData> MakeMoveSuccessTestCases()
        {
            var firstCellPos = new Vector2Int(0, 0);
            var secondCellPos = new Vector2Int(0, 1);
            var thirdCellPos = new Vector2Int(2, 1);
            
            yield return new TestCaseData(firstCellPos, secondCellPos, thirdCellPos)
                .SetName($"Test Case 1 - First cell pos: {firstCellPos}, Second cell pos: {secondCellPos}, Third cell pos: {thirdCellPos}");

            firstCellPos = new Vector2Int(2, 0);
            secondCellPos = new Vector2Int(1, 0);
            thirdCellPos = new Vector2Int(0, 1);
            
            yield return new TestCaseData(firstCellPos, secondCellPos, thirdCellPos)
                .SetName($"Test Case 2 - First cell pos: {firstCellPos}, Second cell pos: {secondCellPos}, Third cell pos: {thirdCellPos}");
            
            firstCellPos = new Vector2Int(2, 2);
            secondCellPos = new Vector2Int(0, 1);
            thirdCellPos = new Vector2Int(0, 0);
            
            yield return new TestCaseData(firstCellPos, secondCellPos, thirdCellPos)
                .SetName($"Test Case 3 - First cell pos: {firstCellPos}, Second cell pos: {secondCellPos}, Third cell pos: {thirdCellPos}");
            
            firstCellPos = new Vector2Int(1, 2);
            secondCellPos = new Vector2Int(1, 1);
            thirdCellPos = new Vector2Int(0, 1);
            
            yield return new TestCaseData(firstCellPos, secondCellPos, thirdCellPos)
                .SetName($"Test Case 4 - First cell pos: {firstCellPos}, Second cell pos: {secondCellPos}, Third cell pos: {thirdCellPos}");
        }
        
        [Test]
        [TestCaseSource(nameof(MakeMoveFailedTestCases))]
        public void MakeMove_Failed(Vector2Int firstMove, Vector2Int secondMove, Vector2Int thirdMove)
        {
            var didMove = false;
            
            _ticTacToeManager.OnMoveMade += OnMadeMove;
            _ticTacToeManager.MakeMove(firstMove);
            didMove = false;
            
            _ticTacToeManager.MakeMove(secondMove);
            
            if (firstMove == secondMove)
            {
                Assert.IsFalse(didMove);
                return;
            }

            didMove = false;
            _ticTacToeManager.MakeMove(thirdMove);
            _ticTacToeManager.OnMoveMade -= OnMadeMove;
            
            if (secondMove == thirdMove)
            {
                Assert.IsFalse(didMove);
                return;
            }

            return;

            void OnMadeMove(IPlayer player, Vector2Int cellPos)
            {
                didMove = true;
            }
        }
        
        private static IEnumerable<TestCaseData> MakeMoveFailedTestCases()
        {
            var firstCellPos = new Vector2Int(0, 0);
            var secondCellPos = new Vector2Int(1, 2);
            var thirdCellPos = new Vector2Int(0, 0);
            
            yield return new TestCaseData(firstCellPos, secondCellPos, thirdCellPos)
                .SetName($"Test Case 1 - First cell pos: {firstCellPos}, Second cell pos: {secondCellPos}, Third cell pos: {thirdCellPos}");

            firstCellPos = new Vector2Int(2, 2);
            secondCellPos = new Vector2Int(2, 2);
            thirdCellPos = new Vector2Int(1, 1);
            
            yield return new TestCaseData(firstCellPos, secondCellPos, thirdCellPos)
                .SetName($"Test Case 2 - First cell pos: {firstCellPos}, Second cell pos: {secondCellPos}, Third cell pos: {thirdCellPos}");
            
            firstCellPos = new Vector2Int(1, 2);
            secondCellPos = new Vector2Int(0, 1);
            thirdCellPos = new Vector2Int(1, 2);
            
            yield return new TestCaseData(firstCellPos, secondCellPos, thirdCellPos)
                .SetName($"Test Case 3 - First cell pos: {firstCellPos}, Second cell pos: {secondCellPos}, Third cell pos: {thirdCellPos}");
            
            firstCellPos = new Vector2Int(0, 0);
            secondCellPos = new Vector2Int(2, 0);
            thirdCellPos = new Vector2Int(2, 0);
            
            yield return new TestCaseData(firstCellPos, secondCellPos, thirdCellPos)
                .SetName($"Test Case 4 - First cell pos: {firstCellPos}, Second cell pos: {secondCellPos}, Third cell pos: {thirdCellPos}");
        }
        
        [Test]
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(2, 0)]
        public void UndoMove_Success(int cellXPos, int cellYPos)
        {
            Vector2Int? resultCellPos = null;
            var didUndoMove = false;
            var expectedCellPos = new Vector2Int(cellXPos, cellYPos);
            
            _ticTacToeManager.MakeMove(expectedCellPos);
            _ticTacToeManager.OnMoveUndid += OnMoveUndid;
            _ticTacToeManager.UndoMove();
            _ticTacToeManager.OnMoveUndid -= OnMoveUndid;
            
            Assert.IsTrue(didUndoMove);
            Assert.IsNotNull(resultCellPos);
            Assert.AreEqual(resultCellPos, expectedCellPos);
            return;

            void OnMoveUndid(Vector2Int cellPos)
            {
                resultCellPos = cellPos;
                didUndoMove = true;
            }
        }
        
        [Test]
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(2, 0)]
        public void UndoMove_Failed_Empty_Move_History(int cellXPos, int cellYPos)
        {
            Vector2Int? resultCellPos = null;
            var didUndoMove = false;
            var expectedCellPos = new Vector2Int(cellXPos, cellYPos);

            _ticTacToeManager.OnMoveUndid += OnMoveUndid;
            _ticTacToeManager.UndoMove();
            _ticTacToeManager.OnMoveUndid -= OnMoveUndid;
            
            Assert.IsFalse(didUndoMove);
            Assert.IsNull(resultCellPos);
            Assert.AreNotEqual(resultCellPos, expectedCellPos);
            return;

            void OnMoveUndid(Vector2Int cellPos)
            {
                resultCellPos = cellPos;
                didUndoMove = true;
            }
        }
        
        [Test]
        [TestCase(6)]
        [TestCase(3213)]
        [TestCase(10)]
        public void GameWin_By_Time(int secondsToMakeMove)
        {
            var isGameFinished = false;

            _ticTacToeManager.OnGameFinished += OnGameFinished;
            _turnTimer.Tick(secondsToMakeMove); //Waiting to turn time pass
            _ticTacToeManager.OnGameFinished -= OnGameFinished;
            
            Assert.IsTrue(isGameFinished);
            return;

            void OnGameFinished(IPlayer player, Vector2Int[] lines) => isGameFinished = true;
        }
        
        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(4)]
        public void GameWin_By_Time_Failed(int secondsToMakeMove)
        {
            var isGameFinished = false;
            var waitSeconds = _config.SecondsToMakeMove - 1;

            _ticTacToeManager.OnGameFinished += OnGameFinished;
            _turnTimer.Tick(waitSeconds); //Waiting to turn time pass, but it's not enough
            _ticTacToeManager.OnGameFinished -= OnGameFinished;
            
            Assert.IsFalse(isGameFinished);
            return;

            void OnGameFinished(IPlayer player, Vector2Int[] lines) => isGameFinished = true;
        }
        
        [TestCaseSource(nameof(GetTestCases))]
        public void GameWin_By_Move(Vector2Int[] moves)
        {
            var isGameFinished = false;
            IPlayer winPlayer = null;

            _ticTacToeManager.OnGameFinished += OnGameFinished;

            foreach (var move in moves)
            {
                _ticTacToeManager.MakeMove(move);
            }

            _ticTacToeManager.OnGameFinished -= OnGameFinished;

            Assert.IsTrue(isGameFinished);
            Assert.IsNotNull(winPlayer);
            return;

            void OnGameFinished(IPlayer player, Vector2Int[] lines)
            {
                winPlayer = player;
                isGameFinished = true;
            }
        }

        private static IEnumerable<TestCaseData> GetTestCases()
        {
            yield return new TestCaseData(new[]
                {
                    new Vector2Int(0, 0), 
                    new Vector2Int(0, 1), 
                    new Vector2Int(1, 1), 
                    new Vector2Int(2, 0), 
                    new Vector2Int(2, 2)
                })
                .SetName("Test Case 1 - Diagonally");

            yield return new TestCaseData(new[]
                {
                    new Vector2Int(0, 2), 
                    new Vector2Int(0, 1), 
                    new Vector2Int(1, 2), 
                    new Vector2Int(2, 0), 
                    new Vector2Int(2, 2)
                })
                .SetName("Test Case 2 - Vertically");
            
            yield return new TestCaseData(new[]
                {
                    new Vector2Int(1, 0), 
                    new Vector2Int(0, 1), 
                    new Vector2Int(1, 1), 
                    new Vector2Int(2, 0), 
                    new Vector2Int(1, 2)
                })
                .SetName("Test Case 3 - Horizontally");
            
            yield return new TestCaseData(new[]
                {
                    new Vector2Int(0, 0), 
                    new Vector2Int(0, 1), 
                    new Vector2Int(1, 0), 
                    new Vector2Int(0, 2), 
                    new Vector2Int(1, 1),
                    new Vector2Int(1, 2),
                    new Vector2Int(2, 2),
                    new Vector2Int(2, 1),
                    new Vector2Int(2, 0)
                })
                .SetName("Test Case 4 - Made all moves");
        }
        
        [TestCaseSource(nameof(GetDrawCases))]
        public void GameDraw_Test(Vector2Int[] moves)
        {
            var isGameFinished = false;
            Vector2Int[] expectedLines = null;
            IPlayer winPlayer = null;

            _ticTacToeManager.OnGameFinished += OnGameFinished;

            foreach (var move in moves)
            {
                _ticTacToeManager.MakeMove(move);
            }

            _ticTacToeManager.OnGameFinished -= OnGameFinished;

            Assert.IsTrue(isGameFinished);
            Assert.IsNull(winPlayer);
            Assert.IsNull(expectedLines);
            return;

            void OnGameFinished(IPlayer player, Vector2Int[] lines)
            {
                expectedLines = lines;
                winPlayer = player;
                isGameFinished = true;
            }
        }

        private static IEnumerable<TestCaseData> GetDrawCases()
        {
            yield return new TestCaseData(new[]
                {
                    new Vector2Int(0, 0), 
                    new Vector2Int(0, 1), 
                    new Vector2Int(0, 2), 
                    new Vector2Int(1, 0), 
                    new Vector2Int(1, 1),
                    new Vector2Int(2, 2),
                    new Vector2Int(2, 1),
                    new Vector2Int(2, 0),
                    new Vector2Int(1, 2)
                })
                .SetName("Test Case 1");

            yield return new TestCaseData(new[]
                {
                    new Vector2Int(2, 2), 
                    new Vector2Int(2, 1), 
                    new Vector2Int(2, 0), 
                    new Vector2Int(1, 2), 
                    new Vector2Int(1, 1),
                    new Vector2Int(0, 0),
                    new Vector2Int(1, 0),
                    new Vector2Int(0, 2),
                    new Vector2Int(0, 1),
                })
                .SetName("Test Case 2");
            
            yield return new TestCaseData(new[]
                {
                    new Vector2Int(1, 1), 
                    new Vector2Int(1, 0), 
                    new Vector2Int(0, 1), 
                    new Vector2Int(2, 1), 
                    new Vector2Int(0, 0),
                    new Vector2Int(0, 2),
                    new Vector2Int(1, 2),
                    new Vector2Int(2, 2),
                    new Vector2Int(2, 0),
                })
                .SetName("Test Case 3");
        }
        
        [Test]
        public void ResetGame_Test()
        {
            var isGameReset = false;

            _ticTacToeManager.OnGameReset += TicTacToeManagerOnOnGameFinished;
            
            _ticTacToeManager.ResetGame();
            
            _ticTacToeManager.OnGameReset -= TicTacToeManagerOnOnGameFinished;
            
            Assert.IsTrue(isGameReset);
            return;

            void TicTacToeManagerOnOnGameFinished() => isGameReset = true;
        }
        
        [Test]
        public void GetHint_Test()
        {
            var firstMove = new Vector2Int(1, 1);
            var secondsMove = new Vector2Int(2, 2);

            var hintPos = _ticTacToeManager.GetHint;
            Assert.IsFalse(_playersController.IsComputerTurn);
            Assert.IsNotNull(hintPos);
            Assert.IsFalse(_board.IsFieldOccupiedAtPosition(hintPos.Value));

            _ticTacToeManager.MakeMove(firstMove);
            
            hintPos = _ticTacToeManager.GetHint;
            Assert.IsFalse(_playersController.IsComputerTurn);
            Assert.IsNotNull(hintPos);
            Assert.IsFalse(_board.IsFieldOccupiedAtPosition(hintPos.Value));
            
            
            _ticTacToeManager.MakeMove(secondsMove);
            
            hintPos = _ticTacToeManager.GetHint;
            Assert.IsFalse(_playersController.IsComputerTurn);
            Assert.IsNotNull(hintPos);
            Assert.IsFalse(_board.IsFieldOccupiedAtPosition(hintPos.Value));
        }
        
        [TearDown]
        public void TearDown()
        {
            Container.UnbindAll();
        }
    }
}