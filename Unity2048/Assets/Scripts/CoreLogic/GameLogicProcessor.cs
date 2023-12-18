using System;

namespace Game_2048
{
    public class GameLogicProcessor
    {
        private enum Direction { Up, Left, Down, Right }

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
        private int SetDirectionIndex(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up: return -1;
                case Direction.Left: return -1;
                case Direction.Down: return 1;
                case Direction.Right: return 1;
            }
            throw new Exception("Incorrect input");
        }

        private int SetDirectionLimit(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up: return 0;
                case Direction.Left: return 0;
                case Direction.Down: return _grid.GetLength(1) - 1;
                case Direction.Right: return _grid.GetLength(1) - 1;
            }
            throw new Exception("Incorrect input");
        }

        private bool IndexCondition(Direction direction, int index)
        {
            switch (direction)
            {
                case Direction.Up: return index > 0;
                case Direction.Left: return index > 0;
                case Direction.Down: return index < _grid.GetLength(1) - 1;
                case Direction.Right: return index < _grid.GetLength(1) - 1;
            }
            throw new Exception("Incorrect input");
        }

        private bool InnerCycleCondition(Direction direction, int j)
        {
            switch (direction)
            {
                case Direction.Up: return j < _grid.GetLength(1);
                case Direction.Left: return j < _grid.GetLength(1);
                case Direction.Down: return j >= 0;
                case Direction.Right: return j >= 0;
            }
            throw new Exception("Incorrect input");
        }

        private int SetIndexStartPosition(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up: return 1;
                case Direction.Left: return 1;
                case Direction.Down: return _grid.GetLength(1) - 1;
                case Direction.Right: return _grid.GetLength(1) - 1;
            }
            throw new Exception("Incorrect input");
        }

        private Tile NormalizeArguments(Direction direction, int i, int j, int index, bool isUsingIndex)
        {
            if (isUsingIndex)
            {
                switch (direction)
                {
                    case Direction.Up: return _grid[index, i];
                    case Direction.Left: return _grid[i, index];
                    case Direction.Down: return _grid[index, i];
                    case Direction.Right: return _grid[i, index];
                }
            }
            else
            {
                switch (direction)
                {
                    case Direction.Up: return _grid[j, i];
                    case Direction.Left: return _grid[i, j];
                    case Direction.Down: return _grid[j, i];
                    case Direction.Right: return _grid[i, j];
                }
            }           
            throw new Exception("Incorrect input");
        }

        public void MoveUp()
        {
            MoveTiles(Direction.Up, false);
        }

        public void MoveLeft()
        {
            MoveTiles(Direction.Left, false);
        }

        public void MoveDown()
        {
            MoveTiles(Direction.Down, false);
        }

        public void MoveRight()
        {
            MoveTiles(Direction.Right, false);
        }

        private void MoveTiles(Direction direction, bool isCheckingTurn)
        {
            int directionIndex = SetDirectionIndex(direction);
            int directionLimit = SetDirectionLimit(direction);
            int indexStartPosition = SetIndexStartPosition(direction);

            for (int i = 0; i < _grid.GetLength(0); i++)
            {
                for (int j = indexStartPosition; InnerCycleCondition(direction, j); j += (directionIndex * -1))
                {
                    int index = j;
                    while (IndexCondition(direction, index))
                    {
                        if (NormalizeArguments(direction, i, j, index, false).Value != 0)
                        {
                            index += directionIndex;
                            if (index == directionLimit && NormalizeArguments(direction, i, j, index, true).Value == 0)
                            {
                                if (!isCheckingTurn)
                                {
                                    NormalizeArguments(direction, i, j, index, true).SetValue(NormalizeArguments(direction, i, j, index, false).Value, false);
                                    NormalizeArguments(direction, i, j, index, false).SetValue(0);
                                    _isMoveExecuted = true;
                                    break;
                                }
                                else
                                {
                                    _turnIsAvailable = true;
                                    return;
                                }
                            }

                            if (NormalizeArguments(direction, i, j, index, true).Value > 0 && NormalizeArguments(direction, i, j, index, true).Value != NormalizeArguments(direction, i, j, index, false).Value)
                            {
                                if(NormalizeArguments(direction, i, j, index + (directionIndex * -1), true) != NormalizeArguments(direction, i, j, index, false))
                                {
                                    if (!isCheckingTurn)
                                    {
                                        var tempIndex = NormalizeArguments(direction, i, j, index, false).Value;
                                        NormalizeArguments(direction, i, j, index, false).SetValue(0);
                                        NormalizeArguments(direction, i, j, index + (directionIndex * -1), true).SetValue(tempIndex, false);
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

                            if (!NormalizeArguments(direction, i, j, index, true).IsLocked && NormalizeArguments(direction, i, j, index, false).Value == NormalizeArguments(direction, i, j, index, true).Value)
                            {
                                if (!isCheckingTurn)
                                {
                                    NormalizeArguments(direction, i, j, index, false).SetValue(0);
                                    NormalizeArguments(direction, i, j, index, true).SetValue(NormalizeArguments(direction, i, j, index, true).Value * 2);
                                    NormalizeArguments(direction, i, j, index, true).SetLock(true);
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
            MoveTiles(Direction.Up,isCheckingStatus);
            if (_turnIsAvailable)
                return true;
            MoveTiles(Direction.Left,isCheckingStatus);
            if (_turnIsAvailable)
                return true;
            MoveTiles(Direction.Down,isCheckingStatus);
            if (_turnIsAvailable)
                return true;
            MoveTiles(Direction.Right,isCheckingStatus);
            if (_turnIsAvailable)
                return true;

            return false;
        }
    }
}
