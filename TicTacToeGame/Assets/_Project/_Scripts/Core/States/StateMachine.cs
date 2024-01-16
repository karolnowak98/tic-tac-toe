using System;
using Zenject;

namespace GlassyCode.TTT.Core.States
{
    public abstract class StateMachine<T> : ITickable
    {
        private IState<T> _currentState;

        public event Action OnStateChanged;

        protected void ChangeState(IState<T> newState, T owner, params object[] optionalArgs)
        {
            _currentState?.Exit(owner, optionalArgs);

            _currentState = newState;
            OnStateChanged?.Invoke();

            _currentState?.Enter(owner, optionalArgs);
        }

        public void Tick()
        {
            _currentState.Tick();
        }
    }
}