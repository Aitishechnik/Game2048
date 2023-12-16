using Game_2048;
using UnityEngine;

public class GameView : MonoBehaviour
{
    private GameManager _gameManager = new GameManager();

    [SerializeField]
    private FieldView _fieldView;

    private void Start()
    {
        _fieldView.CreateView(_gameManager.Field);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _gameManager.MoveLeft();
            _gameManager.NextTurn();
            _fieldView.SyncField();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _gameManager.MoveRight();
            _gameManager.NextTurn();
            _fieldView.SyncField();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _gameManager.MoveUp();
            _gameManager.NextTurn();
            _fieldView.SyncField();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _gameManager.MoveDown();
            _gameManager.NextTurn();
            _fieldView.SyncField();
        }
    }
}
