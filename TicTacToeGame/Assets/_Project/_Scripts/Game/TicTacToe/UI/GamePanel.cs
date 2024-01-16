using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using GlassyCode.TTT.Core.BundleAssets.Data;
using GlassyCode.TTT.Core.BundleAssets.Logic;
using GlassyCode.TTT.Core.UI;
using GlassyCode.TTT.Game.TicTacToe.Data;
using GlassyCode.TTT.Game.TicTacToe.Data.Enums;
using GlassyCode.TTT.Game.TicTacToe.Logic;
using GlassyCode.TTT.Game.TicTacToe.Logic.Players;

namespace GlassyCode.TTT.Game.TicTacToe.UI
{
    public class GamePanel : Panel
    {
        [SerializeField] private CellUIs[] _cells; //With odin I would serialize 2 dimensional using dictionaries for example, for that case I just wrap structs though
        [SerializeField] private Image _background;
        [SerializeField] private Image _buttonsBlocker;
        [SerializeField] private Image _line;

        private ITicTacToeManager _ticTacToeManager;
        private IPlayersController _playersController;
        private IBundleLoader _bundleLoader;
        private Sprite _bgSprite;
        private Sprite _xSymbolSprite;
        private Sprite _oSymbolSprite;
        private Sprite _lineSprite;
        
        [Inject]
        private void Construct(ITicTacToeManager ticTacToeManager, IPlayersController playersController,
            ITicTacToeConfig config, IBundleLoader bundleLoader)
        {
            _ticTacToeManager = ticTacToeManager;
            _playersController = playersController;
            _bundleLoader = bundleLoader;
            
            for (var x = 0; x < _cells.Length; x++)
            {
                for (var y = 0; y < _cells[x].Cells.Length; y++)
                {
                    var xCopy = x;
                    var yCopy = y;
                    _cells[x].Cells[y].Btn.onClick.AddListener(() => _ticTacToeManager.MakeMove(new Vector2Int(xCopy, yCopy)));
                }
            }
            
            _ticTacToeManager.OnMoveMade += UpdateCellSprite;
            _ticTacToeManager.OnMoveUndid += ResetCell;
            _ticTacToeManager.OnGameReset += ResetBoard;
            _ticTacToeManager.OnGameFinished += ShowWinningLine;
            _playersController.OnPlayersSwapped += UpdateButtonsBlocker; 
            _bundleLoader.OnBundleLoad += LoadSprites;
        }
        
        private void UpdateButtonsBlocker(IPlayer player)
        {
            _buttonsBlocker.enabled = player.IsComputer;
        }

        private void OnDestroy()
        {
            foreach (var cellUIs in _cells)
            {
                foreach (var cell in cellUIs.Cells)
                {
                    cell.Btn.onClick.RemoveAllListeners();
                }
            }

            _ticTacToeManager.OnMoveMade -= UpdateCellSprite;
            _ticTacToeManager.OnMoveUndid -= ResetCell;
            _ticTacToeManager.OnGameReset -= ResetBoard;
            _ticTacToeManager.OnGameFinished -= ShowWinningLine;
            _playersController.OnPlayersSwapped -= UpdateButtonsBlocker; 
            _bundleLoader.OnBundleLoad += LoadSprites;
        }
        
        private void Awake()
        {
            StartCoroutine(LoadBundleAndSprites());
        }
        
        private IEnumerator LoadBundleAndSprites()
        {
            if (!_bundleLoader.IsBundleLoaded)
            {
                yield return _bundleLoader.LoadDefaultBundleAsync();
            }
            else
            {
                LoadSprites(null);
            }
        }
        
        private void Start()
        {
            _buttonsBlocker.enabled = _playersController.IsComputerTurn;
        }

        private void LoadSprites(string bundleName)
        {
            var bgSprite = _bundleLoader.GetSprite(AssetBundleNames.BgSprite);
            var lineSprite = _bundleLoader.GetSprite(AssetBundleNames.LineSprite);
            var xSymbolSprite = _bundleLoader.GetSprite(AssetBundleNames.XSymbolSprite);
            var oSymbolSprite = _bundleLoader.GetSprite(AssetBundleNames.OSymbolSprite);

            if (bgSprite && _background)
            {
                _background.sprite = bgSprite;
            }
            
            if (lineSprite && _line)
            {
                _line.sprite = lineSprite;
            }
            
            if (xSymbolSprite)
            {
                _xSymbolSprite = xSymbolSprite;
            }
            
            if (oSymbolSprite)
            {
                _oSymbolSprite = oSymbolSprite;
            }
        }
        
        private void ResetCell(Vector2Int cellPos)
        {
            _buttonsBlocker.enabled = _playersController.IsComputerTurn;
            _cells[cellPos.x].Cells[cellPos.y].SymbolImage.enabled = false;
        }

        private void ResetBoard()
        {
            foreach (var cellUIs in _cells)
            {
                foreach (var cellUI in cellUIs.Cells)
                {
                    cellUI.SymbolImage.enabled = false;
                }
            }

            _buttonsBlocker.enabled = _playersController.IsComputerTurn;
            _line.enabled = false;
        }

        private void UpdateCellSprite(IPlayer playerThatMadeMove, Vector2Int cellPos)
        {
            var symbolImage = _cells[cellPos.x].Cells[cellPos.y].SymbolImage;
            symbolImage.sprite = playerThatMadeMove.Symbol == Symbol.X ? _xSymbolSprite : _oSymbolSprite;
            symbolImage.enabled = true;
        }

        private void ShowWinningLine(IPlayer player, Vector2Int[] indexes)
        {
            if (indexes == null || indexes.Length < 3) 
                return;

            var linePositions = WinningCoords.GetWinningLinePositions(indexes);

            if (linePositions == null)
                return;
            
            _line.rectTransform.anchoredPosition = new Vector3(linePositions[0], linePositions[1], 0f);
            _line.rectTransform.eulerAngles = new Vector3(0f, 0f, linePositions[2]);
            _line.enabled = true;
        }
    }
}