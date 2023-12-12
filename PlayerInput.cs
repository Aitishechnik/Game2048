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
                    PressedUp?.Invoke(false);
                break;

                case LEFT:
                PressedLeft?.Invoke(false);
                break;

                case DOWN:
                    PressedDown?.Invoke(false);
                break;

                case RIGHT:
                    PressedRight?.Invoke(false);
                break;

                case ESCAPE:
                    EscapePressed?.Invoke();
                break;
            }
        }

        public event Action<bool> PressedLeft;
        public event Action<bool> PressedRight;
        public event Action<bool> PressedUp;
        public event Action<bool> PressedDown;
        public event Action EscapePressed;

    }
}
