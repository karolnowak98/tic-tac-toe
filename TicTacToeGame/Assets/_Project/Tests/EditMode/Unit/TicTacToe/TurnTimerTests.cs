using NUnit.Framework;
using Zenject;
using GlassyCode.TTT.Game.TicTacToe.Logic.Timers;
using GlassyCode.TTT.Tests.Mocks.Features.TicTacToe.Installers;

namespace GlassyCode.TTT.Tests.EditMode.Unit.TicTacToe
{
    [TestFixture]
    public class TurnTimerTests : ZenjectUnitTestFixture
    {
        private ITurnTimer _turnTimer;
        
        [SetUp]
        public void Init()
        {
            TurnTimerTestInstaller.Install(Container);
            TicTacToeConfigTestInstaller.Install(Container);
            _turnTimer = Container.Resolve<ITurnTimer>();
        }

        [Test]
        public void OnTimePassed_Success()
        {
            var timePassed = false;
            
            const int secondsToWait = 6;
            _turnTimer.OnTimePassed += () => timePassed = true;
            _turnTimer.Start();
            _turnTimer.Tick(secondsToWait);
            
            Assert.IsTrue(timePassed);
        }
        
        [Test]
        public void OnTimePassed_Not_Enough_Seconds()
        {
            var timePassed = false;
            
            const int secondsToWait = 3;
            _turnTimer.OnTimePassed += () => timePassed = true;
            _turnTimer.Start();
            _turnTimer.Tick(secondsToWait);
            
            Assert.IsFalse(timePassed);
        }
        
        [Test]
        public void OnTurnTimeUpdated_Interval_Test()
        {
            var turnTimeUpdated = 0;
            
            const int secondsToWait = 5;
            _turnTimer.OnTurnTimeUpdated += (x) => turnTimeUpdated++;
            _turnTimer.Start();
            
            _turnTimer.Tick(1);
            _turnTimer.Tick(2);
            _turnTimer.Tick(3);
            _turnTimer.Tick(4);
            _turnTimer.Tick(secondsToWait);
            
            Assert.AreEqual(4, turnTimeUpdated);
        }
        
        [Test]
        public void Stop_Test()
        {
            var timePassed = false;
            
            const int secondsToWait = 5;
            _turnTimer.OnTimePassed += () => timePassed = true;
            _turnTimer.Start();
            _turnTimer.Tick(1);
            _turnTimer.Tick(2);
            _turnTimer.Stop();
            _turnTimer.Tick(3);
            _turnTimer.Tick(4);
            _turnTimer.Tick(secondsToWait);
            
            Assert.IsFalse(timePassed);
        }

        [TearDown]
        public void TearDown()
        {
            Container.UnbindAll();
        }
    }
}