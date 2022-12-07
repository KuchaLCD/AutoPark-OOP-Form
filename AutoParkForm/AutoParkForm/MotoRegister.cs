﻿using System;
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
            comboBox1.Items.Add("Toyota Landcruser Prada");
            comboBox1.Items.Add("Mercedes S300");
            comboBox1.Items.Add("Renault Captur");
            comboBox1.Items.Add("Kia Cerato");
            comboBox1.Items.Add("Honda Civic");
            comboBox1.Items.Add("Ford Mustang");
            comboBox1.Items.Add("Volksvagen Passat B7");
            comboBox1.Items.Add("Audi TT");
            comboBox1.Items.Add("Audi A7");
            comboBox1.Items.Add("Toyota Camry 3.5");
            comboBox1.Items.Add("BMW X5");
            comboBox1.Items.Add("BMW X7");
            comboBox1.Items.Add("Волга 21");
            comboBox1.Items.Add("Kia Sportage 4");
            comboBox1.Items.Add("Kia Rio");
            comboBox1.Items.Add("Skoda Rapid");
        }

        private void MotoRegister_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random randomizer = new Random();
            double randomNumber = randomizer.Next(100000, 999999);
            textBox4.Text = randomNumber.ToString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //автозаполнение
            switch (comboBox1.SelectedItem)
            {
                case "Toyota Landcruser Prada":
                    textBox1.Text = "255";     //макс. скорость 
                    textBox2.Text = "3,5";     //объем двигателя
                    textBox5.Text = "1700";     //масса
                    textBox6.Text = "2,5";     //ширина
                    break;
                case "Mercedes S300":
                    textBox1.Text = "270";
                    textBox2.Text = "2,5";
                    textBox5.Text = "1650";
                    textBox6.Text = "2,4";
                    break;
                case "Renault Captur":
                    textBox1.Text = "250";
                    textBox2.Text = "1,9";
                    textBox5.Text = "1550";
                    textBox6.Text = "2,1";
                    break;
                case "Kia Cerato":
                    textBox1.Text = "240";
                    textBox2.Text = "2,1";
                    textBox5.Text = "1700";
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
            //добавляем изображение мотоцикла
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
            //По нажатию данной кнопки производится запись(регистрация) машины
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
