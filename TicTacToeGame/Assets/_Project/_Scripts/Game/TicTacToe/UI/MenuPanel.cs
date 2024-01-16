using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using TMPro;
using GlassyCode.TTT.Core.BundleAssets.Logic;
using GlassyCode.TTT.Core.UI;
using GlassyCode.TTT.Game.States.Data;
using GlassyCode.TTT.Game.States.Logic.Interfaces;

namespace GlassyCode.TTT.Game.TicTacToe.UI
{
    public class MenuPanel : Panel
    {
        [SerializeField] private ToggleGroup _gameMode;
        [SerializeField] private Button _startBtn;
        [SerializeField] private Button _reSkinBtn;
        [SerializeField] private TMP_InputField _bundleNameInputField;

        private IGameStatesManager _gameStatesManager;
        
        [Inject] 
        private void Construct(IBundleLoader bundleLoader, IGameStatesManager gameStatesManager)
        {
            _gameStatesManager = gameStatesManager;

            _startBtn.onClick.AddListener(StartGame);
            _reSkinBtn.onClick.AddListener(() => bundleLoader.LoadBundle(_bundleNameInputField.text));
        }
        
        private void OnDestroy()
        {
            _startBtn.onClick.RemoveAllListeners();
            _reSkinBtn.onClick.RemoveAllListeners();
        }

        private void StartGame()
        {
            var selectedToggle = _gameMode.ActiveToggles().FirstOrDefault();

            if (selectedToggle != null && Enum.TryParse(selectedToggle.name, out GameMode gameMode))
            {
                _gameStatesManager.ChangeStateToInGame(gameMode);
            }
            else
            {
                Debug.LogError("Invalid or no toggle selected. Make sure toggles have names same as in GameMode enum.");
            }
        }
    }
}