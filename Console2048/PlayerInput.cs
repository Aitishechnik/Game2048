using System;

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
                    PressUp?.Invoke();
                    break;

                case LEFT:
                    PressLeft?.Invoke();
                    break;

                case DOWN:
                    PressDown?.Invoke();
                    break;

                case RIGHT:
                    PressRight?.Invoke();
                    break;

                case ESCAPE:
                    EscapePressed?.Invoke();
                    break;
            }
        }

        //public event Action<ConsoleKey, bool> ButtonPressed;
        public event Action PressUp;
        public event Action PressDown;
        public event Action PressLeft;
        public event Action PressRight;
        public event Action EscapePressed;

    }
}
