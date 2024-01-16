using System;
using UnityEngine;

namespace GlassyCode.TTT.Game.TicTacToe.Logic.Movement.Strategies
{
    public interface IMoveStrategy
    {
        event Action<Vector2Int> OnMadeMove;
        void Tick(float deltaTime);
        void PrepareMove(Vector2Int? cellPos = null);
        void MakeMove();
    }
}
