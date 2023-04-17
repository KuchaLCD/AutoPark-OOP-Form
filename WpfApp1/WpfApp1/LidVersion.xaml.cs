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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для LidVersion.xaml
    /// </summary>
    public partial class LidVersion : Window
    {
        List<Customer> customers = new List<Customer>();
        Park park;
        string FileName;
        public LidVersion()
        {
            park = new Park("LuxuryPark", ListForTransport.transports);
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_3(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            FlowDocument document = new FlowDocument();
            Paragraph paragraph = new Paragraph();
            paragraph.Inlines.Add(new Run(park.About()));
            document.Blocks.Add(paragraph);
            Output.Document = document;

            //Output.Document = "DNLDINVLNvlnvslnvdinviNSDVNsodnv" +
            //    "\nDNLDINVLNvlnvslnvdinviNSDVNsodnv" +
            //    "\nDNLDINVLNvlnvslnvdinviNSDVNsodnv" +
            //    "\nDNLDINVLNvlnvslnvdinviNSDVNsodnv" +
            //    "\nDNLDINVLNvlnvslnvdinviNSDVNsodnv" +
            //    "\nDNLDINVLNvlnvslnvdinviNSDVNsodnv" +
            //    "\nDNLDINVLNvlnvslnvdinviNSDVNsodnv" +
            //    "\nDNLDINVLNvlnvslnvdinviNSDVNsodnv" +
            //    "\nDNLDINVLNvlnvslnvdinviNSDVNsodnv" +
            //    "\nDNLDINVLNvlnvslnvdinviNSDVNsodnv" +
            //    "\nDNLDINVLNvlnvslnvdinviNSDVNsodnv" +
            //    "\nDNLDINVLNvlnvslnvdinviNSDVNsodnv" +
            //    "\nDNLDINVLNvlnvslnvdinviNSDVNsodnv" +
            //    "\nDNLDINVLNvlnvslnvdinviNSDVNsodnv" +
            //    "\nDNLDINVLNvlnvslnvdinviNSDVNsodnv" +
            //    "\nDNLDINVLNvlnvslnvdinviNSDVNsodnv" +
            //    "\nDNLDINVLNvlnvslnvdinviNSDVNsodnv" +
            //    "\nDNLDINVLNvlnvslnvdinviNSDVNsodnv" +
            //    "\nDNLDINVLNvlnvslnvdinviNSDVNsodnv" +
            //    "\nDNLDINVLNvlnvslnvdinviNSDVNsodnv" +
            //    "\nDNLDINVLNvlnvslnvdinviNSDVNsodnv" +
            //    "\nDNLDINVLNvlnvslnvdinviNSDVNsodnv" +
            //    "\nDNLDINVLNvlnvslnvdinviNSDVNsodnv" +
            //    "\nDNLDINVLNvlnvslnvdinviNSDVNsodnv" +
            //    "\nDNLDINVLNvlnvslnvdinviNSDVNsodnv" +
            //    "\nDNLDINVLNvlnvslnvdinviNSDVNsodnv" +
            //    "\nDNLDINVLNvlnvslnvdinviNSDVNsodnv" +
            //    "\nDNLDINVLNvlnvslnvdinviNSDVNsodnv" +
            //    "\nDNLDINVLNvlnvslnvdinviNSDVNsodnv" +
            //    "\nDNLDINVLNvlnvslnvdinviNSDVNsodnv" +
            //    "\nDNLDINVLNvlnvslnvdinviNSDVNsodnv" +
            //    "\nDNLDINVLNvlnvslnvdinviNSDVNsodnv" +
            //    "\nDNLDINVLNvlnvslnvdinviNSDVNsodnv";
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

        }

        private void Output_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ClearOutput_Click(object sender, RoutedEventArgs e)
        {
            FlowDocument document = new FlowDocument();
            Paragraph paragraph = new Paragraph();
            paragraph.Inlines.Add(new Run(""));
            document.Blocks.Add(paragraph);
            Output.Document = document;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            GridInfo.ItemsSource = null;
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = DataBase.connectionString;
            cn.Open();
            string strSelectPersons = "Select * From Customer";
            SqlCommand cmdSelectCustomers = new SqlCommand(strSelectPersons, cn);

            SqlDataReader customersDataReader = cmdSelectCustomers.ExecuteReader();
            customers.Clear();    // очистка списка 
            while (customersDataReader.Read())
            {
                string id = customersDataReader.GetString(0);
                string firstName = customersDataReader.GetString(1);
                string sureName = customersDataReader.GetString(2);
                string lastName = customersDataReader.GetString(3);
                string pos = customersDataReader.GetString(4);

                // Формирование очередного объекта типа Person и помещение его в коллекцию
                Customer cs = new Customer(id, firstName, sureName, lastName, pos);
                customers.Add(cs);
            }

            // Закрытие соединения
            cn.Close();
            GridInfo.ItemsSource = customers;
        }
    }
}
