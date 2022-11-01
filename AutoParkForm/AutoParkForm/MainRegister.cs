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
    public partial class MainRegister : Form
    {
        public MainRegister()
        {
            InitializeComponent();
            transport = new List<Transport> { };
            park = new Park("LuxuryPark", transport);
            //новый спосбо задания времени регистрации
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "MM/dd/yyyy";
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            //и времени пребывания
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "MM/dd/yyyy";
            dateTimePicker2.ValueChanged += dateTimePicker1_ValueChanged;
        }
        //============
        internal static List<Transport> transport { get; set; }
        Park park;
        string FileName;
        //============
        private void button1_Click(object sender, EventArgs e)  //вывод информации
        {
            for (int i = 0; i < transport.Count; i++)
            {
                //label1.Text = transport[i].InfoString();
                label1.Text = park.About();
                pictureBox2.ImageLocation = transport[i].Picture;   //показать изображение
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
            string name = Convert.ToString(textBox1.Text);
            int registerNumberForPark = Convert.ToInt32(textBox2.Text);
            double mass = Convert.ToDouble(textBox3.Text);
            double whidth = Convert.ToDouble(textBox4.Text);
            DateTime timeOfRegistrForPark = Convert.ToDateTime(dateTimePicker1.Value.ToLongTimeString());
            DateTime stayTime = Convert.ToDateTime(dateTimePicker2.Value.ToLongTimeString());
            string picture = FileName;
            string notes = Convert.ToString(richTextBox1.Text);

            Transport trans = new Transport(name, registerNumberForPark, mass, whidth, timeOfRegistrForPark, stayTime, picture, notes);
            transport.Add(trans);
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
            CarRegister gg = new CarRegister();
            gg.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            pictureBox2.Image = null;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
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
            BusRegister gg = new BusRegister();
            gg.Show();
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
