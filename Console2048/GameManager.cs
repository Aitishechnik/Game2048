using System;
using System.Runtime.InteropServices;

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
            _input.PressUp += _gameLogicProcessor.MoveUp;
            _input.PressLeft += _gameLogicProcessor.MoveLeft;
            _input.PressDown += _gameLogicProcessor.MoveDown;
            _input.PressRight += _gameLogicProcessor.MoveRight;
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
            if (!_gameLogicProcessor.CheckIfTurnIsAvailable(true))
            {
                _gameUI.PrintField();
                Console.WriteLine();
                Console.WriteLine($"Game is over! Your highest score is {_field.GetMaxScore()}");
                Console.ReadKey(true);
                Environment.Exit(0);
            }
        }
    }
}
