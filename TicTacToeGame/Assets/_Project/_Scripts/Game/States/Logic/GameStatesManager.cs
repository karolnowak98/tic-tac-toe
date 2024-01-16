using Zenject;
using GlassyCode.TTT.Core.States;
using GlassyCode.TTT.Game.States.Data;
using GlassyCode.TTT.Game.States.Logic.Interfaces;

namespace GlassyCode.TTT.Game.States.Logic
{
    public class GameStatesManager : StateMachine<GameStatesManager>, IInitializable, IGameStatesManager
    {
        [Inject(Id = GameStateInjectIds.EntryStateId)] private IGameState _entryState;
        [Inject(Id = GameStateInjectIds.MenuStateId)] private IGameState _menuState;
        [Inject(Id = GameStateInjectIds.InGameStateId)] private IGameState _inGameState;
        
        public GameMode GameMode { get; set; }
        
        public void Initialize()
        {
            ChangeStateToEntry(); //Instead of that we can create some config to specify some init config, for that case it's simpler.
        }

        public void ChangeStateToMenu()
        {
            ChangeState(_menuState, this);
        }
        
        public void ChangeStateToEntry()
        {
            ChangeState(_entryState, this);
        }
        
        public void ChangeStateToInGame(GameMode gameMode)
        {
            ChangeState(_inGameState, this, gameMode);
        }
    }
}
