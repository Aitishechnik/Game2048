namespace Game_2048
{
    public class Tile
    {
        private bool _isLocked;
        public bool IsLocked => _isLocked;

        private const int FIRST_TILE_RANDOM_VALUE = 2;
        private const int SECOND_TILE_RANDOM_VALUE = 4;
        public int FirstTileRandomValue => FIRST_TILE_RANDOM_VALUE;

        private int _value;
        public int Value => _value;

        public void SetValue(int value)
        {
            _value = value;
        }

        public void SetRandomValue()
        {
            var index = Utilities.random.Next(0, 2);
            _value = index == 0 ? FIRST_TILE_RANDOM_VALUE : SECOND_TILE_RANDOM_VALUE;
        }

        public void SetLock(bool isLocked)
        {
            _isLocked = isLocked;
        }
    }
}
