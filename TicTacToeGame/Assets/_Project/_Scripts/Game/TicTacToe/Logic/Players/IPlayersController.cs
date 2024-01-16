using System;
using GlassyCode.TTT.Game.TicTacToe.Data.Enums;

namespace GlassyCode.TTT.Game.TicTacToe.Logic.Players
{
    public interface IPlayersController
    {
        IPlayer CurrentPlayer { get; }
        IPlayer NextPlayer { get; }
        Symbol CurrentPlayerSymbol { get; }
        bool IsComputerTurn { get; }
        event Action<IPlayer> OnPlayersSwapped; //<newPlayer>
        void InitPlayers();
        void SwapPlayers();
        IPlayer PlayerBySymbol(Symbol symbol);
    }
}