using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoParkForm
{
    public partial class UserRegister : Form
    {
        public UserRegister()
        {
            InitializeComponent();
            park = new Park("LuxuryPark", ListForTransport.transports);
        }
        string FileName;
        Park park;
        private void button6_Click(object sender, EventArgs e)
        {
            //По нажатию данной кнопки производится вызорв формы регистрации автобуса
            BusRegister gg = new BusRegister();
            gg.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //По нажатию данной кнопки производится вызорв формы регистрации машины
            //Создаём локальную переменную(ссылку) для хранения нового экземпляра формы регистрации машины
            CarRegister carReg = new CarRegister();
            carReg.ShowDialog();    //вызов модальной формы регистрации машины
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //По нажатию данной кнопки производится вызорв формы регистрации мотоцикла
            MotoRegister gg = new MotoRegister();
            gg.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //По нажатию данной кнопки производится вывод информации об активах парка
            if (ListForTransport.transports.Count == 0)
            {
                richTextBox2.Text = "Парк пуст";
            }
            for (int i = 0; i < ListForTransport.transports.Count; i++)
            {
                richTextBox2.Text = park.About();
                pictureBox2.ImageLocation = ListForTransport.transports[i].Picture;   //функция - показать изображение
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //По нажатию данной кнопки производится очистка вывода
            richTextBox2.Text = "";     //очищаем поле вывода
            pictureBox2.Image = null;   //очищаем поле с картинкой
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //По нажатию данной кнопки производится селективный вывод информации
            Transport boofer = (Transport)listBox1.SelectedItem;
            if (boofer == null)
            {
                DialogResult res = MessageBox.Show("В селективном списке отсутсвуют объекты, либо не выбран нужный элемент", "Сообщение");
            }
            else
            {
                richTextBox2.Text = boofer.InfoString();
                pictureBox2.ImageLocation = boofer.Picture;   //показать изображение
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //По нажатию данной кнопки производится обновление группы элементов селективного списка
            listBox1.Items.Clear();
            for (int i = 0; i < ListForTransport.transports.Count; i++)
            {
                listBox1.Items.Add(ListForTransport.transports[i]);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Вы действительно хотите выйти?","Сообщение",MessageBoxButtons.YesNo,
            MessageBoxIcon.Information,
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.DefaultDesktopOnly);
            if(res == DialogResult.Yes)
            {
                Hide();
                Connecting Connect = new Connecting();
                Connect.ShowDialog();
                this.Close();
            }
            this.TopMost = true;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            //По нажатию данной кнопки производится операция расчёта оплаты стоянки в автопарке для выбранного элемента
            Transport boofer = (Transport)listBox1.SelectedItem;
            if (boofer == null)
            {
                DialogResult res = MessageBox.Show("В селективном списке отсутсвуют объекты для расчёта оплаты, либо не выбран нужный элемент", "Сообщение");
            }
            else
            {
                richTextBox2.Text = boofer.Calculate();
            }
        }

        private void UserRegister_Load(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            //По нажатию данной кнопки производится операция удаления транспорта из селективного списка
            Transport boofer = (Transport)listBox1.SelectedItem;
            if (boofer == null)
            {
                DialogResult res = MessageBox.Show("В селективном списке отсутсвуют объекты для удаления, либо не выбран нужный элемент", "Сообщение");
            }
            else
            {
                listBox1.Items.Remove(boofer);
                ListForTransport.transports.Remove(boofer);
                richTextBox2.Text = "";
                pictureBox2.Image = null;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Вау! Ты решил(ла) попробовать обычный пользовательский интерфейс)\nЭта версия разрботана специально под нужды платных атопарков", "Сообщение");
        }
    }
}
