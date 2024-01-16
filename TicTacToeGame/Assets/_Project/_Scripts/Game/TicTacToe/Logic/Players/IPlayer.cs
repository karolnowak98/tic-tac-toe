using System;
using UnityEngine;
using GlassyCode.TTT.Game.TicTacToe.Data.Enums;

namespace GlassyCode.TTT.Game.TicTacToe.Logic.Players
{
    public interface IPlayer
    {
        PlayerType Type { get; }
        string Name { get; }
        Symbol Symbol { get; }
        bool IsComputer { get; }
        event Action<Vector2Int> OnMadeMove;
        void Tick(float deltaTime);
        void PrepareMove(Vector2Int? fieldPosition = null);
    }
}
