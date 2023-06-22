using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class New_Image : Form
    {
        public New_Image()
        {
            InitializeComponent();
        }

        //Обработка нажатие на кнопку Окей
        private void button1_Click(object sender, EventArgs e)
        {
            //Меняем размер главного окна
            Form1 main = this.Owner as Form1;
            main.Size = new Size((int)numericUpDown1.Value, (int)numericUpDown2.Value);
            main.pictureBox1.Size = new Size((int)numericUpDown1.Value-150, (int)numericUpDown2.Value);
            
            //Повторяем действия из Clear
            main.graphics.Clear(main.pictureBox1.BackColor);
            main.pictureBox1.Image = main.map;
            Close();


            
        }

        //Обработка нажатие на кнопку выйти
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}