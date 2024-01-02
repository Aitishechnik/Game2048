using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameScreen;

    [SerializeField]
    private GameObject _lossScreen;

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

    private Coroutine _flickerButtonTextRoutine;

    private void Awake()
    {
        StartMainMenu();
    }

    private IEnumerator FlickerButtonText()
    {
        bool isFading = true;
        while (true)
        {
            if(_startButtonText.color.a >= 1)
            {
                isFading = true;
            }

            if(_startButtonText.color.a <= 0.3)
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
        _lossScreen.gameObject.SetActive(false);
        _start.onClick.AddListener(_gameView.StartGame);
        StartCoroutine(FlickerButtonText());
        //_resetRecord.onClick.AddListener();
    }

    public void TurnOffMainMenuScreen()
    {
        _start.onClick.RemoveAllListeners();
        _resetRecord.onClick.RemoveAllListeners();
        StopAllCoroutines();
        _mainMenuScreen.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
