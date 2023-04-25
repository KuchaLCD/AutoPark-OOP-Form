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
    /// Логика взаимодействия для Deleting.xaml
    /// </summary>
    public partial class Deleting : Window
    {
        public Deleting()
        {
            InitializeComponent();

            ListBoxForTransp.Items.Clear();
            ListForTransport.transports.Clear();
            SqlConnection cn = new SqlConnection();     // Объект-соединение
            cn.ConnectionString = DataBase.connectionString;
            // Открытие подключения
            cn.Open();

            // Формирование команды на языке SQL для выборки данных из таблицы
            string strSelectTransport = "Select * From Transport";

            SqlCommand cmdSelectTransport = new SqlCommand(strSelectTransport, cn);

            SqlDataReader transportsDataReader = cmdSelectTransport.ExecuteReader();

            ListForTransport.transports.Clear();    // очистка списка persons
            while (transportsDataReader.Read())
            {
                int id = transportsDataReader.GetInt32(0);
                string naming = transportsDataReader.GetString(1);
                double mass = transportsDataReader.GetDouble(2);
                double wight = transportsDataReader.GetDouble(3);
                DateTime timeOfRegistrForPark = Convert.ToDateTime(transportsDataReader.GetString(4));
                DateTime stayTime = Convert.ToDateTime(transportsDataReader.GetString(5));
                string picture = transportsDataReader.GetString(6);
                string notes = transportsDataReader.GetString(7);

                // Формирование очередного объекта и помещение его в коллекцию
                Transport tr = new Transport(id, naming, mass, wight, timeOfRegistrForPark, stayTime, picture, notes);
                ListForTransport.transports.Add(tr);
            }
            // Закрытие соединения
            cn.Close();

            for (int i = 0; i < ListForTransport.transports.Count; i++)
            {
                ListBoxForTransp.Items.Add(ListForTransport.transports[i]);
            }
        }

        private void DeleteElement_Click(object sender, RoutedEventArgs e)
        {
            Transport boofer = (Transport)ListBoxForTransp.SelectedItem;
            int selectedID = boofer.RegisterNumberForPark;        //need some fixes
            if (boofer == null)
            {
                MessageBox.Show("В селективном списке отсутсвуют объекты для удаления, либо не выбран нужный элемент", "Сообщение");
            }
            else
            {
                ListBoxForTransp.Items.Remove(boofer);
                ListForTransport.transports.Remove(boofer);
            }

            
            //Удаление инфы о парке
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
                MessageBox.Show("Complete!");
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так");
            }
            cn.Close();
            
        }

        private void GoOut_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult Result = MessageBox.Show("Прекратить удаление?", "Message", MessageBoxButton.YesNo);
            if (Result == MessageBoxResult.Yes)
            {
                Hide();
                LidVersion Lid = new LidVersion();
                Lid.ShowDialog();
                this.Close();
            }
        }
    }
}
