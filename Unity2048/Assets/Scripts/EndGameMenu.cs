using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGameMenu : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _announceTable;

    [SerializeField]
    private GameObject _gameOverMenuScreen;

    [SerializeField]
    private Button _returnToMainMenu;

    [SerializeField]
    private Button _resetGame;

    [SerializeField]
    private MainMenu _mainMenu;

    [SerializeField]
    private GameView _gameView;

    public void StartEndMenu(string announce)
    {
        _gameOverMenuScreen.SetActive(true);
        _announceTable.text = announce;
        _resetGame.onClick.AddListener(_gameView.StartGame);
        _resetGame.onClick.AddListener(TurnOffEndGameScreen);
        _returnToMainMenu.onClick.AddListener(_mainMenu.StartMainMenu);
        _returnToMainMenu.onClick.AddListener(_gameView.TurnOffGameViewScreen);
        _returnToMainMenu.onClick.AddListener(TurnOffEndGameScreen);
    }

    public void TurnOffEndGameScreen()
    {
        _returnToMainMenu.onClick.RemoveAllListeners();
        _resetGame.onClick.RemoveAllListeners();
        _gameOverMenuScreen.SetActive(false);
    }
}
