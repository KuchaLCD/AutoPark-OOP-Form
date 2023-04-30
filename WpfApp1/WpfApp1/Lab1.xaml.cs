using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Lab1.xaml
    /// </summary>
    public partial class Lab1 : Window
    {
        public Lab1()
        {
            InitializeComponent();
            PositionLogger.Items.Add("Hryack");
            PositionLogger.Items.Add("DishWash");
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        //Кнопка Запись
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection cn = new SqlConnection();     // Объект-соединение
            cn.ConnectionString = DataBase.connectionString;
            // Открытие подключения
            cn.Open();
            try
            {
                // Формирование данных для ввода в таблицу бызы (считывание из полей textBox)
                string id = Convert.ToString(DataGridViewCust.Items.Count);
                string fnm = FirstName.Text;
                string snm = SureName.Text;
                string lnm = LastName.Text;
                // получаем id выделенного объекта
                string pos = (string)PositionLogger.SelectedValue;
                string IDpos = IDPosition.Text;
                //new way to fill the id
                switch (PositionLogger.SelectedValue)
                {
                    case "Hryack":
                        IDpos = "2231"; break;
                    case "DishWash":
                        IDpos = "4343"; break;
                }
                // Создание SQL команды ввода
                string strInsertCustomers = string.Format("INSERT INTO Customer VALUES ('{0}','{1}','{2}','{3}','{4}')", id, fnm, snm, lnm, IDpos);

                // Посмотрим на экране сформированную SQL-команду ввода
                // MessageBox.Show("Сформирована команда ввода:\n\n" + strInsertPersons + "\n\nПрочитайте введенные данные");

                // Создание объекта-команды
                SqlCommand cmdInsertCustomers = new SqlCommand(strInsertCustomers, cn);

                // Исполнение команды ввода
                cmdInsertCustomers.ExecuteNonQuery();

                FirstName.Text = string.Empty;
                SureName.Text = string.Empty;
                LastName.Text = string.Empty;
                IDPosition.Text = string.Empty;
            }
            catch
            {
                if (FirstName.Text.Length > 20 || SureName.Text.Length > 20 || LastName.Text.Length > 20 || IDPosition.Text.Length > 10 || IDPosition.Text.Length > 4)
                {
                    MessageBox.Show("Возможно одно из вводимых полей слишком большое. Проверьте все на наличие выхода из ограничений");
                }
                MessageBox.Show("Произошла ошибка ввода данных. Нажмите на кнопку Загрузить данные");
            }

            cn.Close();

            // Очищаем поля ввода
            // Перепись объектов Person в dataGridView1
            // dataGridView1.DataSource = persons;

            //---------------Функция автообновления-----------------------
            DataGridViewCust.ItemsSource = null;
            cn.Close();

            cn.ConnectionString = DataBase.connectionString;
            // Открытие подключения
            cn.Open();

            // Формирование команды на языке SQL для выборки данных из таблицы
            string strSelectPersons = "Select * From Customer";

            SqlCommand cmdSelectCustomers = new SqlCommand(strSelectPersons, cn);

            SqlDataReader customersDataReader = cmdSelectCustomers.ExecuteReader();

            ListsDB.customers.Clear();    // очистка списка persons
            while (customersDataReader.Read())
            {
                string id = customersDataReader.GetString(0);
                string firstName = customersDataReader.GetString(1);
                string sureName = customersDataReader.GetString(2);
                string lastName = customersDataReader.GetString(3);
                string pos = customersDataReader.GetString(4);

                // Формирование очередного объекта типа Person и помещение его в коллекцию
                Customer cs = new Customer(id, firstName, sureName, lastName, pos);
                ListsDB.customers.Add(cs);
            }
            // Закрытие соединения
            cn.Close();
            // Перепись объектов Person в dataGridView1
            DataGridViewCust.ItemsSource = ListsDB.customers;
        }
        //Кнопка Загрузить данные
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DataGridViewCust.ItemsSource = null;
            // ****************************************** !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            // cn - это объект-соединение с БД
            SqlConnection cn = new SqlConnection();

            // ConnectionString - это строка подключения к БД (одно из свойств подключения)
            // Подключаемся к локальному серверу (local)\SQLEXPRESS, к базе TestBase1
            cn.ConnectionString = DataBase.connectionString;


            //cn.ConnectionString = @"Data Source=kisel\SQLEXPRESS;" +
            //                         "Integrated Security=SSPI;" +
            //                         "Initial Catalog=TestBase1";

            // Открытие подключения
            cn.Open();

            // Формирование команды на языке SQL для выборки данных из таблицы
            string strSelectPersons = "Select * From Customer";

            // Создание объекта-команды 
            // параметры: SQL-команда (strSelectPersons) и соединение (cn)
            SqlCommand cmdSelectCustomers = new SqlCommand(strSelectPersons, cn);

            // Исполнение команды
            // Результат исполнения: объект personsDataReader типа SqlDataReader,
            // представляющий собой в нашем случае поток строк из таблицы Persons
            SqlDataReader customersDataReader = cmdSelectCustomers.ExecuteReader();

            /*         
            -----------------------------------------------------------------------------
              Т.к. personsDataReader- это поток, то данные из него
              можно считывать только поэлементно (элемент потока здесь строка из таблицы БД)
              
              Для того, чтобы "разобрать" строку на составные части (атрибуты), 
              используется методы GetType(i), где:
              - Type - тип данных атрибута,
              - (i) - номер столбца атрибута (нумерация с нуля).
              Например, метод GetString(3) выбирает пол из поля 3 (4-й столбец таблицы)  
             
              Далее: Считывание данных из потока данных и помещение их в коллекцию persons
            */

            ListsDB.customers.Clear();    // очистка списка 
            while (customersDataReader.Read())
            {
                string id = customersDataReader.GetString(0);
                string firstName = customersDataReader.GetString(1);
                string sureName = customersDataReader.GetString(2);
                string lastName = customersDataReader.GetString(3);
                string pos = customersDataReader.GetString(4);

                // Формирование очередного объекта типа Person и помещение его в коллекцию
                Customer cs = new Customer(id, firstName, sureName, lastName, pos);
                ListsDB.customers.Add(cs);
            }

            // Закрытие соединения
            cn.Close();


            // Перепись объектов Person в dataGridView
            DataGridViewCust.ItemsSource = ListsDB.customers;
        }

        private void DataGridViewCust_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        /* Кнопка удаления элемента из DataGrid*/
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SqlConnection cn = new SqlConnection();     // Объект-соединение

            cn.ConnectionString = DataBase.connectionString;

            int selectedID = DataGridViewCust.SelectedIndex;
            //selectedID2 = DataGridViewCust.SelectedItem.ToString();       -not work

            // Открытие подключения
            cn.Open();
            string strDeleteCustomers = string.Format($"DELETE FROM Customer WHERE (IDCust = '{selectedID}')");

            SqlCommand cmdDeleteCustomers = new SqlCommand(strDeleteCustomers, cn);

            // Исполнение команды ввода
            try
            {
                cmdDeleteCustomers.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так");
            }
            //---------------Функция автообновления-----------------------
            DataGridViewCust.ItemsSource = null;
            cn.Close();

            cn.ConnectionString = DataBase.connectionString;
            // Открытие подключения
            cn.Open();

            // Формирование команды на языке SQL для выборки данных из таблицы
            string strSelectPersons = "Select * From Customer";

            SqlCommand cmdSelectCustomers = new SqlCommand(strSelectPersons, cn);

            SqlDataReader customersDataReader = cmdSelectCustomers.ExecuteReader();

            ListsDB.customers.Clear();    // очистка списка persons
            while (customersDataReader.Read())
            {
                string id = customersDataReader.GetString(0);
                string firstName = customersDataReader.GetString(1);
                string sureName = customersDataReader.GetString(2);
                string lastName = customersDataReader.GetString(3);
                string pos = customersDataReader.GetString(4);

                // Формирование очередного объекта типа Person и помещение его в коллекцию
                Customer cs = new Customer(id, firstName, sureName, lastName, pos);
                ListsDB.customers.Add(cs);
            }
            // Закрытие соединения
            cn.Close();
            // Перепись объектов Person в dataGridView1
            DataGridViewCust.ItemsSource = ListsDB.customers;
        }
        //Кнопка Обновить
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            DataGridViewCust.ItemsSource = null;
            SqlConnection cn = new SqlConnection();

            cn.ConnectionString = DataBase.connectionString;
            // Открытие подключения
            cn.Open();

            // Формирование команды на языке SQL для выборки данных из таблицы
            string strSelectPersons = "Select * From Customer";

            SqlCommand cmdSelectCustomers = new SqlCommand(strSelectPersons, cn);

            SqlDataReader customersDataReader = cmdSelectCustomers.ExecuteReader();

            ListsDB.customers.Clear();    // очистка списка persons
            while (customersDataReader.Read())
            {
                string id = customersDataReader.GetString(0);
                string firstName = customersDataReader.GetString(1);
                string sureName = customersDataReader.GetString(2);
                string lastName = customersDataReader.GetString(3);
                string pos = customersDataReader.GetString(4);

                // Формирование очередного объекта типа Person и помещение его в коллекцию
                Customer cs = new Customer(id, firstName, sureName, lastName, pos);
                ListsDB.customers.Add(cs);
            }
            // Закрытие соединения
            cn.Close();
            // Перепись объектов Person в dataGridView1
            DataGridViewCust.ItemsSource = ListsDB.customers;
        }
        //Кнопка Очистить
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            DataGridViewCust.ItemsSource = null;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            MessageBoxResult Result = MessageBox.Show("Вернутся на главную?", "Message", MessageBoxButton.YesNo);
            if (Result == MessageBoxResult.Yes)
            {
                Hide();
                MainWindow Main = new MainWindow();
                Main.ShowDialog();
                this.Close();
            }
        }

        private void TextBox_TextChanged_3(object sender, TextChangedEventArgs e)
        {

        }
    }
}
