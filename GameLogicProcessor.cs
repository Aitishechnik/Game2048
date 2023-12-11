using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_2048
{
    public class GameLogicProcessor
    {
        private Tile[,] _field;

        public GameLogicProcessor(Tile[,] field)
        {
            _field = field;
        }

        public void MoveLeft()
        {
            for (int i = 0; i < _field.GetLength(0); i++)
            {
                bool isUnited = false;
                for (int j = 1; j < _field.GetLength(1); j++)
                {
                    int index = j;
                    while (index > 0)
                    {
                        if (_field[i, j].Value != 0)
                        {
                            index--;
                            if (_field[i, index].Value == 0 ||
                                _field[i, index].Value == _field[i, j].Value)
                            {
                                if (!isUnited && _field[i, index].Value == _field[i, j].Value)
                                {
                                    _field[i, index].SetValue(_field[i, index].Value + _field[i, j].Value);
                                    _field[i, j].SetValue(0);
                                    isUnited = true;
                                    break;
                                }
                            }
                            if (index == 0)
                            {
                                var currentValue = _field[i, j].Value;
                                _field[i, j].SetValue(0);
                                _field[i, index + 1].SetValue(currentValue);
                                break;
                            }
                        }
                        else
                            break;
                    }
                }
            }
        }
    }
}
