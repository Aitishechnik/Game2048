using System;

namespace Game_2048
{
    public class GameManager
    {
        private Field _field = new Field();
        public Field Field => _field;
        private GameLogicProcessor _gameLogicProcessor;

        public GameManager()
        {
            _gameLogicProcessor = new GameLogicProcessor(_field);
            _field.GameOverCheckingEvent += CheckGameOverStatus;
            NextTurn();
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
                Console.WriteLine();
                Console.WriteLine($"Game is over! Your highest score is {_field.GetMaxScore()}");
                Console.ReadKey(true);
                Environment.Exit(0);
            }
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
    }
}
