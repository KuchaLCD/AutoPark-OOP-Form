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
using Microsoft.Win32;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для StyleUI.xaml
    /// </summary>
    public partial class StyleUI : Window
    {
        public static int deleteCount = 0;
        public StyleUI()
        {
            try
            {
                InitializeComponent();

                if (ListsDB.users.Count == 0)
                {
                    ChangeAvatarSTYLE.Visibility = Visibility.Hidden;
                }
                else
                {
                    ChangeAvatarSTYLE.Visibility = Visibility.Visible;
                }

                string IDPos = string.Empty;
                FullNameUserSTYLE.Content = ListsDB.users[0].ToString();
                string pos = ListsDB.users[0].IDPos;
                string avt = ListsDB.users[0].AvatarPicture;
                string defaultImage = @"C:\Users\CATAT\AutoPark-OOP-Form\WpfApp1\WpfApp1\BLANK.jpg";

                string sqlExpressionPos = "SELECT IDPos, PosName FROM Positions";

                using (SqlConnection connection = new SqlConnection(DataBase.connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sqlExpressionPos, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        IDPos = reader.GetString(0);
                        string posName = reader.GetString(1);

                        if (pos == IDPos)
                        {
                            PositionSTYLE.Content = posName;
                        }
                    }

                    reader.Close();
                }
                //Выгрузка изображения и ИН пользователя
                try
                {
                    ImageSource image = new BitmapImage(new Uri(avt, UriKind.Absolute));
                    StyleAvatar.Source = image;
                }
                catch
                {
                    ImageSource image = new BitmapImage(new Uri(defaultImage, UriKind.Absolute));
                    StyleAvatar.Source = image;
                }
            }
            catch
            {
                InitializeComponent();
            }
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string pos = ListsDB.users[0].IDPos;

            SqlConnection cn = new SqlConnection();     // Объект-соединение
            cn.ConnectionString = DataBase.connectionString;
            // Открытие подключения
            cn.Open();
            string userPicture = "";
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "JPG files (*.jpg)|*.jpg";
            openFile.ShowDialog();
            userPicture = openFile.FileName;

            try
            {
                ImageSource image = new BitmapImage(new Uri(userPicture, UriKind.Absolute));
                StyleAvatar.Source = image;
            }
            catch
            {
                MessageBox.Show("You dont choose a picture");
            }
            // Создание SQL команды ввода
            string strInsertUser = string.Format($"UPDATE Users SET AvatarPicture = '{userPicture}' WHERE IDPos = {pos}");

            // Создание объекта-команды
            SqlCommand cmdInsertUser = new SqlCommand(strInsertUser, cn);

            // Исполнение команды ввода
            cmdInsertUser.ExecuteNonQuery();

            cn.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            CarRegister CarReg = new CarRegister();
            CarReg.ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            BusRegister busReg = new BusRegister();
            busReg.ShowDialog();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            MotoRegister motoReg = new MotoRegister();
            motoReg.ShowDialog();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            MenuSTYLE.Visibility = Visibility.Visible;
            MenuBarSTYLE.Visibility = Visibility.Hidden;
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            MenuSTYLE.Visibility = Visibility.Hidden;
            MenuBarSTYLE.Visibility = Visibility.Visible;
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            ImageSTYLE.Source = null;
            try
            {
                if (ListsDB.transports.Count == 0)
                {
                    MessageBox.Show("В парке отсутствует транспорт");
                }
                else
                {
                    FlowDocument document = new FlowDocument();
                    Paragraph paragraph = new Paragraph();          //нужно для очистки комментария
                    paragraph.Inlines.Add(new Run(ListsDB.transports[GridSTYLE.SelectedIndex].CalculateOwn()));
                    document.Blocks.Add(paragraph);

                    OutputSTYLE.Document = document;
                }
            }
            catch
            {
                MessageBox.Show("Вами не был выбран элемент для расчёта стоимости стоянки");
            }
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            FlowDocument document = new FlowDocument();
            Paragraph paragraph = new Paragraph();
            paragraph.Inlines.Add(new Run(""));
            document.Blocks.Add(paragraph);
            OutputSTYLE.Document = document;
            ImageSTYLE.Source = null;
        }

        private void Button_Click_15(object sender, RoutedEventArgs e)
        {
            AdditToolSecCategory.Visibility = Visibility.Visible;
        }

        private void Button_Click_16(object sender, RoutedEventArgs e)
        {
            AdditToolSecCategory.Visibility = Visibility.Hidden;
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            ImageSTYLE.Source = null;
            if (ListsDB.transports.Count == 0)
            {
                FlowDocument document = new FlowDocument();
                Paragraph paragraph = new Paragraph();          //нужно для очистки комментария
                paragraph.Inlines.Add(new Run("В парке отсутствует транспорт\nПрибыль парка = 0"));
                document.Blocks.Add(paragraph);

                OutputSTYLE.Document = document;
            }
            else
            {
                try
                {
                    FlowDocument document = new FlowDocument();
                    Paragraph paragraph = new Paragraph();          //нужно для очистки комментария
                    paragraph.Inlines.Add(new Run(ListsDB.transports[0].CalculateIncome()));
                    document.Blocks.Add(paragraph);

                    OutputSTYLE.Document = document;
                }
                catch
                {
                    MessageBox.Show("Some content was wrong!");
                }
            }
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            ListsDB.transports.Clear();        //данная опреация необходима на случай пустой таблицы в бд, т.к. если внести элемент без добавления его в бд, то некоторые данные могут быть случайно продублированы(речь идёт об одних и тех же элементах)

            GridSTYLE.ItemsSource = null;
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = DataBase.connectionString;
            cn.Open();
            string strSelectTransport = "Select * From Transport";
            SqlCommand cmdSelectTransport = new SqlCommand(strSelectTransport, cn);

            SqlDataReader transportsDataReader = cmdSelectTransport.ExecuteReader();
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
                if (maxSpeed == 0 && volumeOfEngine == 0 && wheelCount == 0 && roadNumber == "0")
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
            GridSTYLE.ItemsSource = ListsDB.transports;
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            try
            {
                FlowDocument document = new FlowDocument();
                Paragraph paragraph = new Paragraph();
                paragraph.Inlines.Add(new Run(ListsDB.transports[GridSTYLE.SelectedIndex].InfoString()));
                document.Blocks.Add(paragraph);
                OutputSTYLE.Document = document;
                try
                {
                    for (int i = 0; i < ListsDB.transports.Count; i++)
                    {
                        BitmapImage jpg = new BitmapImage();
                        jpg.BeginInit();
                        jpg.UriSource = new Uri(ListsDB.transports[GridSTYLE.SelectedIndex].Picture);
                        jpg.EndInit();
                        ImageSTYLE.Source = jpg;   //функция - показать изображение
                    }
                }
                catch
                {
                    string defaultImage = @"C:\Users\CATAT\AutoPark-OOP-Form\WpfApp1\WpfApp1\BLANK.jpg";
                    BitmapImage jpg = new BitmapImage();
                    jpg.BeginInit();
                    jpg.UriSource = new Uri(defaultImage);
                    jpg.EndInit();
                    ImageSTYLE.Source = jpg;   //функция - показать изображение
                }
            }
            catch
            {
                MessageBox.Show("Вами не был выбран элемент для вывода информации");
            }
        }

        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            Deleting Del = new Deleting();
            Del.ShowDialog();
        }

        private void Button_Click_14(object sender, RoutedEventArgs e)
        {
            FlowDocument document = new FlowDocument();
            Paragraph paragraph = new Paragraph();
            paragraph.Inlines.Add(new Run(Park.park.About()));
            document.Blocks.Add(paragraph);
            OutputSTYLE.Document = document;
            //ShowingImage.Source = (ImageSource)FileName;      //not work
            try
            {
                for (int i = 0; i < ListsDB.transports.Count; i++)
                {
                    BitmapImage jpg = new BitmapImage();
                    jpg.BeginInit();
                    jpg.UriSource = new Uri(ListsDB.transports[i].Picture);
                    jpg.EndInit();
                    ImageSTYLE.Source = jpg;   //функция - показать изображение
                }
            }
            catch
            {
                string defaultImage = @"C:\Users\CATAT\AutoPark-OOP-Form\WpfApp1\WpfApp1\BLANK.jpg";
                BitmapImage jpg = new BitmapImage();
                jpg.BeginInit();
                jpg.UriSource = new Uri(defaultImage);
                jpg.EndInit();
                ImageSTYLE.Source = jpg;   //функция - показать изображение
            }
        }

        private void Button_Click_17(object sender, RoutedEventArgs e)
        {
            AddOrder addOrd = new AddOrder();
            addOrd.ShowDialog();
        }

        private void Button_Click_18(object sender, RoutedEventArgs e)
        {
            AddCustomer addCust = new AddCustomer();
            addCust.ShowDialog();
        }

        private void Button_Click_19(object sender, RoutedEventArgs e)
        {
            ListsDB.orders.Clear();
            SqlConnection cn = new SqlConnection();     // Объект-соединение
            cn.ConnectionString = DataBase.connectionString;
            // Открытие подключения
            cn.Open();

            // Формирование команды на языке SQL для выборки данных из таблицы
            string strSelectOrder = "Select * From Orders";

            SqlCommand cmdSelectOrder = new SqlCommand(strSelectOrder, cn);

            SqlDataReader orderDataReader = cmdSelectOrder.ExecuteReader();

            ListsDB.transports.Clear();    // очистка списка persons
            while (orderDataReader.Read())
            {
                int id = orderDataReader.GetInt32(0);
                string idCust = orderDataReader.GetString(1);
                DateTime startRent = Convert.ToDateTime(orderDataReader.GetString(2));
                DateTime endRent = Convert.ToDateTime(orderDataReader.GetString(3));
                int idTransp = orderDataReader.GetInt32(4);
                double bill = orderDataReader.GetDouble(5);

                // Формирование очередного объекта и помещение его в коллекцию
                Order ord = new Order(id, idCust, startRent, endRent, idTransp, bill);
                ListsDB.orders.Add(ord);
            }
            // Закрытие соединения
            cn.Close();
            GridSTYLE.ItemsSource = ListsDB.orders;
        }

        private void Button_Click_20(object sender, RoutedEventArgs e)
        {
            FlowDocument document = new FlowDocument();
            Paragraph paragraph = new Paragraph();
            for (int i = 0; i < ListsDB.orders.Count; i++)
            {
                paragraph.Inlines.Add(new Run(ListsDB.orders[i].InfoString()));
            }
            document.Blocks.Add(paragraph);
            OutputSTYLE.Document = document;
        }

        private void Button_Click_21(object sender, RoutedEventArgs e)
        {
            try
            {
                FlowDocument document = new FlowDocument();
                Paragraph paragraph = new Paragraph();
                paragraph.Inlines.Add(new Run(ListsDB.orders[GridSTYLE.SelectedIndex].InfoString()));
                document.Blocks.Add(paragraph);
                OutputSTYLE.Document = document;
            }
            catch
            {
                MessageBox.Show("Вами не был выбран элемент для вывода информации");
            }
        }

        private void Button_Click_22(object sender, RoutedEventArgs e)
        {
            deleteCount = 1;
            Deleting Del = new Deleting();
            Del.ShowDialog();
        }

        private void Button_Click_23(object sender, RoutedEventArgs e)
        {
            ListsDB.customers.Clear();
            SqlConnection cn = new SqlConnection();     // Объект-соединение
            cn.ConnectionString = DataBase.connectionString;
            // Открытие подключения
            cn.Open();

            // Формирование команды на языке SQL для выборки данных из таблицы
            string strSelectCust = "Select * From Customer";

            SqlCommand cmdSelectCust = new SqlCommand(strSelectCust, cn);

            SqlDataReader custDataReader = cmdSelectCust.ExecuteReader();

            ListsDB.transports.Clear();    // очистка списка persons
            while (custDataReader.Read())
            {
                string id = custDataReader.GetString(0);
                string fname = custDataReader.GetString(1);
                string sname = custDataReader.GetString(2);
                string lname = custDataReader.GetString(3);
                string pos = custDataReader.GetString(4);

                // Формирование очередного объекта и помещение его в коллекцию
                Customer cust = new Customer(id, fname, sname, lname, pos);
                ListsDB.customers.Add(cust);
            }
            // Закрытие соединения
            cn.Close();
            GridSTYLE.ItemsSource = ListsDB.customers;
        }

        private void Button_Click_24(object sender, RoutedEventArgs e)
        {
            try
            {
                FlowDocument document = new FlowDocument();
                Paragraph paragraph = new Paragraph();
                paragraph.Inlines.Add(new Run($"ИН Заказчика: {ListsDB.orders[GridSTYLE.SelectedIndex].IDCustomer}\nИтог к оплате для выбранного элемента = {ListsDB.orders[GridSTYLE.SelectedIndex].Bill}"));
                document.Blocks.Add(paragraph);
                OutputSTYLE.Document = document;
            }
            catch
            {
                MessageBox.Show("Вами не был выбран элемент для вывода информации");
            }
        }

        private void Button_Click_25(object sender, RoutedEventArgs e)
        {
            ImageSTYLE.Source = null;
            if (ListsDB.transports.Count == 0 && ListsDB.orders.Count == 0)
            {
                FlowDocument document = new FlowDocument();
                Paragraph paragraph = new Paragraph();          //нужно для очистки комментария
                paragraph.Inlines.Add(new Run("В парке отсутствует транспорт и заказы\nПрибыль парка = 0"));
                document.Blocks.Add(paragraph);

                OutputSTYLE.Document = document;
            }
            else
            {
                try
                {
                    FlowDocument document = new FlowDocument();
                    Paragraph paragraph = new Paragraph();          //нужно для очистки комментария
                    paragraph.Inlines.Add(new Run(ListsDB.transports[0].OurIncome()));
                    document.Blocks.Add(paragraph);

                    OutputSTYLE.Document = document;
                }
                catch
                {
                    MessageBox.Show("Some content was wrong! \nTry to update transport list");
                }
            }
        }
    }
}
