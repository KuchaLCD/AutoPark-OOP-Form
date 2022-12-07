using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace AutoParkForm
{
    public partial class MainRegister : Form
    {
        DataBase db = new DataBase();
        public MainRegister()
        {
            //Здесь представлены все основные настройки отображения
            InitializeComponent();
            park = new Park("LuxuryPark", ListForTransport.transports);
            //новый спосбо задания времени регистрации
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd.MM.yyyy hh:mm";
            //и времени пребывания
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd.MM.yyyy hh:mm";
        }
        //============
        Park park;
        string FileName;        //имя файла для открытия картинки
        //============
        private void button1_Click(object sender, EventArgs e)  //вывод информации
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //По нажатию данной кнопки производится запись(регистрация) транспорта в список активов парка и добавление транспорта в селективный список
            string name = Convert.ToString(textBox1.Text);
            int registerNumberForPark = Convert.ToInt32(textBox2.Text);
            double mass = Convert.ToDouble(textBox3.Text);
            double whidth = Convert.ToDouble(textBox4.Text);

            DateTime timeOfRegistrForPark = Convert.ToDateTime(dateTimePicker1.Value);
            DateTime stayTime = Convert.ToDateTime(dateTimePicker2.Value);

            string picture = FileName;
            string notes = Convert.ToString(richTextBox1.Text);

            Transport tr = new Transport(name, registerNumberForPark, mass, whidth, timeOfRegistrForPark, stayTime, picture, notes);
            ListForTransport.transports.Add(tr);
            listBox1.Items.Add(tr);
            //информация внесена, теперь можно очистить поля ввода
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            pictureBox1.Image = null;
            richTextBox1.Clear();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //По нажатию данной кнопки производится вызорв формы регистрации машины
            //Создаём локальную переменную(ссылку) для хранения нового экземпляра формы регистрации машины
            CarRegister carReg = new CarRegister();
            carReg.ShowDialog();    //вызов модальной формы регистрации машины
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //По нажатию данной кнопки производится очистка вывода
            richTextBox2.Text = "";     //очищаем поле вывода
            pictureBox2.Image = null;   //очищаем поле с картинкой
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //По нажатию данной кнопки производится добавление изображения для машины(создаётся ссылка на объект формата .jpg в форме строки(прямой путь к файлу))
            //добавляем изображение машины
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "JPG files (*.jpg)|*.jpg";
            openFile.ShowDialog();
            FileName = openFile.FileName;
            pictureBox1.ImageLocation = FileName;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //По нажатию данной кнопки производится вызорв формы регистрации автобуса
            BusRegister gg = new BusRegister();
            gg.ShowDialog();
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

        public void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            //если поле с текстом будет статично, то эта функция будет добавлять возможность листать содержимое объекта хранения текста
            richTextBox1.ScrollBars = (RichTextBoxScrollBars)ScrollBars.Both;
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

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            //По нажатию данной кнопки производится операция выхода из текущего окна с возможностью выбора
            DialogResult res = MessageBox.Show("Вы действительно хотите выйти?", "Сообщение", MessageBoxButtons.YesNo,
            MessageBoxIcon.Information,
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.DefaultDesktopOnly);
            if (res == DialogResult.Yes)
            {
                Hide();
                Connecting Connect = new Connecting();
                Connect.ShowDialog();
                this.Close();
            }
            this.TopMost = true;
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
            //По нажатию данной кнопки производится вызорв формы регистрации мотоцикла
            MotoRegister gg = new MotoRegister();
            gg.ShowDialog();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Поздравляю! Ты выбрал(ла) продвинутую версию)))\nТут ты можешь творить всё что только можно\n(даже создать обыкновенный начальный класс транспорт)", "Сообщение");
        }
    }
}
