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
    public partial class MotoRegister : Form
    {
        string FileName;
        public MotoRegister()
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
            comboBox1.Items.Add("Ducati-1098");
            comboBox1.Items.Add("Honda Blackbird CBR1100XX");
            comboBox1.Items.Add("BMW S1000 RR");
            comboBox1.Items.Add("Yamaha YZF-R1");
            comboBox1.Items.Add("Ninja ZX-14");
            comboBox1.Items.Add("MV Agusta F4 CC");
            comboBox1.Items.Add("Suzuki Hayabusa");
            comboBox1.Items.Add("МТТ Turbine Superbike");
            comboBox1.Items.Add("МТТ Street Fighter");
            comboBox1.Items.Add("Dodge Tomahawk");
        }

        private void MotoRegister_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //По нажатию данной кнопки производится генерация случайного номера регистрации в парке
            Random randomizer = new Random();
            double randomNumber = randomizer.Next(100000, 999999);
            textBox4.Text = randomNumber.ToString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //автозаполнение
            switch (comboBox1.SelectedItem)
            {
                case "Ducati-1098":
                    textBox1.Text = "200";     //макс. скорость 
                    textBox2.Text = "1,5";     //объем двигателя
                    textBox5.Text = "173";     //масса
                    textBox6.Text = "1,5";     //ширина
                    break;
                case "Honda Blackbird CBR1100XX":
                    textBox1.Text = "290";
                    textBox2.Text = "1,2";
                    textBox5.Text = "225";
                    textBox6.Text = "1,4";
                    break;
                case "BMW S1000 RR":
                    textBox1.Text = "300";
                    textBox2.Text = "1,9";
                    textBox5.Text = "270";
                    textBox6.Text = "1,9";
                    break;
                case "Yamaha YZF-R1":
                    textBox1.Text = "340";
                    textBox2.Text = "2,1";
                    textBox5.Text = "269";
                    textBox6.Text = "1,7";
                    break;
                case "Ninja ZX-14":
                    textBox1.Text = "345";
                    textBox2.Text = "2,1";
                    textBox5.Text = "240";
                    textBox6.Text = "1,5";
                    break;
                case "MV Agusta F4 CC":
                    textBox1.Text = "306";
                    textBox2.Text = "2,2";
                    textBox5.Text = "245";
                    textBox6.Text = "1,3";
                    break;
                case "Suzuki Hayabusa":
                    textBox1.Text = "330";
                    textBox2.Text = "2,3";
                    textBox5.Text = "320";
                    textBox6.Text = "1,2";
                    break;
                case "МТТ Turbine Superbike":
                    textBox1.Text = "365";
                    textBox2.Text = "2,3";
                    textBox5.Text = "225";
                    textBox6.Text = "1,9";
                    break;
                case "МТТ Street Fighter":
                    textBox1.Text = "402";
                    textBox2.Text = "2,5";
                    textBox5.Text = "380";
                    textBox6.Text = "1,7";
                    break;
                case "Dodge Tomahawk":
                    textBox1.Text = "480";
                    textBox2.Text = "2,3";
                    textBox5.Text = "680";
                    textBox6.Text = "2,3";
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
            //По нажатию данной кнопки производится добавление изображения мотоцикла
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
            //По нажатию данной кнопки производится запись(регистрация) мотоцикла
            double volumeOfEngine = Convert.ToDouble(textBox2.Text);
            double maxSpeed = Convert.ToDouble(textBox1.Text);
            string roadNumber = Convert.ToString(textBox3.Text);
            string name = comboBox1.Text;
            int registerNumberForPark = Convert.ToInt32(textBox4.Text);
            double mass = Convert.ToDouble(textBox5.Text);
            double whidth = Convert.ToDouble(textBox6.Text);
            DateTime timeOfRegistrForPark = Convert.ToDateTime(dateTimePicker1.Value);
            DateTime stayTime = Convert.ToDateTime(dateTimePicker2.Value);
            string picture = FileName;
            string notes = Convert.ToString(richTextBox1.Text);

            Transport trans = new Motocicle(volumeOfEngine, maxSpeed, roadNumber, name, registerNumberForPark, mass, whidth, timeOfRegistrForPark, stayTime, picture, notes);
            ListForTransport.transports.Add(trans);
            //информация внесена, теперь можно очистить поля ввода
            comboBox1.Text = null;
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            pictureBox1.Image = null;
            richTextBox1.Clear();
        }
    }
}
