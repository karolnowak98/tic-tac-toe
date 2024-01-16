using UnityEngine;
using UnityEngine.UI;
using Zenject;
using TMPro;
using GlassyCode.TTT.Core.UI;
using GlassyCode.TTT.Game.TicTacToe.Data.Enums;
using GlassyCode.TTT.Game.TicTacToe.Logic;
using GlassyCode.TTT.Game.TicTacToe.Logic.Boards;
using GlassyCode.TTT.Game.TicTacToe.Logic.Players;
using GlassyCode.TTT.Game.TicTacToe.Logic.Timers;

namespace GlassyCode.TTT.Game.TicTacToe.UI
{
    public class HelperPanel : Panel
    {
        [SerializeField] private TextMeshProUGUI _turnTmp;
        [SerializeField] private TextMeshProUGUI _timeLeftTmp;
        [SerializeField] private TextMeshProUGUI _hintTmp;
        [SerializeField] private Button _hintBtn;
        [SerializeField] private Button _undoBtn;
        [SerializeField] private Button _resetBtn;

        private ITicTacToeManager _ticTacToeManager;
        private IPlayersController _playersController;
        private IBoard _board;
        private ITurnTimer _turnTimer;

        [Inject]
        private void Construct(IPlayersController playersController, 
            ITicTacToeManager ticTacToeManager, IBoard board, ITurnTimer turnTimer)
        {
            _playersController = playersController;
            _ticTacToeManager = ticTacToeManager;
            _board = board;
            _turnTimer = turnTimer;
            
            _hintBtn.onClick.AddListener(ShowHint);
            _undoBtn.onClick.AddListener(_ticTacToeManager.UndoMove);
            _resetBtn.onClick.AddListener(_ticTacToeManager.ResetGame);
            _playersController.OnPlayersSwapped += UpdatePlayerInfo;
            _turnTimer.OnTurnTimeUpdated += UpdateTurnTimer;
        }


        private void OnDestroy()
        {
            _hintBtn.onClick.RemoveAllListeners();
            _undoBtn.onClick.RemoveAllListeners();
            _resetBtn.onClick.RemoveAllListeners();
            _playersController.OnPlayersSwapped -= UpdatePlayerInfo;
            _turnTimer.OnTurnTimeUpdated -= UpdateTurnTimer;
        }

        private void UpdateTurnTimer(float remainingSeconds)
        {
            _timeLeftTmp.text = $"Turn time: {remainingSeconds:F1} seconds";
        }

        private void UpdatePlayerInfo(IPlayer newPlayer)
        {
            _turnTmp.text = $"Turn: {newPlayer.Name} ({(newPlayer.Symbol == Symbol.X ? "X" : "O")})";
            _hintTmp.text = "Hint: ";
        }

        private void ShowHint()
        {
            var hintPos = _ticTacToeManager.GetHint;

            if (hintPos == null)
                return;

            _hintTmp.text = $"Hint: You can place symbol at cell in the {hintPos.Value.x + 1} row and {hintPos.Value.y + 1} column.";
        }
    }
}