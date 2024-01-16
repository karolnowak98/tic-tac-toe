using GlassyCode.TTT.Game.States.Data;

namespace GlassyCode.TTT.Game.States.Logic.Interfaces
{
    public interface IGameStatesManager
    {
        GameMode GameMode { get; set; }
        void ChangeStateToMenu();
        void ChangeStateToEntry();
        void ChangeStateToInGame(GameMode gameMode);
    }
}
