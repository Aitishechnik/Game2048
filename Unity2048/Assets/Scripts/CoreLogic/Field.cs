using System;
using System.Collections.Generic;

namespace Game_2048
{
    public class Field
    {
        private const int INITIAL_TILE_VALUE = 0;
        private const int INITIAL_TILES_AMOUNT = 2;
        private const int FIELD_LENGTH = 4;

        private Tile[,] _gameField = new Tile[FIELD_LENGTH, FIELD_LENGTH];
        public Tile[,] GameField => _gameField;

        private List<Tile> _tiles = new List<Tile>();
        public List<Tile> Tiles => _tiles;

        public Field()
        {
            InitializeField();
        }

        public Tile GetRandomZeroTile()
        {
            var randomIndex = Utilities.random.Next(0, FIELD_LENGTH * FIELD_LENGTH);

            for (int j = 0; j < FIELD_LENGTH * FIELD_LENGTH; j++)
            {
                var index = (randomIndex + j) % (FIELD_LENGTH * FIELD_LENGTH);
                if (_tiles[index].Value != INITIAL_TILE_VALUE)
                    continue;

                return _tiles[index];
            }

            GameOverCheckingEvent?.Invoke();
            return null;
        }

        private void InitializeField()
        {
            bool isFirstInitialied = false;

            for (int i = 0; i < FIELD_LENGTH; i++)
            {
                for (int j = 0; j < FIELD_LENGTH; j++)
                {
                    _gameField[i, j] = new Tile();
                    UnlockAllTiles += _gameField[i, j].SetLock;
                    _tiles.Add(_gameField[i, j]);
                }
            }

            for(int i = 0; i < INITIAL_TILES_AMOUNT; i++)
            {
                var tile = GetRandomZeroTile();

                if (isFirstInitialied)
                {
                    tile.SetRandomValue();
                }
                else
                {
                    isFirstInitialied = true;
                    tile.SetValue(tile.FirstTileRandomValue);
                }
            }
        }
        
        public event Action<bool> UnlockAllTiles;

        public void UnlockAllTilesEventHandler()
        {
            UnlockAllTiles?.Invoke(false);
        }

        public void AddRandomTile()
        {
            GetRandomZeroTile().SetRandomValue();
        }

        public int GetMaxScore()
        {
            var score = 0;
            foreach (var tile in _tiles)
            {
                if(tile.Value > score)
                    score = tile.Value;
            }
            return score;
        }

        public event Action GameOverCheckingEvent; 
    }
}
