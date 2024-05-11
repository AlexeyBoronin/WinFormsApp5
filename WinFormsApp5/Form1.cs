using System.Drawing;
namespace WinFormsApp5
{
    public partial class Form1 : Form
    {
        private Graphics graphics;
        private int resolution;
        private GameEngine engine;


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

            engine = new GameEngine(
                rows: pictureBox1.Height / resolution,
                columns: pictureBox1.Width / resolution,
                (int)numDensity.Value
                );

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);
            timer1.Start();

        }
        private void DrawNextGeneration()
        {
            graphics.Clear(Color.Black);
            var field = engine.GetCurrentGeneration();

            for (int x = 0; x < field.GetLength(0); x++)
            {
                for (int y = 0; y < field.GetLength(1); y++)
                {
                    if (field[x, y])
                        graphics.FillRectangle(Brushes.Crimson, x * resolution, y * resolution, resolution - 1, resolution - 1);
                }
            }
            pictureBox1.Refresh();
            engine.NextGeneration();
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
            DrawNextGeneration();
        }

        private void bStart_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void bStop_Click(object sender, EventArgs e)
        {
            StopGame();
        }

        private void pictureBox1_MouseMove_1(object sender, MouseEventArgs e)
        {
            //if (!timer1.Enabled) return;
            //if (e.Button == MouseButtons.Left)
            //{
            //    var x = e.Location.X / resolution;
            //    var y = e.Location.Y / resolution;
            //    var validationPassed = ValidationMousePosition(x, y);
            //    if (validationPassed)
            //        field[x, y] = true;
            //}
            //if (e.Button == MouseButtons.Right)
            //{
            //    var x = e.Location.X / resolution;
            //    var y = e.Location.Y / resolution;
            //    var validationPassed = ValidationMousePosition(x, y);
            //    if (validationPassed)
            //        field[x, y] = false;
            //}
        }

        //private bool ValidationMousePosition(int x, int y)
        //{
        //    return x >= 0 && y >= 0 && x < columns && y < rows;
        //}

    }
}
