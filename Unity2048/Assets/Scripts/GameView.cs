using Game_2048;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    private GameManager _gameManager = new GameManager();

    [SerializeField]
    private TextMeshProUGUI _scoreTable;

    [SerializeField]
    private TextMeshProUGUI _gameLossResult;

    [SerializeField]
    private FieldView _fieldView;

    [SerializeField]
    private GameObject _gameScreen;

    [SerializeField]
    private GameObject _lossScreen;

    [SerializeField]
    private Button _gameRestartButton;

    [SerializeField]
    private FlyTilesController _flyTilesController;

    private Dictionary<Tile, Tile> _animationCoordinates = new Dictionary<Tile, Tile>();
    public Dictionary<Tile, Tile> AnimationCoordinates => _animationCoordinates;

    private bool _gameIsOn = true;

    private float _nextTurnTime;
    private const float TURN_TRANSITION_DELTA = 1.2f;

    private void Start()
    {
        Application.targetFrameRate = 30;
        _nextTurnTime = _flyTilesController.TileTransitionTime * TURN_TRANSITION_DELTA;

        _gameScreen.SetActive(true);
        _lossScreen.SetActive(false);
        _gameRestartButton.onClick.AddListener(ResetGame);

        _fieldView.CreateView(_gameManager.Field);
        UpdateScoreTable();
        
        _gameManager.GameOver += GameIsLost;
        _gameManager.FromToTilesCoordinates += HandleAnimationCoordinates;
    }

    private void HandleAnimationCoordinates(Tile from, Tile to)
    {
        _animationCoordinates.Add(from, to);
    }

    private void UpdateScoreTable()
    {
        _scoreTable.text = $"Score: {_gameManager.GetCurrentScore()}";
    }

    private void GameIsLost()
    {
        _lossScreen?.SetActive(true);
        LossScreenText();
        _gameIsOn = false;
    }

    private void ResetGame()
    {
        _lossScreen.SetActive(false);
        _gameManager.ClearAllEvents();
        _gameManager = new GameManager();
        _gameManager.GameOver += GameIsLost;
        _gameManager.FromToTilesCoordinates += HandleAnimationCoordinates;
        _fieldView.CreateView(_gameManager.Field);
        UpdateScoreTable();
        _gameIsOn = true;
    }

    private void LossScreenText()
    {
        _gameLossResult.text = $"You've lost :( Score: {_gameManager.GetCurrentScore()} Tile: {_gameManager.Field.GetMaxTileValue()}";
    }

    private void DoFlyingTiles()
    {
        StartCoroutine(FlightAnimation());
        _animationCoordinates.Clear();
    }

    private IEnumerator FlightAnimation()
    {
        _gameIsOn = false;
        foreach (var tile in _animationCoordinates)
        {
            var from = _fieldView.GetTileView(tile.Key);
            var to = _fieldView.GetTileView(tile.Value);

            _flyTilesController.Fly(from, to);           
        }
        yield return new WaitForSeconds(_nextTurnTime);
        _gameManager.NextTurn();
        _fieldView.SyncField();
        UpdateScoreTable();
        _gameIsOn = true;
    }

    private void Update()
    {
        if (_gameIsOn)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _gameManager.MoveLeft();
                DoFlyingTiles();
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _gameManager.MoveRight();
                DoFlyingTiles();
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _gameManager.MoveUp();
                DoFlyingTiles();
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                _gameManager.MoveDown();
                DoFlyingTiles();
            }
        }       
    }
}
