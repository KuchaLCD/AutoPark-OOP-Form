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
    /// Логика взаимодействия для Lab2.xaml
    /// </summary>
    public partial class Lab2 : Window
    {
        DataTable positions;
        DataTable customer;
        DataSet dataSet;
        SqlDataAdapter adapter1;
        SqlDataAdapter adapter2;

        public Lab2()
        {
            InitializeComponent();
            //----------------------------Таблица Positions-----------------------------------
            //--------------------------------------------------------------------------------
            positions = new DataTable("Positions");

            // Три столбца (как в БД -> следите чтобы типы данных совпадали)
            var posId = new DataColumn("IDPos", typeof(string));
            var posName = new DataColumn("PosName", typeof(string));
            var posSalary = new DataColumn("Salary", typeof(int));

            // Добавляем 3-ри столбца в таблицу Positions (Должности)
            positions.Columns.AddRange(new[] { posId, posName, posSalary });
            customer = new DataTable("Customer");

            // 5-ть столбцов
            var iDCust = new DataColumn("IDCust", typeof(string));
            var firstName = new DataColumn("FirstName", typeof(string));
            var sureName = new DataColumn("SureName", typeof(string));
            var lastName = new DataColumn("LastName", typeof(string));
            var iDPos = new DataColumn("IDPos", typeof(string));

            // Добавляем столбцы в табл.
            customer.Columns.AddRange(new[] { iDCust, firstName, sureName, lastName, iDPos });
            //--------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------

            // Определяем первичные ключи
            positions.PrimaryKey = new[] { positions.Columns["IDPos"] };
            customer.PrimaryKey = new[] { customer.Columns["IDCust"] };

            // Создаем схему рассоединённого набора данных
            dataSet = new DataSet("Employees");
            // Добавляем таблицы
            dataSet.Tables.Add(positions);
            dataSet.Tables.Add(customer);

            var positionsToPersons = new DataRelation("PositionsToPersons", positions.Columns["IDPos"], customer.Columns["IDPos"]);
            dataSet.Relations.Add(positionsToPersons);
            //--------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------
        }

        private void DataGridViewCust_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Загружаем данные       
            //--------------------------------------------------------------------------------
            // cn - это объект-соединение с БД
            SqlConnection cn = new SqlConnection();

            cn.ConnectionString = DataBase.connectionString;

            // Можно явно не открывать и не закрываеть соединение, метод Fill адаптера сам все сделает
            //// Открытие подключения
            //cn.Open();
            //// Закрытие соединения
            //cn.Close();


            // Заполняем список должностей
            //--------------------------------------------------------------------------------            
            // Формируем команду на языке SQL для выборки данных из таблицы
            string strSelectPos = "Select * From Positions";
            // Передаем адаптеру в качестве входных параметров: текст запроса и строку подключения
            adapter1 = new SqlDataAdapter(strSelectPos, cn);
            // С помощью метода Fill заполняем таблицу, которую мы создали на стороне клиента 
            adapter1.Fill(positions);
            // Заполняем список должностей
            //PositionLogger2.ItemsSource = (System.Collections.IEnumerable)positions;
            PositionLogger2.DisplayMemberPath = "PosName";
            //PositionLogger2.ItemsSource = "IDPos";
            //--------------------------------------------------------------------------------

            string strSelectPers = "Select * From Customer";
            // Передаем адаптеру текст запроса и строку подключения
            adapter2 = new SqlDataAdapter(strSelectPers, cn);
            // Заполняем таблицу "Persons"
            adapter2.Fill(customer);
            // Если до этого была привязка к источнику, очищаем
            DataGridViewCust2.ItemsSource = null;
            // Отображаем таблицу "Persons" на форме в dataGridView
            DataGridViewCust2.ItemsSource = customer.AsDataView();
            //--------------------------------------------------------------------------------
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Вводим
            // 1 - создали пустую строку с требуемой структурой
            DataRow row = customer.NewRow();
            // 2 - заполняем поля строки
            row["IDCust"] = Convert.ToString(DataGridViewCust2.Items.Count);
            row["FirstName"] = FirstName2.Text;
            row["SureName"] = SureName2.Text;
            row["LastName"] = LastName2.Text;
            row["IDPos"] = PositionLogger2.SelectedValue;

            // добавляем данные в таблицу
            customer.Rows.Add(row);



            // Сохраняем в БАЗЕ ДАННЫХ
            // создаем объект SqlCommandBuilder
            SqlCommandBuilder commandBuilder1 = new SqlCommandBuilder(adapter1);
            adapter1.Update(positions);

            SqlCommandBuilder commandBuilder2 = new SqlCommandBuilder(adapter2);
            adapter2.Update(customer);

            // очищаем полностью DataSet
            dataSet.Clear();
            // заново получаем данные из бд
            // перезагружаем данные
            adapter1.Fill(positions);
            adapter2.Fill(customer);

            // Очищаем поля ввода
            FirstName2.Text = string.Empty;
            SureName2.Text = string.Empty;
            LastName2.Text = string.Empty;
            PositionLogger2.Text = string.Empty;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {

        }
    }
}