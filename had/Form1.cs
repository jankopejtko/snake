using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace had
{
    public partial class Form1 : Form
    {
        public int direction = 3;
        public int score = 0;
        Random rn = new Random();
        public Form1()
        {
            InitializeComponent();
            Size size = new Size(400, 400);
            this.MaximumSize = size;
            this.MinimumSize = size;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
        }
        List<PictureBox> points = new List<PictureBox>();
        List<Control> snake = new List<Control>();
        public void spawnPoint()
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.Location = new Point(rn.Next(10, this.Width), rn.Next(10, this.Height));
            pictureBox.Width = 10;
            pictureBox.Height = pictureBox.Width;
            pictureBox.BackColor = Color.Red;
            points.Add(pictureBox);
            this.Controls.Add(pictureBox);
        }
        public void move(Panel p, int a) 
        {
            switch (a) 
            {
                case 1:
                    p.Location = new Point(p.Left, p.Top - p.Width); //up
                    break;
                case 2:
                    p.Location = new Point(p.Left - p.Width, p.Top); //left
                    break;
                case 3:
                    p.Location = new Point(p.Left + p.Width, p.Top); //right
                    break;
                case 4:
                    p.Location = new Point(p.Left, p.Top + p.Width); //down
                    break;
            }
        }
        public void ColisionCheckWall() 
        {
            if (snake[0].Location.X < 0) 
            {
                snake[0].Location = new Point(this.Width, snake[0].Location.Y);
            }
            else if (snake[0].Location.X > this.Width)
            {
                snake[0].Location = new Point(0, snake[0].Location.Y);
            }
            else if (snake[0].Location.Y < 0)
            {
                snake[0].Location = new Point(snake[0].Location.X, this.Height);
            }
            else if (snake[0].Location.Y > this.Height)
            {
                snake[0].Location = new Point(snake[0].Location.X, 0);
            }
        }
        private void ColisionCheckSnake() 
        {
            for (int i = 1; i < snake.Count; i++) 
            {
                if (snake[0].Bounds.IntersectsWith(snake[i].Bounds)) 
                {
                    Application.Exit();
                }
            }
        }
        public void ColisionCheck(Panel p) 
        {
            for(int i = 0; i < points.Count; i++)
            {
                if(p.Bounds.IntersectsWith(points[i].Bounds))
                {
                    this.Controls.Remove(points[i]);
                    points.Remove(points[i]);
                    score++;
                    this.Text = "score: " + score.ToString();
                    this.Controls.Add(p);
                    addOcas();
                }
            }
        }
        public void addOcas() 
        {
            snake.Add(new Panel());
            snake[snake.Count - 1].Location = new Point(snake[0].Location.X, snake[0].Location.Y);
            snake[snake.Count - 1].Height = 10;
            snake[snake.Count - 1].Width = 10;
            snake[snake.Count - 1].BackColor = Color.Green;
            for (int j = 0; j < snake.Count; j++)
            {
                this.Controls.Add(snake[snake.Count - 1]);
            }
            try 
            {
                timer2.Interval = (int)(timer2.Interval * 0.90);
            }
            catch(Exception ex) 
            {
                return;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            snake.Add(panel1);
            this.Text = "score: ";
            timer2.Interval = 200;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            spawnPoint();
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.W || e.KeyCode == Keys.Up) 
            {
                direction = 1;
            }
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
            {
                direction = 2;
            }
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
            {
                direction = 3;
            }
            if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down)
            {
                direction = 4;
            }
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            moveBody();
            ColisionCheck(panel1);
            ColisionCheckWall();
        }
        public void moveBody() 
        {
            int x = snake[0].Location.X;
            int y = snake[0].Location.Y;
            for (int i = 1; i < snake.Count; i++)
            {
                int a = snake[i].Location.X;
                int b = snake[i].Location.Y;
                snake[i].Location = new Point(x, y);
                x = a;
                y = b;
            }
            move(panel1, direction);
            ColisionCheckSnake();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
        private void timer3_Tick(object sender, EventArgs e)
        {
            
        }
    }
    public class snakeBody
    {
        Point position;
        int direction;
        public snakeBody(Point position, int direction)
        {
            this.direction = direction;
            this.position = position;
        }
    }
}