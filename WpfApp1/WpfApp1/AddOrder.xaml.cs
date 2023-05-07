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
    /// Логика взаимодействия для AddOrder.xaml
    /// </summary>
    public partial class AddOrder : Window
    {
        public AddOrder()
        {
            InitializeComponent();
            for (int i = 0; i < ListsDB.customers.Count; i++)
            {
                IDCustPicker.Items.Add(ListsDB.customers[i].ID);
            }
            for (int i = 0; i < ListsDB.transports.Count; i++)
            {
                TransportPicker.Items.Add(ListsDB.transports[i].Naming);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)     //need some fixes
        {
            //try
            //{
            //    Random randomizer = new Random();
            //    double randomNumber = randomizer.Next(10000, 99999);

            //    int idOrd = Convert.ToInt32(randomNumber);
            //    string idCust = Convert.ToString(IDCustPicker.SelectedItem);
            //    DateTime startRent = (DateTime)startRentPicker.SelectedDate;
            //    DateTime endRent = (DateTime)endRentPicker.SelectedDate;
            //    int idTransp = Convert.ToInt32(ListsDB.transports[Convert.ToInt32(TransportPicker.SelectedItem)].RegisterNumberForPark);
            //    double bill = ListsDB.orders[0].CalculateBill();

            //    Order ord = new Order(idOrd, idCust, startRent, endRent, idTransp, bill);
            //    ListsDB.orders.Add(ord);

            //}
            //catch
            //{
            //    MessageBox.Show("Некоторые поля были не заполнены при внесении в базу!", "Сообщение");
            //}
            Random randomizer = new Random();
            double randomNumber = randomizer.Next(10000, 99999);

            int idOrd = Convert.ToInt32(randomNumber);
            string idCust = Convert.ToString(IDCustPicker.SelectedItem);
            DateTime startRent = (DateTime)startRentPicker.SelectedDate;
            DateTime endRent = (DateTime)endRentPicker.SelectedDate;
            int idTransp = Convert.ToInt32(ListsDB.transports[TransportPicker.SelectedIndex].RegisterNumberForPark);
            double bill = ListsDB.orders[0].CalculateBill(startRent, endRent);

            Order ord = new Order(idOrd, idCust, startRent, endRent, idTransp, bill);
            ListsDB.orders.Add(ord);

            //Теперь начинаем внесение в базу данных

            SqlConnection cn = new SqlConnection();     // Объект-соединение
            cn.ConnectionString = DataBase.connectionString;
            // Открытие подключения
            cn.Open();

            try
            {
                Random qrandomizer = new Random();
                double qrandomNumber = randomizer.Next(10000, 99999);

                int qidOrd = Convert.ToInt32(randomNumber);
                string qidCust = Convert.ToString(IDCustPicker.SelectedItem);
                DateTime qstartRent = (DateTime)startRentPicker.SelectedDate;
                DateTime qendRent = (DateTime)endRentPicker.SelectedDate;
                int qidTransp = Convert.ToInt32(ListsDB.transports[TransportPicker.SelectedIndex].RegisterNumberForPark);
                double qbill = ListsDB.orders[0].CalculateBill(qstartRent, qendRent);

                // Создание SQL команды ввода
                string strInsertOrder = string.Format("INSERT INTO Orders VALUES ('{0}','{1}','{2}','{3}','{4}','{5}')", qidOrd, qidCust, qstartRent, qendRent, qidTransp, qbill);

                // Создание объекта-команды
                SqlCommand cmdInsertOrder = new SqlCommand(strInsertOrder, cn);

                // Исполнение команды ввода
                cmdInsertOrder.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Some content was wrong or not input");
            }

            cn.Close();
        }

        private void TransportPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void IDCustPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
