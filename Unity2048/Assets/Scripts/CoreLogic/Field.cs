using System;
using System.Collections.Generic;

namespace Game_2048
{
    public class Field
    {
        private int _currentScore;
        public int CurrecntScore => _currentScore;

        private const int INITIAL_TILES_AMOUNT = 2;
        private const int FIELD_LENGTH = 4;

        private List<Tile> _zeroValueTiles = new List<Tile>();

        private Tile[,] _gameField = new Tile[FIELD_LENGTH, FIELD_LENGTH];
        public Tile[,] GameField => _gameField;

        private List<Tile> _tiles = new List<Tile>();
        public List<Tile> Tiles => _tiles;

        public Field(int[,] fieldValues = null)
        {
            InitializeField(fieldValues);
        }     

        public Tile GetRandomZeroTile()
        {
            _zeroValueTiles.Clear();
            foreach (var tile in _tiles)
            {
                if (tile.Value == 0)
                    _zeroValueTiles.Add(tile);
            }

            if(_zeroValueTiles.Count > 0)
            {
                var randomTile = _zeroValueTiles[Utilities.random.Next(0, _zeroValueTiles.Count)];
                return randomTile;
            }

            GameOverCheckingEvent?.Invoke();
            return null;
        }

        private void InitializeField(int[,] fieldValues = null)
        {
            bool isFirstInitialized = false;

            for (int i = 0; i < FIELD_LENGTH; i++)
            {
                for (int j = 0; j < FIELD_LENGTH; j++)
                {
                    _gameField[i, j] = new Tile();
                    _gameField[i, j].GetUpdatedTileValue += UpdateScore;
                    UnlockAllTiles += _gameField[i, j].SetLock;

                    _tiles.Add(_gameField[i, j]);
                }
            }
            if(fieldValues == null)
            {
                for (int i = 0; i < INITIAL_TILES_AMOUNT; i++)
                {
                    var tile = GetRandomZeroTile();

                    if (isFirstInitialized)
                    {
                        tile.SetRandomValue();
                    }
                    else
                    {
                        isFirstInitialized = true;
                        tile.SetValue(tile.FirstTileRandomValue, false);
                    }
                }
            }
            else
            {
                for(int i = 0; i < _gameField.GetLength(0); i++)
                {
                    for (int j = 0; j <  GameField.GetLength(1); j++)
                    {
                        _gameField[i, j].SetValue(fieldValues[i, j], false);
                    }
                }
                _currentScore = SaveSystem.Instance.GameData.CurrentScore;
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

        private void UpdateScore(int value)
        {
            _currentScore += value;
        }

        public int GetMaxTileValue()
        {
            var score = 0;
            foreach (var tile in _tiles)
            {
                if(tile.Value > score)
                    score = tile.Value;
            }
            return score;
        }

        public void ClearFieldEvents()
        {
            foreach(var tile in _tiles)
            {
                tile.ClearEvent();
            }
            UnlockAllTiles = null;
        }

        public event Action GameOverCheckingEvent; 
    }
}
