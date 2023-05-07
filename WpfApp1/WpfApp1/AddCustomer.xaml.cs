using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data.SqlClient;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для AddCustomer.xaml
    /// </summary>
    public partial class AddCustomer : Window
    {
        public AddCustomer()
        {
            InitializeComponent();
            SqlConnection cn = new SqlConnection();     // Объект-соединение
            cn.ConnectionString = DataBase.connectionString;
            // Открытие подключения
            cn.Open();
            string strSelectTransport = "Select IDPos, PosName From Positions";
            SqlCommand cmdSelectTransport = new SqlCommand(strSelectTransport, cn);
            SqlDataReader transportsDataReader = cmdSelectTransport.ExecuteReader();

            while (transportsDataReader.Read())
            {
                string id = transportsDataReader.GetString(0);
                string posName = transportsDataReader.GetString(1);
                PositionCather.Items.Add(posName);
            }
            // Закрытие соединения
            cn.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Random randomizer = new Random();
                double randomNumber = randomizer.Next(1000, 9999);

                string id = Convert.ToString(randomNumber);
                string firstName = FirstName.Text;
                string sureName = SureName.Text;
                string lastName = LastName.Text;
                string idPos = PositionCather.Text;
                switch (PositionCather.SelectedItem)
                {
                    case "Admin":
                        idPos = "0001";
                        break;
                    case "Lider":
                        idPos = "0002";
                        break;
                    case "PM":
                        idPos = "2231";
                        break;
                    case "NoWork":
                        idPos = "4044";
                        break;
                    case "BA":
                        idPos = "4343";
                        break;
                }

                Customer cust = new Customer(id, firstName, sureName, lastName, idPos);
                ListsDB.customers.Add(cust);

            }
            catch
            {
                MessageBox.Show("Некоторые поля были не заполнены при внесении в базу!", "Сообщение");
            }

            //Теперь начинаем внесение в базу данных

            SqlConnection cn = new SqlConnection();     // Объект-соединение
            cn.ConnectionString = DataBase.connectionString;
            // Открытие подключения
            cn.Open();

            try
            {
                Random trandomizer = new Random();
                double trandomNumber = trandomizer.Next(1000, 9999);

                string id = Convert.ToString(trandomNumber);
                string firstName = FirstName.Text;
                string sureName = SureName.Text;
                string lastName = LastName.Text;
                string idPos = PositionCather.Text;
                switch (PositionCather.SelectedItem)
                {
                    case "Admin":
                        idPos = "0001";
                        break;
                    case "Lider":
                        idPos = "0002";
                        break;
                    case "PM":
                        idPos = "2231";
                        break;
                    case "NoWork":
                        idPos = "4044";
                        break;
                    case "BA":
                        idPos = "4343";
                        break;
                }

                // Создание SQL команды ввода
                string strInsertTransport = string.Format("INSERT INTO Customer VALUES ('{0}','{1}','{2}','{3}','{4}')", id, firstName, sureName, lastName, idPos);

                // Создание объекта-команды
                SqlCommand cmdInsertTransport = new SqlCommand(strInsertTransport, cn);

                // Исполнение команды ввода
                cmdInsertTransport.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Some content was wrong or not input");
            }

            cn.Close();

            FirstName.Clear();
            SureName.Clear();
            LastName.Clear();
        }

        private void PositionCather_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
