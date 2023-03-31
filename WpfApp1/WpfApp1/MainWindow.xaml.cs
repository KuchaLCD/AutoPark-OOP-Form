using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            string sqlExpression = "SELECT * FROM Users";

            using (SqlConnection connection = new SqlConnection(DataBase.connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                string login = string.Empty;
                string password = string.Empty;

                while(reader.Read())
                {
                    login = reader.GetString(0);
                    password = reader.GetString(1);
                }
                if (PassBox.Password == password && Login.Text == login)
                {
                    Hide();
                    User UserVers = new User();
                    UserVers.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Some content was wrong. Check for right input data", "Сообщение");
                }
                reader.Close();
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // TextBox_TextChanged.PasswordChar = '*';
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string password = PassBox.Password;
            if (ShowingPass.Text == string.Empty)
            {
                ShowingPass.Visibility = Visibility.Visible;
                ShowingPass.Text = password;
            }
            else if (ShowingPass.Visibility == Visibility.Hidden)
            {
                ShowingPass.Visibility = Visibility.Visible;
                ShowingPass.Text = password;
            }
            else if (ShowingPass.Text != null)
            {
                ShowingPass.Visibility = Visibility.Hidden;
            }
            
        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {

        }
    }
}
