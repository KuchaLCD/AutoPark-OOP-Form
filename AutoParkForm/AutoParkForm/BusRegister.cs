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
    public partial class BusRegister : Form
    {
        string FileName;
        public BusRegister()
        {
            InitializeComponent();
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            //новый спосбо задания времени регистрации
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd.MM.yyyy hh:mm";
            //и времени пребывания
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd.MM.yyyy hh:mm";
            //несколько готовых вариантов серийных моделей автомобилей
            comboBox1.Items.Add("Scania");
            comboBox1.Items.Add("Volvo");
            comboBox1.Items.Add("Mercedes");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //автозаполнение
            switch (comboBox1.SelectedItem)
            {
                case "Scania":
                    textBox1.Text = "155";     //макс. скорость 
                    textBox2.Text = "12";     //объем двигателя
                    textBox5.Text = "1700";     //масса
                    textBox6.Text = "7";     //ширина
                    break;
                case "Volvo":
                    textBox1.Text = "170";
                    textBox2.Text = "13,5";
                    textBox5.Text = "1900";
                    textBox6.Text = "8";
                    break;
                case "Mercedes":
                    textBox1.Text = "150";
                    textBox2.Text = "19";
                    textBox5.Text = "2500";
                    textBox6.Text = "8,5";
                    break;
            }
            //это лишь примерный перечень
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //По нажатию данной кнопки производится добавление изображения автобуса
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "JPG files (*.jpg)|*.jpg";
            openFile.ShowDialog();
            FileName = openFile.FileName;
            pictureBox1.ImageLocation = FileName;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //По нажатию данной кнопки производится запись(регистрация) автобуса
            double volumeOfEngine = Convert.ToDouble(textBox2.Text);
            double maxSpeed = Convert.ToDouble(textBox1.Text);
            string roadNumber = Convert.ToString(textBox3.Text);
            string name = comboBox1.Text;
            int registerNumberForPark = Convert.ToInt32(textBox4.Text);
            double mass = Convert.ToDouble(textBox5.Text);
            double whidth = Convert.ToDouble(textBox6.Text);
            double countOfWheels = Convert.ToDouble(textBox7.Text);
            DateTime timeOfRegistrForPark = Convert.ToDateTime(dateTimePicker1.Value);
            DateTime stayTime = Convert.ToDateTime(dateTimePicker2.Value);
            string picture = FileName;
            string notes = Convert.ToString(richTextBox1.Text);

            Transport trans = new Bus(volumeOfEngine, maxSpeed, roadNumber, name, registerNumberForPark, mass, whidth, timeOfRegistrForPark, stayTime, picture, notes, countOfWheels);
            ListForTransport.transports.Add(trans);
            //информация внесена, теперь можно очистить поля ввода
            comboBox1.Text = null;
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            pictureBox1.Image = null;
            richTextBox1.Clear();
        }

        private void BusRegister_Load(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //По нажатию данной кнопки производится генерация случайного номера регистрации в парке
            Random randomizer = new Random();
            double randomNumber = randomizer.Next(100000, 999999);
            textBox4.Text = randomNumber.ToString();
        }
    }
}
