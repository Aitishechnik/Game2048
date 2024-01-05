using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameScreen;

    [SerializeField]
    private GameObject _endGameScreen;

    [SerializeField]
    private GameObject _mainMenuScreen;

    [SerializeField]
    private Button _start;

    [SerializeField]
    private Button _resetRecord;

    [SerializeField]
    private GameView _gameView;

    [SerializeField]
    private TextMeshProUGUI _startButtonText;

    [SerializeField]
    private TextMeshProUGUI _bestScoreTableText;

    private Coroutine _flickerButtonRoutin;

    private void Awake()
    {
        StartMainMenu();
    }

    private IEnumerator FlickerButtonText()
    {
        _startButtonText.color = new Color(_startButtonText.color.r, _startButtonText.color.g, _startButtonText.color.b, 1);
        bool isFading = true;
        while (true)
        {
            if(_startButtonText.color.a >= 1f)
            {
                isFading = true;
            }

            if(_startButtonText.color.a < 0.3f)
            {
                isFading = false;
            }
            _startButtonText.color = new Color(_startButtonText.color.r, _startButtonText.color.g, _startButtonText.color.b, isFading ? _startButtonText.color.a - 0.025f : _startButtonText.color.a + 0.025f);

                yield return null;
        }        
    }

    public void StartMainMenu()
    {
        _mainMenuScreen.SetActive(true);
        _gameScreen.gameObject.SetActive(false);
        _endGameScreen.gameObject.SetActive(false);
        _start.onClick.AddListener(_gameView.StartGame);
        if (_flickerButtonRoutin == null)
            _flickerButtonRoutin = StartCoroutine(FlickerButtonText());
        BestScoreAnnouce();
        _resetRecord.onClick.AddListener(SaveSystem.Instance.NullifyBestScore);
        _resetRecord.onClick.AddListener(BestScoreAnnouce);
    }

    public void TurnOffMainMenuScreen()
    {
        _start.onClick.RemoveAllListeners();
        _resetRecord.onClick.RemoveAllListeners();
        _mainMenuScreen.SetActive(false);
    }

    private void BestScoreAnnouce()
    {
        _bestScoreTableText.text = "Best Score: " + SaveSystem.Instance.GameData.BestScore.ToString();
    }

}
