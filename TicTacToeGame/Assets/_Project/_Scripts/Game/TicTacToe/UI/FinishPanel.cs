using UnityEngine;
using UnityEngine.UI;
using Zenject;
using TMPro;
using GlassyCode.TTT.Core.UI;
using GlassyCode.TTT.Game.States.Logic.Interfaces;
using GlassyCode.TTT.Game.TicTacToe.Logic;
using GlassyCode.TTT.Game.TicTacToe.Logic.Players;

namespace GlassyCode.TTT.Game.TicTacToe.UI
{
    public class FinishPanel : Panel
    {
        [SerializeField] private TextMeshProUGUI _wonTmp;
        [SerializeField] private Button _resetBtn;
        [SerializeField] private Button _returnToMenuBtn;

        private ITicTacToeManager _ticTacToeManager;
        private IGameStatesManager _gameStatesManager;
        
        [Inject]
        private void Construct(ITicTacToeManager ticTacToeManager, IGameStatesManager gameStatesManager)
        {
            _ticTacToeManager = ticTacToeManager;
            _gameStatesManager = gameStatesManager;
            
            _resetBtn.onClick.AddListener(ResetGame);
            _returnToMenuBtn.onClick.AddListener(ReturnToMenu);
            _ticTacToeManager.OnGameFinished += FinishGame;

        }

        private void OnDestroy()
        {
            _resetBtn.onClick.RemoveAllListeners();
            _returnToMenuBtn.onClick.RemoveAllListeners();
            _ticTacToeManager.OnGameFinished -= FinishGame;
        }

        private void FinishGame(IPlayer player, Vector2Int[] indexes)
        {
            _wonTmp.text = player == null ? "Draw!" : $"{player.Name} ({player.Symbol}) wins!";
            Show();
        }
        
        private void ResetGame()
        {
            _ticTacToeManager.ResetGame();
            Hide();
        }

        private void ReturnToMenu()
        {
            _gameStatesManager.ChangeStateToMenu();
        }
    }
}