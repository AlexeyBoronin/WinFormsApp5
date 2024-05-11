using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp5
{
    public class GameEngine
    {
        private bool[,] field;
        private readonly int rows;
        private readonly int columns;
        private Random random=new Random();

        public GameEngine(int rows, int columns, int density)
        {
            this.rows = rows;
            this.columns = columns;
            field = new bool[columns, rows];
            for (int x = 0; x < columns; x++)
                for (int y = 0; y < rows; y++)
                {
                    field[x, y] = random.Next(density) == 0;
                }
        }
        public void NextGeneration()
        {
            
            var newfield = new bool[columns, rows];
            for (int x = 0; x < columns; x++)
                for (int y = 0; y < rows; y++)
                {
                    var neightCount = CountNeighbours(x, y);
                    var haslife = field[x, y];
                    if (!haslife && neightCount == 3)
                    {
                        newfield[x, y] = true;
                    }
                    else if (haslife && (neightCount < 2 || neightCount > 3))
                    {
                        newfield[x, y] = false;
                    }
                    else
                    {
                        newfield[x, y] = field[x, y];
                    }

                    
                }

            field = newfield;
            

        }
        public bool[,] GetCurrentGeneration()
        {
            var result= new bool[columns, rows];
            for (int x = 0; x < columns; x++)
                for (int y = 0; y < rows; y++)
                {
                    result[x, y] = field[x,y];
                }

                    return result;
        }
        private int CountNeighbours(int x, int y)
        {
            int count = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    int col = (x + i + columns) % columns;
                    int row = (y + j + rows) % rows;

                    bool isSelfChecking = col == x && row == y;
                    var haslife = field[col, row];
                    if (haslife && !isSelfChecking)
                        count++;
                }
            }
            return count;
        }
        private bool ValidationCellPosition(int x, int y)
        {
            return x >= 0 && y >= 0 && x < columns && y < rows;
        }
        private void UpdateCell(int x, int y, bool state)
        {
           
            if (ValidationCellPosition(x,y))
                field[x, y] = state;
        }
        public void AddCell(int x, int y)
        {
            UpdateCell(x, y, state:true);
        }
        public void RemoteCell(int x, int y)
        {
            UpdateCell(x, y, state: false);
        }
    }
}
