using System;
using UnityEngine;
using UnityEngine.UI;

namespace GlassyCode.TTT.Game.TicTacToe.UI
{
    [Serializable]
    public struct CellUI
    {
        [SerializeField] private Button _btn;
        [SerializeField] private Image _symbolImage;

        public Button Btn => _btn;
        public Image SymbolImage => _symbolImage;
    }
}