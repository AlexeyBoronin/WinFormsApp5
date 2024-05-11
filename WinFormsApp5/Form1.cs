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
        private void StarGame()
        {
            if (timer1.Enabled)
                return;

            numResolution.Enabled=false;
            numDensity.Enabled=false;

            resolution = (int)numResolution.Value;
            rows=pictureBox1.Height/resolution;
            columns=pictureBox1.Width/resolution;
            field = new bool[rows, columns];
            
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);
            timer1.Start();
            //graphics.FillRectangle(Brushes.Crimson, 0, 0, resolution, resolution);
            Random rnd = new Random();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    field[i, j] = rnd.Next((int)numDensity.Value) == 0;
                }
            }
        }
        private void StopGame()
        {
            if(!timer1.Enabled)
                return;
            timer1.Stop();
            numResolution.Enabled = true;
            numDensity.Enabled = true;

        }
        private void NextGeneration()
        {
            graphics.Clear(Color.Black);
            var newfield=new bool[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    var count=Count(i, j);
                    var haslife=field[i, j];
                    if(!haslife&&count==3)
                    {
                        newfield[i, j] = true;
                    }else if(haslife&&(count>3||count<2))
                    {
                        newfield[i,j] = false;
                    }
                    else
                    {
                        newfield[i,j]=field[i,j];
                    }
                    if(field[i, j])
                    {
                        graphics.FillRectangle(Brushes.Crimson,i*resolution,j*resolution,resolution,resolution);
                    }
                }
                
                
            }
            field=newfield;
            pictureBox1.Refresh();
        }
        private int Count(int x, int y)
        {
            int count = 0;

            for(int i=-1; i<2;i++)
                for(int j=-1; j<2;)
                {
                    int col = (x + i+columns)%columns;
                    int row = (y + j+rows)%rows;
                    var isSelfChecking = col == x && col == y;
                    var haslife = field[col, row];
                    if(haslife &&!isSelfChecking)
                        count++;
                }
            return count;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            NextGeneration();
        }

        private void к—тарт_Click(object sender, EventArgs e)
        {
            StarGame();
        }

        private void bStop_Click(object sender, EventArgs e)
        {
            StopGame();
        }
    }
}
