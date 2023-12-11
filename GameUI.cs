using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_2048
{
    public class GameUI
    {
        private Tile[,] _field;

        public GameUI(Tile[,] field) 
        { 
            _field = field;
        }

        public void PrintField()
        {
            Console.Clear();
            for (int i = 0; i < _field.GetLength(0); i++)
            {
                for (int j = 0; j < _field.GetLength(1); j++)
                {
                    if (_field[i, j].Value == 0)
                    {
                        Console.Write($"_ ");
                    }
                    else
                    {
                        Console.Write($"{_field[i, j].Value} ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
