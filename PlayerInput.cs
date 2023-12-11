using System;
using System.Collections;

namespace Game_2048
{
    public class PlayerInput
    {
        private const ConsoleKey UP = ConsoleKey.UpArrow;
        private const ConsoleKey LEFT = ConsoleKey.LeftArrow;
        private const ConsoleKey DOWN = ConsoleKey.DownArrow;
        private const ConsoleKey RIGHT = ConsoleKey.RightArrow;
        private const ConsoleKey ESCAPE = ConsoleKey.Escape;

        public void ButtonIsPresed(ConsoleKeyInfo input)
        {
            switch (input.Key)
            {
                case UP:
                UpPressed?.Invoke();
                break;

                case LEFT:
                LeftPressed?.Invoke();
                break;

                case DOWN:
                DownPressed?.Invoke();
                break;

                case RIGHT:
                RightPressed?.Invoke();
                break;

                case ESCAPE:
                EscapePressed?.Invoke();
                break;
            }
        }

        public event Action UpPressed;
        public event Action LeftPressed;
        public event Action DownPressed;
        public event Action RightPressed;
        public event Action EscapePressed;

    }
}
