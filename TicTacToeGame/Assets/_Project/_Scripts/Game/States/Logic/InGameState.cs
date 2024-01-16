using Zenject;
using GlassyCode.TTT.Core.SceneLoader;
using GlassyCode.TTT.Core.Time;
using GlassyCode.TTT.Game.States.Data;
using GlassyCode.TTT.Game.States.Logic.Interfaces;

namespace GlassyCode.TTT.Game.States.Logic
{
    public class InGameState : IGameState
    {
        private ISceneLoader _sceneLoader;
        
        [Inject]
        private void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void Enter(GameStatesManager owner, params object[] optionalParams)
        {
            owner.GameMode = (GameMode) optionalParams[0];
            _sceneLoader.LoadGameScene();
        }
        
        public void Exit(GameStatesManager owner, params object[] optionalParams)
        {
            
        }

        public void Tick()
        {
            
        }
    }
}
