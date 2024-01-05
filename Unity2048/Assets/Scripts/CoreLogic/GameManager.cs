using System;

namespace Game_2048
{
    public class GameManager
    {
        private Field _field;
        public Field Field => _field;
        private GameLogicProcessor _gameLogicProcessor;

        public GameManager(int[,] fieldValues = null)
        {
            _field = fieldValues == null ? new Field() : new Field(fieldValues);
            _gameLogicProcessor = new GameLogicProcessor(_field);
            _field.GameOverCheckingEvent += CheckGameOverStatus;
            _gameLogicProcessor.FromToTilesCoordinates += SendAnimationTilesCoordinates;
        }

        public int[,] GetFieldValuesMatrix()
        {
            int[,] valuesMatrix = new int[_field.GameField.GetLength(0), _field.GameField.GetLength(1)];
            
            for(int i = 0; i < valuesMatrix.GetLength(0); i++)
            {
                for(int j = 0; j < valuesMatrix.GetLength(1); j++)
                {
                    valuesMatrix[i,j] = _field.GameField[i,j].Value;
                }
            }

            return valuesMatrix;
        }

        public void SendAnimationTilesCoordinates(Tile from, Tile to)
        {
            FromToTilesCoordinates?.Invoke(from, to);
        }

        public int GetCurrentScore()
        {
            return _field.CurrecntScore;
        }

        public void NextTurn()
        {
            if (_gameLogicProcessor.IsMoveExecuted)
                _field.AddRandomTile();
            _gameLogicProcessor.SetIsMoveExecutedToFalse();
            _field.GetRandomZeroTile();
        }

        private void CheckGameOverStatus()
        {
            if (!_gameLogicProcessor.CheckIfTurnIsAvailable(true))
            {
                GameOver?.Invoke();
            }
        }

        public void ClearAllEvents()
        {
            _field.ClearFieldEvents();
            _gameLogicProcessor.ClearProcessorEvents();
            FromToTilesCoordinates = null;
            GameOver = null;
        }

        public void MoveLeft()
        {
            _gameLogicProcessor.MoveLeft();
        }

        public void MoveRight()
        {
            _gameLogicProcessor.MoveRight();
        }

        public void MoveUp()
        {
            _gameLogicProcessor.MoveUp();
        }

        public void MoveDown()
        {
            _gameLogicProcessor.MoveDown();
        }

        public event Action GameOver;
        public event Action<Tile, Tile> FromToTilesCoordinates;
    }
}
