using System;

namespace Game_2048
{
    public class GameLogicProcessor
    {
        private Field _field;
        private Tile[,] _grid;

        private bool _isMoveExecuted = false;
        public bool IsMoveExecuted => _isMoveExecuted;

        private bool _turnIsAvailable = true;
        public bool TurnIsAvailable => _turnIsAvailable;
        public GameLogicProcessor(Field field)
        {
            _field = field;
            _grid = _field.GameField;
        }

        public void SetIsMoveExecutedToFalse()
        {
            _isMoveExecuted = false;
        }

        #region Universal Method
        private int SetDirectionIndex(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow: return -1;
                case ConsoleKey.LeftArrow: return -1;
                case ConsoleKey.DownArrow: return 1;
                case ConsoleKey.RightArrow: return 1;
            }
            throw new Exception("Incorrect input");
        }

        private int SetDirectionLimit(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow: return 0;
                case ConsoleKey.LeftArrow: return 0;
                case ConsoleKey.DownArrow: return _grid.GetLength(1) - 1;
                case ConsoleKey.RightArrow: return _grid.GetLength(1) - 1;
            }
            throw new Exception("Incorrect input");
        }

        private bool IndexCondition(ConsoleKey key, int index)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow: return index > 0;
                case ConsoleKey.LeftArrow: return index > 0;
                case ConsoleKey.DownArrow: return index < _grid.GetLength(1) - 1;
                case ConsoleKey.RightArrow: return index < _grid.GetLength(1) - 1;
            }
            throw new Exception("Incorrect input");
        }

        private bool InnerCycleCondition(ConsoleKey key, int j)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow: return j < _grid.GetLength(1);
                case ConsoleKey.LeftArrow: return j < _grid.GetLength(1);
                case ConsoleKey.DownArrow: return j >= 0;
                case ConsoleKey.RightArrow: return j >= 0;
            }
            throw new Exception("Incorrect input");
        }

        private int SetIndexStartPosition(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow: return 1;
                case ConsoleKey.LeftArrow: return 1;
                case ConsoleKey.DownArrow: return _grid.GetLength(1) - 1;
                case ConsoleKey.RightArrow: return _grid.GetLength(1) - 1;
            }
            throw new Exception("Incorrect input");
        }

        private Tile NormalizeArguments(ConsoleKey key ,int i, int j, int index, bool isUsingIndex)
        {
            if (isUsingIndex)
            {
                switch (key)
                {
                    case ConsoleKey.UpArrow: return _grid[index, i];
                    case ConsoleKey.LeftArrow: return _grid[i, index];
                    case ConsoleKey.DownArrow: return _grid[index, i];
                    case ConsoleKey.RightArrow: return _grid[i, index];
                }
            }
            else
            {
                switch (key)
                {
                    case ConsoleKey.UpArrow: return _grid[j, i];
                    case ConsoleKey.LeftArrow: return _grid[i, j];
                    case ConsoleKey.DownArrow: return _grid[j, i];
                    case ConsoleKey.RightArrow: return _grid[i, j];
                }
            }           
            throw new Exception("Incorrect input");
        }

        public void MoveTiles(ConsoleKey key, bool isCheckingTurn)
        {
            int directionIndex = SetDirectionIndex(key);
            int directionLimit = SetDirectionLimit(key);
            int indexStartPosition = SetIndexStartPosition(key);

            for (int i = 0; i < _grid.GetLength(0); i++)
            {
                for (int j = indexStartPosition; InnerCycleCondition(key, j); j += (directionIndex * -1))
                {
                    int index = j;
                    while (IndexCondition(key, index))
                    {
                        if (NormalizeArguments(key, i, j, index, false).Value != 0)
                        {
                            index += directionIndex;
                            if (index == directionLimit && NormalizeArguments(key, i, j, index, true).Value == 0)
                            {
                                if (!isCheckingTurn)
                                {
                                    NormalizeArguments(key, i, j, index, true).SetValue(NormalizeArguments(key, i, j, index, false).Value);
                                    NormalizeArguments(key, i, j, index, false).SetValue(0);
                                    _isMoveExecuted = true;
                                    break;
                                }
                                else
                                {
                                    _turnIsAvailable = true;
                                    return;
                                }
                            }

                            if (NormalizeArguments(key, i, j, index, true).Value > 0 && NormalizeArguments(key, i, j, index, true).Value != NormalizeArguments(key, i, j, index, false).Value)
                            {
                                if(NormalizeArguments(key, i, j, index + (directionIndex * -1), true) != NormalizeArguments(key, i, j, index, false))
                                {
                                    if (!isCheckingTurn)
                                    {
                                        var tempIndex = NormalizeArguments(key, i, j, index, false).Value;
                                        NormalizeArguments(key, i, j, index, false).SetValue(0);
                                        NormalizeArguments(key, i, j, index + (directionIndex * -1), true).SetValue(tempIndex);
                                        _isMoveExecuted = true;
                                    }
                                    else
                                    {
                                        _turnIsAvailable = true;
                                        return;
                                    }
                                }                              
                                break;
                            }

                            if (!NormalizeArguments(key, i, j, index, true).IsLocked && NormalizeArguments(key, i, j, index, false).Value == NormalizeArguments(key, i, j, index, true).Value)
                            {
                                if (!isCheckingTurn)
                                {
                                    NormalizeArguments(key, i, j, index, false).SetValue(0);
                                    NormalizeArguments(key, i, j, index, true).SetValue(NormalizeArguments(key, i, j, index, true).Value * 2);
                                    NormalizeArguments(key, i, j, index, true).SetLock(true);
                                    _isMoveExecuted = true;
                                    break;
                                }
                                else
                                {
                                    _turnIsAvailable = true;
                                    return;
                                }
                            }
                        }
                        else
                            break;
                    }
                }
                _field.UnlockAllTilesEventHandler();
            }
            if (isCheckingTurn)
                _turnIsAvailable = false;
        }
        #endregion

        
        public bool CheckIfTurnIsAvailable(bool isCheckingStatus = true)
        {
            MoveTiles(ConsoleKey.UpArrow,isCheckingStatus);
            if (_turnIsAvailable)
                return true;
            MoveTiles(ConsoleKey.LeftArrow,isCheckingStatus);
            if (_turnIsAvailable)
                return true;
            MoveTiles(ConsoleKey.DownArrow,isCheckingStatus);
            if (_turnIsAvailable)
                return true;
            MoveTiles(ConsoleKey.RightArrow,isCheckingStatus);
            if (_turnIsAvailable)
                return true;

            return false;
        }
    }
}
