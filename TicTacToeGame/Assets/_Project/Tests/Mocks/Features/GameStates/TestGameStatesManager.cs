using System;
using System.ComponentModel;
using GlassyCode.TTT.Game.States.Data;
using GlassyCode.TTT.Game.States.Logic.Interfaces;
using GlassyCode.TTT.Tests.Mocks.Interfaces;

namespace GlassyCode.TTT.Tests.Mocks.Features.GameStates
{
    public class TestGameStatesManager : IGameStatesManager, IInitializableTest
    {
        public GameMode GameMode
        {
            get => GameMode.PlayerVsPlayer;
            set
            {
                if (!Enum.IsDefined(typeof(GameMode), value))
                    throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(GameMode));
                GameMode = value;
            }
        }

        public void Initialize()
        {
            //
        }
        
        public void ChangeStateToMenu()
        {
            //
        }

        public void ChangeStateToEntry()
        {
            //
        }

        public void ChangeStateToInGame(GameMode gameMode)
        {
            //
        }
    }
}