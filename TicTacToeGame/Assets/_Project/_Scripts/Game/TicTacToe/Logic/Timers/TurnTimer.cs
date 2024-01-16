using System;
using Zenject;
using GlassyCode.TTT.Game.TicTacToe.Data;

namespace GlassyCode.TTT.Game.TicTacToe.Logic.Timers
{
    public class TurnTimer : ITurnTimer
    {
        private int _secondsToMakeMove;
        private float _timerUIRefreshInterval;
        private float _elapsedTimeSinceUIUpdate;
        private float _remainingTime;
        private bool _isTimerRunning;

        public event Action<float> OnTurnTimeUpdated;
        public event Action OnTimePassed;

        [Inject]
        private void Construct(ITicTacToeConfig config)
        {
            _secondsToMakeMove = config.SecondsToMakeMove;
            _timerUIRefreshInterval = config.TurnTimerUIRefreshInterval;
        }
        
        public void Tick(float deltaTime)
        {
            if (!_isTimerRunning)
                return;

            _remainingTime -= deltaTime;
            _elapsedTimeSinceUIUpdate += deltaTime;

            if (_elapsedTimeSinceUIUpdate >= _timerUIRefreshInterval)
            {
                OnTurnTimeUpdated?.Invoke(_remainingTime);
                _elapsedTimeSinceUIUpdate = 0;
            }
            
            if (_remainingTime <= 0)
            {
                Stop();
                OnTimePassed?.Invoke();
                _remainingTime = _secondsToMakeMove;
            }
        }
        
        public void Start()
        {
            _isTimerRunning = true;
            _remainingTime = _secondsToMakeMove;
            _elapsedTimeSinceUIUpdate = 0;
            OnTurnTimeUpdated?.Invoke(_remainingTime);
        }
        
        public void Stop()
        {
            _isTimerRunning = false;
        }
    }
}
