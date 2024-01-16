using GlassyCode.TTT.Game.States.Logic.Interfaces;
using Zenject;
using NUnit.Framework;
using GlassyCode.TTT.Game.TicTacToe.Data;
using GlassyCode.TTT.Game.TicTacToe.Data.Enums;
using GlassyCode.TTT.Game.TicTacToe.Logic.Players;
using GlassyCode.TTT.Game.TicTacToe.Logic.Signals;
using GlassyCode.TTT.Game.TicTacToe.Logic.Timers;
using GlassyCode.TTT.Tests.Mocks.Features.GameStates;
using GlassyCode.TTT.Tests.Mocks.Features.TicTacToe.Installers;
using GlassyCode.TTT.Tests.Mocks.Interfaces;

namespace GlassyCode.TTT.Tests.EditMode.Unit.TicTacToe
{
    [TestFixture]
    public class PlayersControllerTests : ZenjectUnitTestFixture
    {
        private IPlayersController _playersController;
        private ITurnTimer _timer;
        private ITicTacToeConfig _config;
        private SignalBus _signalBus;
        
        [SetUp]
        public void InitPlayers()
        {
            SignalsTestInstaller.Install(Container);
            CoreTestInstaller.Install(Container);
            GameStatesManagerTestInstaller.Install(Container);
            TurnTimerTestInstaller.Install(Container);
            TicTacToeConfigTestInstaller.Install(Container);
            BoardTestInstaller.Install(Container);
            PlayersControllerTestInstaller.Install(Container);

            _playersController = Container.Resolve<IPlayersController>();
            _timer = Container.Resolve<ITurnTimer>();
            _config = Container.Resolve<ITicTacToeConfig>();
            _signalBus = Container.Resolve<SignalBus>();

            if (_playersController is IInitializableTest initializable)
            {
                initializable.Initialize();
            }
        }

        [Test]
        public void InitPlayers_Test()
        {
            var playerInstantiated = false;

            _playersController.OnPlayersSwapped += x => playerInstantiated = true;
            _playersController.InitPlayers();

            Assert.IsTrue(playerInstantiated);
            Assert.IsNotNull(_playersController.CurrentPlayer);
            Assert.IsNotNull(_playersController.CurrentPlayer.Symbol);
            Assert.IsNotNull(_playersController.PlayerBySymbol(Symbol.O));
            Assert.IsNotNull(_playersController.PlayerBySymbol(Symbol.X));
        }
        
        [Test]
        public void SwapPlayers_Test()
        {
            _playersController.InitPlayers();
            var currentPlayer = _playersController.CurrentPlayer;
            _playersController.SwapPlayers();
            var swappedPlayer = _playersController.CurrentPlayer;
            Assert.AreNotSame(currentPlayer, swappedPlayer);
        }
        
        [Test]
        public void WinGameByTimeSignal_Fired_Time_Passed()
        {
            var firedSignal = false;
            _signalBus.Subscribe<WinGameByTimeSignal>(x => firedSignal = true);
            _playersController.InitPlayers();
            _timer.Tick(_config.SecondsToMakeMove);
            Assert.IsTrue(firedSignal);
            _signalBus.TryUnsubscribe<WinGameByTimeSignal>(x => firedSignal = true);
        }
        
        [Test]
        public void WinGameByTimeSignal_NotFired_Time_NotPassed()
        {
            var firedSignal = false;
            _signalBus.Subscribe<WinGameByTimeSignal>(x => firedSignal = true);
            _playersController.InitPlayers();
            _timer.Tick(_config.SecondsToMakeMove - 1);
            Assert.IsFalse(firedSignal);
            _signalBus.TryUnsubscribe<WinGameByTimeSignal>(x => firedSignal = true);
        }

        [TearDown]
        public void TearDown()
        {
            Container.UnbindAll();
        }
    }
}