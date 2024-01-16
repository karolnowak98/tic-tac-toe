using Zenject;
using GlassyCode.TTT.Game.States.Data;
using GlassyCode.TTT.Game.States.Logic.Interfaces;

namespace GlassyCode.TTT.Game.States.Logic
{
    public class GameStatesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(GameStatesManager), typeof(IGameStatesManager)
                    , typeof(ITickable), typeof(IInitializable))
                .To<GameStatesManager>()
                .FromSubContainerResolve()
                .ByMethod(InstallGameStateMachine)
                .AsSingle()
                .NonLazy();
        }

        private static void InstallGameStateMachine(DiContainer subContainer)
        {
            subContainer.Bind(typeof(GameStatesManager), typeof(IGameStatesManager)
                    , typeof(ITickable), typeof(IInitializable))
                .To<GameStatesManager>()
                .AsSingle()
                .NonLazy();

            subContainer.Bind<IGameState>().WithId(GameStateInjectIds.EntryStateId).To<EntryState>().AsSingle();
            subContainer.Bind<IGameState>().WithId(GameStateInjectIds.MenuStateId).To<MenuState>().AsSingle();
            subContainer.Bind<IGameState>().WithId(GameStateInjectIds.InGameStateId).To<InGameState>().AsSingle();
        }
    }
}