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
    public partial class Connecting : Form
    {
        public Connecting()
        {
            InitializeComponent();
            this.TopMost = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            Test TestVers = new Test();
            TestVers.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            MainRegister AdminVers = new MainRegister();
            AdminVers.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Hide();
            UserRegister UserVers = new UserRegister();
            UserVers.ShowDialog();
            this.Close();
        }

        private void Connecting_Load(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Вы действительно хотите закрыть приложение?", "Сообщение", MessageBoxButtons.YesNo,
            MessageBoxIcon.Information,
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.DefaultDesktopOnly);
            if (res == DialogResult.Yes)
            {
                this.Close();
            }
            this.TopMost = true;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
