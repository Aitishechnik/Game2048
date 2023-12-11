using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_2048
{
    public class GameLogicProcessor
    {
        private Field _field;
        private Tile[,] _grid;

        public GameLogicProcessor(Field field)
        {
            _field = field;
            _grid = _field.GameField;
        }

        public void MoveLeft() //Дописать логику. сделать метод универсальным для всех направлений
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
                            index--;
                            if (index == 0 && _grid[i, index].Value == 0)
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
        }
    }
}
