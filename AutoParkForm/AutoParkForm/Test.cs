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
    public partial class Test : Form
    {
        public Test()
        {
            //Здесь представлены все основные настройки отображения
            InitializeComponent();
            park = new Park("LuxuryPark", ListForTransport.transports);
        }
        string FileName;    //Полный функционал этого поля представлен в пользовательской и административной версиях
        Park park;
        private void button5_Click(object sender, EventArgs e)
        {
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
        
        private void button4_Click(object sender, EventArgs e)
        {
            //По нажатию данной кнопки производится очистка вывода
            richTextBox2.Text = "";     //очищаем поле вывода
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //По нажатию данной кнопки производится вывод информации об активах парка
            if (ListForTransport.transports.Count == 0)
            {
                richTextBox2.Text = "Парк пуст";
            }
            else
            {
                richTextBox2.Text = park.About();
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
            }
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            double volumeOfEngine = 10;
            double maxSpeed = 255;
            string roadNumber = "AM-2377 EI-1";
            string name = "Scania";
            int registerNumberForPark = 320428731;
            double mass = 3000;
            double whidth = 2.5;
            double countOfWheels = 8;
            DateTime timeOfRegistrForPark = new DateTime(2022, 7, 20, 18, 30, 00);
            DateTime stayTime = new DateTime(2022, 7, 23, 18, 30, 00);
            string picture = FileName;
            string notes = "New bus";

            Transport trans = new Bus(volumeOfEngine, maxSpeed, roadNumber, name, registerNumberForPark, mass, whidth, timeOfRegistrForPark, stayTime, picture, notes, countOfWheels);
            ListForTransport.transports.Add(trans);
            DialogResult res = MessageBox.Show("Создан новый элемент - автобус", "Сообщение");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double volumeOfEngine = 1.5;
            double maxSpeed = 255;
            string roadNumber = "MC-9376 EI-1";
            string name = "Yamaha";
            int registerNumberForPark = 732428942;
            double mass = 590;
            double whidth = 2.5;
            DateTime timeOfRegistrForPark = new DateTime(2022, 7, 20, 18, 30, 00);
            DateTime stayTime = new DateTime(2022, 7, 23, 18, 30, 00);
            string picture = FileName;
            string notes = "New Motocicle";

            Transport trans = new Motocicle(volumeOfEngine, maxSpeed, roadNumber, name, registerNumberForPark, mass, whidth, timeOfRegistrForPark, stayTime, picture, notes);
            ListForTransport.transports.Add(trans);
            DialogResult res = MessageBox.Show("Создан новый элемент - мотоцикл", "Сообщение");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            double volumeOfEngine = 3.5;
            double maxSpeed = 255;
            string roadNumber = "GR-2344 EI-1";
            string name = "Toyota Landcruser Prada";
            int registerNumberForPark = 982347136;
            double mass = 1650;
            double whidth = 2.5;
            DateTime timeOfRegistrForPark = new DateTime(2022, 7, 20, 18, 30, 00);
            DateTime stayTime = new DateTime(2022, 7, 23, 18, 30, 00);
            string picture = FileName;
            string notes = "New car";

            Transport trans = new Car(volumeOfEngine, maxSpeed, roadNumber, name, registerNumberForPark, mass, whidth, timeOfRegistrForPark, stayTime, picture, notes);
            ListForTransport.transports.Add(trans);
            DialogResult res = MessageBox.Show("Создан новый элемент - машина", "Сообщение");
        }

        private void Test_Load(object sender, EventArgs e)
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
            }
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

        private void button9_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("В данной версии не нужно вводить данные с клавиатуры)) \nДостаточно просто нажимать на кнопки", "Сообщение");
        }
    }
}
