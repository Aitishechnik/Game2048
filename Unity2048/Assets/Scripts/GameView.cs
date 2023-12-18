using Game_2048;
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

    private bool _gameIsOn = true;

    private void Start()
    {
        _gameScreen.SetActive(true);
        _lossScreen.SetActive(false);
        _gameRestartButton.onClick.AddListener(ResetGame);

        _fieldView.CreateView(_gameManager.Field);
        UpdateScoreTable();
        
        _gameManager.GameOver += GameIsLost;
    }

    private void UpdateScoreTable()
    {
        _scoreTable.text = $"Score: {_gameManager.GetCurrentScore()}";
    }

    private void GameIsLost() // Заменить удаление и отписку на "чистку"
    {
        _lossScreen?.SetActive(true);
        LossScreenText();
        _gameIsOn = false;
    }

    private void ResetGame() // Исправить ошибки, доделать архитектуру pool->factory->gameView
    {
        _lossScreen.SetActive(false);
        _gameManager.ClearAllEvents();
        _gameManager = new GameManager();
        _gameManager.GameOver += GameIsLost;
        _fieldView.CreateView(_gameManager.Field);
        _gameIsOn = true;
    }

    private void LossScreenText()
    {
        _gameLossResult.text = $"You've lost :( Score: {_gameManager.GetCurrentScore()} Tile: {_gameManager.Field.GetMaxTileValue()}";
    }

    private void Update()
    {
        if (_gameIsOn)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _gameManager.MoveLeft();
                _gameManager.NextTurn();
                _fieldView.SyncField();
                UpdateScoreTable();
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _gameManager.MoveRight();
                _gameManager.NextTurn();
                _fieldView.SyncField();
                UpdateScoreTable();
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _gameManager.MoveUp();
                _gameManager.NextTurn();
                _fieldView.SyncField();
                UpdateScoreTable();
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                _gameManager.MoveDown();
                _gameManager.NextTurn();
                _fieldView.SyncField();
                UpdateScoreTable();
            }
        }       
    }
}
