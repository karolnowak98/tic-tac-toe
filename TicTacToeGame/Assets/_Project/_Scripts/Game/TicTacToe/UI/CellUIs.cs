using System;
using UnityEngine;

namespace GlassyCode.TTT.Game.TicTacToe.UI
{
    [Serializable]
    public struct CellUIs
    {
        [SerializeField] private CellUI[] _cells;

        public CellUI[] Cells => _cells;
    }
}
