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
    public partial class Form1 : Form
    {
        List<Transport> transport;
        Park park;
        public Form1()
        {
            InitializeComponent();
            transport = new List<Transport> { };
            park = new Park("LuxuryPark", transport);
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < transport.Count; i++)
            {
                label1.Text = transport[i].InfoString();
                label1.Text = park.About();
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
            DateTime timeOfRegistrForPark = Convert.ToDateTime(textBox5.Text);
            DateTime stayTime = Convert.ToDateTime(textBox6.Text);

            Transport trans = new Transport(name, registerNumberForPark, mass, whidth, timeOfRegistrForPark, stayTime);
            transport.Add(trans);
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
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
            Form2 gg = new Form2();
            gg.Show();
        }
    }
}
