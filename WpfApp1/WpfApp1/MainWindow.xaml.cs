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
        public string login = string.Empty;
        public string password = string.Empty;
        public string firstName = string.Empty;
        public string sureName = string.Empty;
        public string idPos = string.Empty;
        public string avatar = string.Empty;
        public MainWindow()
        {
            InitializeComponent();

            for (int i = 0; i < ListsDB.users.Count; i++)
            {
                UserList.Items.Add(ListsDB.users[i]);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int controlSum = 0;
            ListsDB.users.Clear();      //предварительное очищение списка пользователелей
            string sqlExpression = "SELECT * FROM Users";

            using (SqlConnection connection = new SqlConnection(DataBase.connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    login = reader.GetString(0);
                    password = reader.GetString(1);
                    firstName = reader.GetString(2);
                    sureName = reader.GetString(3);
                    idPos = reader.GetString(4);
                    avatar = reader.GetString(5);

                    if (PassBox.Password == "user" && Login.Text == "user")
                    {
                        controlSum++;
                        Hide();
                        User UserVers = new User();
                        UserVers.ShowDialog();
                        this.Close();
                    }
                    else if (PassBox.Password == "lider" && Login.Text == "lider")
                    {
                        controlSum++;
                        Hide();
                        LidVersion LidVers = new LidVersion();
                        LidVers.ShowDialog();
                        this.Close();
                    }
                    else if (PassBox.Password == "style" && Login.Text == "style")
                    {
                        controlSum++;
                        Hide();
                        StyleUI style = new StyleUI();
                        style.ShowDialog();
                        this.Close();
                    }
                    else if (PassBox.Password == password && Login.Text == login)
                    {
                        controlSum++;
                        UserDB newUser = new UserDB(login, password, firstName, sureName, idPos, avatar);
                        ListsDB.users.Add(newUser);

                        Hide();
                        //UserInterface User = new UserInterface();
                        //User.ShowDialog();
                        StyleUI style = new StyleUI();
                        style.ShowDialog();
                        this.Close();
                    }
                }
                
                reader.Close();
                if (controlSum < 1 || PassBox.Password == null || Login.Text == string.Empty)
                {
                    MessageBox.Show("Some content was wrong or input data is nondetected! \nPlease check for right input data or fill the fields for login/password.");
                    PassBox.Password = null;
                    Login.Text = string.Empty;
                }
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

        private void Chiter_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            User UserVers = new User();
            UserVers.ShowDialog();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MessageBoxResult Result = MessageBox.Show("Закрыть приложение ?", "Message", MessageBoxButton.YesNo);
            if (Result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void LidChit_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            LidVersion LidVers = new LidVersion();
            LidVers.ShowDialog();
            this.Close();
        }

        private void UserList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserDB selectedUser = (UserDB)UserList.SelectedItem;
            Login.Text = selectedUser.Login;
            PassBox.Password = selectedUser.Password;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (PassBox.Password == null || Login.Text == string.Empty)
            {
                ListsDB.users.Clear();
            }
            Hide();
            StyleUI style = new StyleUI();
            style.ShowDialog();
            this.Close();
        }
    }
}
