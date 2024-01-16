using System;
using UnityEngine;
using GlassyCode.TTT.Game.TicTacToe.Logic.Players;

namespace GlassyCode.TTT.Game.TicTacToe.Logic
{
    public interface ITicTacToeManager
    {
        Vector2Int? GetHint { get; }
        event Action<IPlayer, Vector2Int> OnMoveMade; //<player, moveCellPosition>
        event Action<Vector2Int> OnMoveUndid; //<lastMoveCellPosition>
        event Action<IPlayer, Vector2Int[]> OnGameFinished; //<player, winningCoords>
        event Action OnGameReset;
        void MakeMove(Vector2Int cellPos);
        void UndoMove();
        void ResetGame();
    }
}
