using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Figur1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetSize();
        }
        private class ArrayPoints
        {
            private int index = 0;
            private Point[] points;
            public ArrayPoints(int size)
            {
                if (size<=0) 
                { 
                    size = 2;
                }
                points = new Point[size];
            }
            public void SetPoint(int x,int y)
            {
                if (index >= points.Length)
                {
                    index = 0;
                }
                points[index] = new Point(x, y);
                index++;
            }

            public void ResetPoints()
            {
                index = 0;
            }
            public int GetCountPoints()
            {
                return index;
            }
            public Point[] GetPoints()
            {
                return points;
            }
        }
        private ArrayPoints arrayPoints = new ArrayPoints(2);
        Bitmap map = new Bitmap(100,100);
        Graphics graphics;
        Pen pen = new Pen(Color.Black, 3f);
        private void SetSize()
        {
            Rectangle rectangle = Screen.PrimaryScreen.Bounds;
            map = new Bitmap(rectangle.Width, rectangle.Height);
            graphics = Graphics.FromImage(map);
        }
        private bool crc_OK = false;
        private void button11_add_crc_Click(object sender, EventArgs e)
        {
            crc_OK = true;
        }
        private bool rec_OK = false;
        private void button12_add_rec_Click(object sender, EventArgs e)
        {
            rec_OK = true;
        }
        Point firstp = new Point();
        private bool isMouse = false;
        //private bool line_OK = false;
        /*private void button11_Click(object sender, EventArgs e)
        {
            line_OK = true;
        }*/
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isMouse = true;
            if ((crc_OK == true) && (rec_OK == false))
            {
                isMouse = false;
                firstp = e.Location;
                Graphics gr = pictureBox1.CreateGraphics();
                gr.DrawEllipse(pen, firstp.X, firstp.Y, e.X - firstp.X, e.Y - firstp.Y);                
            }
            else if ((rec_OK == true) && (crc_OK == false) )
            {
                isMouse = false;
                firstp = e.Location;
                Graphics gr = pictureBox1.CreateGraphics();
                gr.DrawRectangle(pen, firstp.X, firstp.Y, e.X - firstp.X, e.Y - firstp.Y);                
            }
            /*else if ((rec_OK == false) && (crc_OK == false) && (line_OK = true))
            {
                isMouse = false;
                firstp = e.Location;
                Graphics gr = pictureBox1.CreateGraphics();
                gr.DrawLine(pen, e.X - firstp.X, e.Y - firstp.Y, e.X, e.Y);
            }*/
            else 
            {
                return;
            }
    }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouse = false;
            arrayPoints.ResetPoints();
            if ((crc_OK == true) && (rec_OK == false))
            {                
                Graphics gr = pictureBox1.CreateGraphics();
                gr.DrawEllipse(pen, firstp.X, firstp.Y, e.X - firstp.X, e.Y - firstp.Y);               
                crc_OK = false;
                
            }            
            else if ((rec_OK == true) && (crc_OK == false))
            {
                Graphics gr = pictureBox1.CreateGraphics();
                gr.DrawRectangle(pen, firstp.X, firstp.Y, e.X - firstp.X, e.Y - firstp.Y);               
                rec_OK = false;               
            }
            /*else if ((rec_OK == false) && (crc_OK == false) && (line_OK = true))
            {
                isMouse = false;

                Graphics gr = pictureBox1.CreateGraphics();
                gr.DrawLine(pen, e.X - firstp.X, e.Y - firstp.Y, e.X, e.Y);
                line_OK = false;
            }*/
            else 
            { 
                return;                
            }
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isMouse)
            {
                return;
            }
            arrayPoints.SetPoint(e.X,e.Y);
            if (arrayPoints.GetCountPoints() >= 2)
            {
                graphics.DrawLines(pen,arrayPoints.GetPoints());
                pictureBox1.Image = map;
                arrayPoints.SetPoint(e.X, e.Y);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            pen.Color = ((Button)sender).BackColor;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            graphics.Clear(pictureBox1.BackColor);
            pictureBox1.Image = map;
        }
        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            pen.Width = trackBar1.Value;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "(*.JPG)|*.jpg";
            if (saveFileDialog1.ShowDialog()== DialogResult.OK)
            {
                if(pictureBox1.Image == null)
                {
                    pictureBox1.Image.Save(saveFileDialog1.FileName);
                }
            }
        }

        
    }
}
