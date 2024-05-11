using System.Drawing;
namespace WinFormsApp5
{
    public partial class Form1 : Form
    {
        private Graphics graphics;
        private int resolution;
        private bool[,] field;
        private int rows;
        private int columns;

        public Form1()
        {
            InitializeComponent();
        }
        private void StartGame()
        {
            if (timer1.Enabled)
                return;
            numResolution.Enabled = false;
            numDensity.Enabled = false;
            resolution = (int)numResolution.Value;
            rows = pictureBox1.Height / resolution;
            columns = pictureBox1.Width / resolution;
            field = new bool[columns, rows];
            Random rnd = new Random();
            for (int x = 0; x < columns; x++)
                for (int y = 0; y < rows; y++)
                {
                    field[x, y] = rnd.Next((int)numDensity.Value) == 0;
                }
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);
            timer1.Start();

        }
        private void NextGeneration()
        {
            graphics.Clear(Color.Black);
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

                    if (haslife)
                    {
                        graphics.FillRectangle(Brushes.Crimson, x * resolution, y * resolution, resolution-1, resolution-1);
                    }
                }

            field = newfield;
            pictureBox1.Refresh();

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
        private void StopGame()
        {
            if (!timer1.Enabled)
                return;
            timer1.Stop();
            numResolution.Enabled = true;
            numDensity.Enabled = true;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            NextGeneration();
        }

        private void bStart_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void bStop_Click(object sender, EventArgs e)
        {
            StopGame();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!timer1.Enabled) return;
            if (e.Button == MouseButtons.Left)
            {
                var x = e.Location.X / resolution;
                var y = e.Location.Y / resolution;
                var validationPassed = ValidationMousePosition(x, y);
                if (validationPassed)
                    field[x, y] = true;
            }
            if (e.Button == MouseButtons.Right)
            {
                var x = e.Location.X / resolution;
                var y = e.Location.Y / resolution;
                var validationPassed = ValidationMousePosition(x, y);
                if (validationPassed)
                    field[x, y] = false;
            }
        }

        private bool ValidationMousePosition(int x, int y)
        {
            return x >= 0 && y >= 0 && x < columns && y < rows;
        }

        private void pictureBox1_MouseMove_1(object sender, MouseEventArgs e)
        {
            if(!timer1.Enabled) return;

            if(e.Button == MouseButtons.Left)
            {
                
                var x= e.Location.X / resolution;
                var y= e.Location.Y / resolution;
                var validationPassed= ValidationMousePosition(x, y);
                if (validationPassed)
                    field[x,y]= true;
            }
            if(e.Button == MouseButtons.Right)
            {
                var x = e.Location.X / resolution;
                var y = e.Location.Y / resolution;
                var validationPassed = ValidationMousePosition(x, y);
                if (validationPassed)
                    field[x, y] = false;
            }
        }
        private bool ValidateMousePosition(int x, int y)
        {
            return x>=0 && y>=0 && x < columns &&y < rows;
        }
    }
}
