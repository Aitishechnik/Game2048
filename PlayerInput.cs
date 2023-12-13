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
                    ButtonPressed?.Invoke(UP,false);
                    break;

                case LEFT:
                    ButtonPressed?.Invoke(LEFT,false);
                    break;

                case DOWN:
                    ButtonPressed?.Invoke(DOWN,false);
                    break;

                case RIGHT:
                    ButtonPressed?.Invoke(RIGHT,false);
                    break;

                case ESCAPE:
                    EscapePressed?.Invoke();
                    break;
            }
        }

        public event Action<ConsoleKey, bool> ButtonPressed;
        public event Action EscapePressed;

    }
}
