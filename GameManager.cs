using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            _input.EscapePressed += () => Environment.Exit(0); //Дописать логику игры и присвоить все методы (все стрелки), перенести весь визуал в GameUI
            _input.LeftPressed += _gameLogicProcessor.MoveLeft;
            RunGame();
        }

        public void RunGame()
        {            
            while (true)
            {
                _gameUI.PrintField();
                _input.ButtonIsPresed(Console.ReadKey());
            }
        }
    }
}
