using Game_2048;
using UnityEngine;

public class GameView : MonoBehaviour
{
    private GameManager _gameManager = new GameManager();

    private void Start()
    {
        foreach(var tile in _gameManager.Field.GameField)
        {
            TileFactory.Instance.Create(tile.Value);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _gameManager.MoveLeft();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _gameManager.MoveRight();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _gameManager.MoveUp();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _gameManager.MoveDown();
        }

        _gameManager.NextTurn();
    }
}
