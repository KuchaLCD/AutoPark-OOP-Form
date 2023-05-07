using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для UserInterface.xaml
    /// </summary>
    public partial class UserInterface : Window
    {
        public UserInterface()
        {
            InitializeComponent();
            string IDPos = string.Empty;
            UserCatcher.Content = ListsDB.users[0].ToString();      
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
                        PositionCatcher.Content = posName;
                    }
                }

                reader.Close();
            }
            //Выгрузка изображения и ИН пользователя
            try
            {
                ImageSource image = new BitmapImage(new Uri(avt, UriKind.Absolute));
                UserAvatar.Source = image;
            }
            catch
            {
                ImageSource image = new BitmapImage(new Uri(defaultImage, UriKind.Absolute));
                UserAvatar.Source = image;
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

        private void Button_Click_11(object sender, RoutedEventArgs e)      //Кнопка высчитать стоимость стоянки отдельному элементу
        {
            UserShowingImage.Source = null;
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
                    paragraph.Inlines.Add(new Run(ListsDB.transports[UserGridInfo.SelectedIndex].CalculateOwn()));
                    document.Blocks.Add(paragraph);

                    UserOutput.Document = document;
                }
            }
            catch
            {
                MessageBox.Show("Вами не был выбран элемент для расчёта стоимости стоянки");
            }
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)       //Кнопка расчёт прибыли
        {
            UserShowingImage.Source = null;
            if (ListsDB.transports.Count == 0)
            {
                FlowDocument document = new FlowDocument();
                Paragraph paragraph = new Paragraph();          //нужно для очистки комментария
                paragraph.Inlines.Add(new Run("В парке отсутствует транспорт\nПрибыль парка = 0"));
                document.Blocks.Add(paragraph);

                UserOutput.Document = document;
            }
            else
            {
                FlowDocument document = new FlowDocument();
                Paragraph paragraph = new Paragraph();          //нужно для очистки комментария
                paragraph.Inlines.Add(new Run(ListsDB.transports[0].CalculateIncome()));
                document.Blocks.Add(paragraph);

                UserOutput.Document = document;
            }
        }

        private void ClearOutput_Click(object sender, RoutedEventArgs e)        //Очистка вывода
        {
            FlowDocument document = new FlowDocument();
            Paragraph paragraph = new Paragraph();
            paragraph.Inlines.Add(new Run(""));
            document.Blocks.Add(paragraph);
            UserOutput.Document = document;
            UserShowingImage.Source = null;
        }

        private void Output_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void GridInfo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            MotoRegister MotoReg = new MotoRegister();
            MotoReg.ShowDialog();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)       //Вывести информацию об отдельно выбранном элементе
        {
            try
            {
                FlowDocument document = new FlowDocument();
                Paragraph paragraph = new Paragraph();
                paragraph.Inlines.Add(new Run(ListsDB.transports[UserGridInfo.SelectedIndex].InfoString()));
                document.Blocks.Add(paragraph);
                UserOutput.Document = document;

                BitmapImage jpg = new BitmapImage();
                jpg.BeginInit();
                jpg.UriSource = new Uri(ListsDB.transports[UserGridInfo.SelectedIndex].Picture);
                jpg.EndInit();
                UserShowingImage.Source = jpg;   //функция - показать изображение
            }
            catch
            {
                MessageBox.Show("Вами не был выбран элемент для вывода информации");
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)       //Обновление инфы о парке
        {
            ListsDB.transports.Clear();        //данная опреация необходима на случай пустой таблицы в бд, т.к. если внести элемент без добавления его в бд, то некоторые данные могут быть случайно продублированы(речь идёт об одних и тех же элементах)

            UserGridInfo.ItemsSource = null;
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
            UserGridInfo.ItemsSource = ListsDB.transports;
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            Deleting Del = new Deleting();
            Del.ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)       //Вывод инфы и парке
        {
            FlowDocument document = new FlowDocument();
            Paragraph paragraph = new Paragraph();
            paragraph.Inlines.Add(new Run(Park.park.About()));
            document.Blocks.Add(paragraph);
            UserOutput.Document = document;
            for (int i = 0; i < ListsDB.transports.Count; i++)
            {
                BitmapImage jpg = new BitmapImage();
                jpg.BeginInit();
                jpg.UriSource = new Uri(ListsDB.transports[i].Picture);
                jpg.EndInit();
                UserShowingImage.Source = jpg;   //функция - показать изображение
            }
        }

        private void UserCar_Click(object sender, RoutedEventArgs e)
        {
            CarRegister CarReg = new CarRegister();
            CarReg.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            BusRegister BusReg = new BusRegister();
            BusReg.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)   //Кнопка изменить аватарку
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
                UserAvatar.Source = image;
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
    }
}
