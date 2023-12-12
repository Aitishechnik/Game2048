using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

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
        /*private int SetOuterLength(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow: return _grid.GetLength(1);
                case ConsoleKey.LeftArrow: return _grid.GetLength(0);
                case ConsoleKey.DownArrow: return _grid.GetLength(1);
                case ConsoleKey.RightArrow: return _grid.GetLength(0);
            }

            throw new Exception("Incorrect input");
        }

        private int SetInnerLength(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow: return _grid.GetLength(0);
                case ConsoleKey.LeftArrow: return _grid.GetLength(1);
                case ConsoleKey.DownArrow: return _grid.GetLength(0);
                case ConsoleKey.RightArrow: return _grid.GetLength(1);
            }
            throw new Exception("Incorrect input");
        }

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
                case ConsoleKey.DownArrow: return _grid.GetLength(0) - 1;
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
                case ConsoleKey.DownArrow: return index < _grid.GetLength(0) - 1;
                case ConsoleKey.RightArrow: return index < _grid.GetLength(1) - 1;
            }
            throw new Exception("Incorrect input");
        }

        private bool InnerCycleCondition(ConsoleKey key, int j)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow: return j < _grid.GetLength(0);
                case ConsoleKey.LeftArrow: return j < _grid.GetLength(1);
                case ConsoleKey.DownArrow: return j > 0;
                case ConsoleKey.RightArrow: return j > 0;
            }
            throw new Exception("Incorrect input");
        }

        private int SetIndexStartPosition(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow: return 1;
                case ConsoleKey.LeftArrow: return 1;
                case ConsoleKey.DownArrow: return _grid.GetLength(0) - 1;
                case ConsoleKey.RightArrow: return _grid.GetLength(1) - 1;
            }
            throw new Exception("Incorrect input");
        }

        public void MoveTiles(ConsoleKey key) //Исправить баги
        {
            int outerLength = SetOuterLength(key);
            int innerLength = SetInnerLength(key);
            int directionIndex = SetDirectionIndex(key);
            int directionLimit = SetDirectionLimit(key);
            int indexStartPosition = SetIndexStartPosition(key);

            for (int i = 0; i < outerLength; i++)
            {
                for (int j = indexStartPosition; InnerCycleCondition(key, j); j+= (directionIndex * -1))
                {
                    int index = j;
                    while (IndexCondition(key, index))
                    {
                        if (_grid[i, j].Value != 0)
                        {
                            index += directionIndex;
                            if (index == directionLimit && _grid[i, index].Value == 0)
                            {
                                _grid[i, index].SetValue(_grid[i, j].Value);
                                _grid[i, j].SetValue(0);
                                break;
                            }

                            if(_grid[i, index].Value > 0 && _grid[i, index].Value != _grid[i, j].Value)
                            {
                                var tempIndex = _grid[i, j].Value;
                                _grid[i, j].SetValue(0);
                                _grid[i, index + 1].SetValue(tempIndex);
                                break;
                            }

                            if (!_grid[i, index].IsLocked && _grid[i,j].Value == _grid[i, index].Value)
                            {
                                _grid[i, j].SetValue(0);
                                _grid[i, index].SetValue(_grid[i, index].Value * 2);
                                _grid[i, index].SetLock(true);
                                break;
                            }
                        }
                        else
                            break;
                    }
                }
                _field.UnlockAllTilesEventHandler();
            }
        }*/
        #endregion

        public void MoveTilesLeft(bool isCheckingTurn)
        {
            for (int i = 0; i < _grid.GetLength(0); i++)
            {
                for (int j = 1; j < _grid.GetLength(1); j++)
                {
                    int index = j;
                    while (index > 0)
                    {
                        if (_grid[i, j].Value != 0)
                        {
                            index --;
                            if (index == 0 && _grid[i, index].Value == 0)
                            {
                                if (!isCheckingTurn)
                                {
                                    _grid[i, index].SetValue(_grid[i, j].Value);
                                    _grid[i, j].SetValue(0);
                                    _isMoveExecuted = true;
                                    break;
                                }
                                else
                                {
                                    _turnIsAvailable = true;
                                    return;
                                }
                                    
                                
                            }

                            if (_grid[i, index].Value > 0 && _grid[i, index].Value != _grid[i, j].Value)
                            {
                                if(_grid[i, j] != _grid[i, index + 1])
                                {
                                    if (!isCheckingTurn)
                                    {
                                        var tempIndex = _grid[i, j].Value;
                                        _grid[i, j].SetValue(0);
                                        _grid[i, index + 1].SetValue(tempIndex);
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

                            if (!_grid[i, index].IsLocked && _grid[i, j].Value == _grid[i, index].Value)
                            {
                                if (!isCheckingTurn)
                                {
                                    _grid[i, j].SetValue(0);
                                    _grid[i, index].SetValue(_grid[i, index].Value * 2);
                                    _grid[i, index].SetLock(true);
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

        public void MoveTilesRight(bool isCheckingTurn)
        {
            for (int i = 0; i < _grid.GetLength(0); i++)
            {
                for (int j = _grid.GetLength(1) - 1; j >= 0; j--)
                {
                    int index = j;
                    while (index < _grid.GetLength(1) - 1)
                    {
                        if (_grid[i, j].Value != 0)
                        {
                            index++;
                            if (index == _grid.GetLength(1) - 1 && _grid[i, index].Value == 0)
                            {
                                if (!isCheckingTurn)
                                {
                                    _grid[i, index].SetValue(_grid[i, j].Value);
                                    _grid[i, j].SetValue(0);
                                    _isMoveExecuted = true;
                                    break;
                                }
                                else
                                {
                                    _turnIsAvailable = true;
                                    return;
                                }
                            }

                            if (_grid[i, index].Value > 0 && _grid[i, index].Value != _grid[i, j].Value)
                            {
                                if(_grid[i, j] != _grid[i, index - 1])
                                {
                                    if (!isCheckingTurn)
                                    {
                                        var tempIndex = _grid[i, j].Value;
                                        _grid[i, j].SetValue(0);
                                        _grid[i, index - 1].SetValue(tempIndex);
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

                            if (!_grid[i, index].IsLocked && _grid[i, j].Value == _grid[i, index].Value)
                            {
                                if (!isCheckingTurn)
                                {
                                    _grid[i, j].SetValue(0);
                                    _grid[i, index].SetValue(_grid[i, index].Value * 2);
                                    _grid[i, index].SetLock(true);
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

        public void MoveTilesUp(bool isCheckingTurn)
        {
            for (int i = 0; i < _grid.GetLength(0); i++)
            {
                for (int j = 1; j < _grid.GetLength(1); j++)
                {
                    int index = j;
                    while (index > 0)
                    {
                        if (_grid[j, i].Value != 0)
                        {
                            index--;
                            if (index == 0 && _grid[index, i].Value == 0)
                            {
                                if (!isCheckingTurn)
                                {
                                    _grid[index, i].SetValue(_grid[j, i].Value);
                                    _grid[j, i].SetValue(0);
                                    _isMoveExecuted = true;
                                    break;
                                }
                                else
                                {
                                    _turnIsAvailable = true;
                                    return;
                                }
                            }

                            if (_grid[index, i].Value > 0 && _grid[index, i].Value != _grid[j, i].Value)
                            {
                                if(_grid[j, i] != _grid[index + 1, i])
                                {
                                    if (!isCheckingTurn)
                                    {
                                        var tempIndex = _grid[j, i].Value;
                                        _grid[j, i].SetValue(0);
                                        _grid[index + 1, i].SetValue(tempIndex);
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

                            if (!_grid[index, i].IsLocked && _grid[j, i].Value == _grid[index, i].Value)
                            {
                                if (!isCheckingTurn)
                                {
                                    _grid[j, i].SetValue(0);
                                    _grid[index, i].SetValue(_grid[index, i].Value * 2);
                                    _grid[index, i].SetLock(true);
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
            if(isCheckingTurn)
                _turnIsAvailable = false;
        }

        public void MoveTilesDown(bool isCheckingTurn)
        {
            for (int i = 0; i < _grid.GetLength(0); i++)
            {
                for (int j = _grid.GetLength(1) - 1; j >= 0; j--)
                {
                    int index = j;
                    while (index < _grid.GetLength(0) - 1)
                    {
                        if (_grid[j, i].Value != 0)
                        {
                            index++;
                            if (index == _grid.GetLength(0) - 1 && _grid[index, i].Value == 0)
                            {
                                if (!isCheckingTurn)
                                {
                                    _grid[index, i].SetValue(_grid[j, i].Value);
                                    _grid[j, i].SetValue(0);
                                    _isMoveExecuted = true;
                                    break;
                                }                                
                                else
                                {
                                    _turnIsAvailable = true;
                                    return;
                                }
                            }

                            if (_grid[index, i].Value > 0 && _grid[index, i].Value != _grid[j, i].Value)
                            {
                                if(_grid[j, i] != _grid[index - 1, i])
                                {
                                    if (!isCheckingTurn)
                                    {
                                        var tempIndex = _grid[j, i].Value;
                                        _grid[j, i].SetValue(0);
                                        _grid[index - 1, i].SetValue(tempIndex);
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

                            if (!_grid[index, i].IsLocked && _grid[j, i].Value == _grid[index, i].Value)
                            {
                                if (!isCheckingTurn)
                                {
                                    _grid[j, i].SetValue(0);
                                    _grid[index, i].SetValue(_grid[index, i].Value * 2);
                                    _grid[index, i].SetLock(true);
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
            if(isCheckingTurn)
                _turnIsAvailable = false;
        }

        public bool CheckIfTurnIsAvailable(bool isCheckingStatus = true)
        {
            MoveTilesUp(isCheckingStatus);
            if(_turnIsAvailable)
                return true;
            MoveTilesLeft(isCheckingStatus);
            if (_turnIsAvailable)
                return true;
            MoveTilesDown(isCheckingStatus);
            if (_turnIsAvailable)
                return true;
            MoveTilesRight(isCheckingStatus);
            if (_turnIsAvailable)
                return true;

            return false;
        }
    }
}
