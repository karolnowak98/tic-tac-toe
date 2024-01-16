using System;

namespace GlassyCode.TTT.Game.TicTacToe.Logic.Timers
{
    public interface ITurnTimer
    {
        event Action<float> OnTurnTimeUpdated;
        event Action OnTimePassed;
        void Tick(float deltaTime);
        void Start();
        void Stop();
    }
}
