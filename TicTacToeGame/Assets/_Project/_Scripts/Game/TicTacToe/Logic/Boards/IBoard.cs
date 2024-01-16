using UnityEngine;
using GlassyCode.TTT.Game.TicTacToe.Data.Enums;

namespace GlassyCode.TTT.Game.TicTacToe.Logic.Boards
{
    public interface IBoard
    {
        void Reset();
        void SetSymbolAtPosition(Vector2Int fieldPos, Symbol symbol);
        void MarkAsEmptyAtPosition(Vector2Int fieldPos);
        Vector2Int? GetRandomEmptyField();
        Vector2Int[] GetWinningCoords(Symbol symbol);
        Symbol CheckWinForPlayers();
        Symbol GetSymbolAtPosition(Vector2Int fieldPos);
        bool IsFieldOccupiedAtPosition(Vector2Int fieldPos);
        bool IsFull();
    }
}