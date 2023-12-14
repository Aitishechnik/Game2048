using System;

namespace Game_2048
{
    public class GameManager
    {
        private Field _field = new Field();
        private GameLogicProcessor _gameLogicProcessor;
        private PlayerInput _input = new PlayerInput();
        GameUI _gameUI;

        public GameManager()
        {
            _gameLogicProcessor = new GameLogicProcessor(_field);
            _gameUI = new GameUI(_field.GameField);
            _input.EscapePressed += () => Environment.Exit(0);
            _input.ButtonPressed += _gameLogicProcessor.MoveTiles;
            GameOverCheckingEvent += _gameLogicProcessor.CheckIfTurnIsAvailable;
            _field.GameOverCheckingEvent += CheckGameOverStatus;
            RunGame();
        }

        public void RunGame()
        {            
            while (true)
            {
                _gameUI.PrintField();
                _input.ButtonIsPresed(Console.ReadKey());
                if (_gameLogicProcessor.IsMoveExecuted)
                    _field.AddRandomTile();
                _gameLogicProcessor.SetIsMoveExecutedToFalse();
                _field.GetRandomZeroTile();
            }
        }

        private void CheckGameOverStatus()
        {
            if (GameOverCheckingEvent!= null && !GameOverCheckingEvent.Invoke(true))
            {
                _gameUI.PrintField();
                Console.WriteLine();
                Console.WriteLine($"Game is over! Your highest score is {_field.GetMaxScore()}");
                Console.ReadKey(true);
                Environment.Exit(0);
            }
        }

        private event Predicate<bool> GameOverCheckingEvent;
    }
}
