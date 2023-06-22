using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetSize();
        }
        
        //переменная отвечающая за нажатие кнопки
        private bool isMouse = false;

        //массив точек
        private ArrayPoints arrayPoints = new ArrayPoints(2);

        public Bitmap map = new Bitmap(100, 100);
        public Graphics graphics;

        //ручка
        public Pen pen = new Pen(Color.Black, 3f);

        private void SetSize()
        {
            Rectangle rectangle = Screen.PrimaryScreen.Bounds;
            map = new Bitmap(rectangle.Width, rectangle.Height);
            graphics = Graphics.FromImage(map);
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
        }

        // класс массива точек
        private class ArrayPoints
        {
            private int index = 0;
            private Point[] points;

            public ArrayPoints(int size)
            {
                if (size <= 0)
                {
                    size = 2;
                }

                points = new Point[size];
            }

            public void SetPoint(int x, int y)
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

            public int GetCountPoints() => index;

            public Point[] GetPoints() => points;
            
        }

        //обрабатываем нажатие в двух след методах
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isMouse = true;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouse = false;
            arrayPoints.ResetPoints();

        }

        //обрабатываем движение мыши с зажатой ЛКМ
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

                arrayPoints.SetPoint(e.X,e.Y);
            }
            
        }

        //Обрабатываем нажатие на кнопку c выбором цвета
        private void button3_Click(object sender, EventArgs e)
        {
            pen.Color = ((Button)sender).BackColor;
        }

        //Обрабатываем нажатие на кнопку c палитрой
        private void button10_Click(object sender, EventArgs e)
        {
            //Создаем и открываем окно с цветами
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pen.Color = colorDialog1.Color;
                ((Button)sender).BackColor = colorDialog1.Color;
            }
        }
        
        //Обрабатываем нажатие на кнопку Clear
        
        private void button2_Click(object sender, EventArgs e)
        {
            graphics.Clear(pictureBox1.BackColor);
            pictureBox1.Image = map;
        }

        
        //Обрабатываем trackBar для увеличения размера курсора
        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            pen.Width = trackBar1.Value;
        }
        
        //Обрабатываем нажатие на кнопку Save
        private void button1_Click(object sender, EventArgs e)
        {
            //Создаем и открываем окно сохранений
            var sfd = new SaveFileDialog();
            sfd.Filter = "Image(*.jpg)|*.jpg|(*.*|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Bitmap btm = map.Clone(new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height),map.PixelFormat);
                btm.Save(sfd.FileName, ImageFormat.Jpeg);

            }
        }

        //Обрабатываем нажатие на кнопку Open
        private void button11_Click(object sender, EventArgs e)
        {
            //Создаем и открываем Диалоговое окно где показываются файлами
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files(*.JPG;*.PNG)|*.JPG;*.PNG|All files(*.*)|*.*";
            

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //Помещаем на задний план pictureBox картинку
                pictureBox1.BackgroundImage = new Bitmap(ofd.FileName);
            }
        }
        
        //Обрабатываем нажатие на кнопку New
        private void button12_Click(object sender, EventArgs e)
        {
            //Создаем вспомогательное окно и показываем его
            var f = new New_Image();
            f.Owner = this;
            f.Show();
        }
    }
}