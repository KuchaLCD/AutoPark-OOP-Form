using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Data;
using System.Windows.Documents;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Deleting.xaml
    /// </summary>
    public partial class Deleting : Window
    {
        public Deleting()
        {
            InitializeComponent();

            ListBoxForTransp.Items.Clear();
            ListsDB.transports.Clear();
            SqlConnection cn = new SqlConnection();     // Объект-соединение
            cn.ConnectionString = DataBase.connectionString;
            // Открытие подключения
            cn.Open();

            // Формирование команды на языке SQL для выборки данных из таблицы
            string strSelectTransport = "Select * From Transport";

            SqlCommand cmdSelectTransport = new SqlCommand(strSelectTransport, cn);

            SqlDataReader transportsDataReader = cmdSelectTransport.ExecuteReader();

            ListsDB.transports.Clear();    // очистка списка persons
            while (transportsDataReader.Read())
            {
                int id = transportsDataReader.GetInt32(0);
                string naming = transportsDataReader.GetString(1);
                double maxSpeed = transportsDataReader.GetDouble(2);
                double volumeOfEngine = transportsDataReader.GetDouble(3);
                double mass = transportsDataReader.GetDouble(4);
                double whidth = transportsDataReader.GetDouble(5);
                int wheelCount = transportsDataReader.GetInt32(6);
                DateTime timeOfRegistrForPark = Convert.ToDateTime(transportsDataReader.GetString(7));
                DateTime stayTime = Convert.ToDateTime(transportsDataReader.GetString(8));
                string roadNumber = transportsDataReader.GetString(9);
                string picture = transportsDataReader.GetString(10);
                string notes = transportsDataReader.GetString(11);

                // Формирование очередного объекта и помещение его в коллекцию
                if (maxSpeed ==  0 && volumeOfEngine == 0 && wheelCount == 0 && roadNumber == "0")
                {
                    Transport tr = new Transport(id, naming, mass, whidth, timeOfRegistrForPark, stayTime, picture, notes);
                    ListsDB.transports.Add(tr);
                }
                else if (wheelCount == 2)
                {
                    Motocicle moto = new Motocicle(volumeOfEngine, maxSpeed, roadNumber, naming, id, mass, whidth, timeOfRegistrForPark, stayTime, picture, notes);
                    ListsDB.transports.Add(moto);
                }
                else if (wheelCount >= 6)
                {
                    Bus bus = new Bus(volumeOfEngine, maxSpeed, roadNumber, naming, id, mass, whidth, timeOfRegistrForPark, stayTime, picture, notes, wheelCount);
                    ListsDB.transports.Add(bus);
                }
                else
                {
                    Car car = new Car(volumeOfEngine, maxSpeed, roadNumber, naming, id, mass, whidth, timeOfRegistrForPark, stayTime, picture, notes);
                    ListsDB.transports.Add(car);
                }
            }
            // Закрытие соединения
            cn.Close();

            for (int i = 0; i < ListsDB.transports.Count; i++)
            {
                ListBoxForTransp.Items.Add(ListsDB.transports[i]);
            }
        }

        private void DeleteElement_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult Result = MessageBox.Show("Удалить выделенный элемент?", "Message", MessageBoxButton.YesNo);
            if (Result == MessageBoxResult.Yes)
            {
                Transport boofer = (Transport)ListBoxForTransp.SelectedItem;
                int selectedID = boofer.RegisterNumberForPark;
                if (boofer == null)
                {
                    MessageBox.Show("В селективном списке отсутсвуют объекты для удаления, либо не выбран нужный элемент", "Сообщение");
                }
                else
                {
                    ListBoxForTransp.Items.Remove(boofer);
                    ListsDB.transports.Remove(boofer);
                }
                //Удаление из БД
                SqlConnection cn = new SqlConnection();     // Объект-соединение

                cn.ConnectionString = DataBase.connectionString;
                // Открытие подключения
                cn.Open();
                string strDeleteTransport = string.Format($"DELETE FROM Transport WHERE (RegisterNumberForPark = '{selectedID}')");

                SqlCommand cmdDeleteTransport = new SqlCommand(strDeleteTransport, cn);

                // Исполнение команды 
                try
                {
                    cmdDeleteTransport.ExecuteNonQuery();
                }
                catch
                {
                    MessageBox.Show("Что-то пошло не так");
                }
                cn.Close();
            }
        }

        private void GoOut_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult Result = MessageBox.Show("Прекратить удаление?", "Message", MessageBoxButton.YesNo);
            if (Result == MessageBoxResult.Yes)
            {
                Hide();
                this.Close();
            }
        }
    }
}
