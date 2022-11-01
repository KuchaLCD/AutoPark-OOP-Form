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
    public partial class CarRegister : Form
    {
        string FileName;
        public CarRegister()
        {
            InitializeComponent();
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            //новый спосбо задания времени регистрации
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "MM/dd/yyyy hh:mm:ss";
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            //и времени пребывания
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "MM/dd/yyyy hh:mm:ss";
            dateTimePicker2.ValueChanged += dateTimePicker1_ValueChanged;
            //несколько готовых вариантов серийных моделей автомобилей
            comboBox1.Items.Add("Toyota Landcruser Prada");
            comboBox1.Items.Add("Mersedes S300");
            comboBox1.Items.Add("Renault Captur");
            comboBox1.Items.Add("Kia Cerato");
            comboBox1.Items.Add("Honda Civic");
            comboBox1.Items.Add("Ford Mustang");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            double volumeOfEngine = Convert.ToDouble(textBox1.Text);
            double maxSpeed = Convert.ToDouble(textBox2.Text);
            string roadNumber = Convert.ToString(textBox3.Text);
            string name = comboBox1.Text;
            int registerNumberForPark = Convert.ToInt32(textBox4.Text);
            double mass = Convert.ToDouble(textBox5.Text);
            double whidth = Convert.ToDouble(textBox6.Text);
            DateTime timeOfRegistrForPark = Convert.ToDateTime(dateTimePicker1.Value.ToLongTimeString());
            DateTime stayTime = Convert.ToDateTime(dateTimePicker2.Value.ToLongTimeString());
            string picture = FileName;
            string notes = Convert.ToString(richTextBox1.Text);

            Transport trans = new Car(volumeOfEngine, maxSpeed, roadNumber, name, registerNumberForPark, mass, whidth, timeOfRegistrForPark, stayTime, picture, notes);
            MainRegister.transport.Add(trans);
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

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.ShowDialog();
            FileName = openFile.FileName;
            pictureBox1.ImageLocation = FileName;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
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

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
