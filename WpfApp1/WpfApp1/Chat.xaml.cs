using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Navigation;

namespace WpfApp1
{
    //MAIN VERSION
    /// <summary>
    /// Логика взаимодействия для Chat.xaml
    /// </summary>
    public partial class Chat : Window
    {
        private bool done = true; // Флаг остановки слушающего потока
        private UdpClient client; // Сокет клиента
        private IPAddress groupAddress;  // Групповой адрес рассылки
        private int localPort; // Локальный порт для приема сообщений
        private int remotePort; // Удаленный порт для отправки сообщений
        private int ttl;

        private IPEndPoint remoteEP;
        private UnicodeEncoding encoding = new UnicodeEncoding();
        private string name;
        private string message;
        bool invokeBot = false;
        public Chat()
        {
            InitializeComponent();
            SendButton.IsEnabled = false;
            EndChatButton.IsEnabled = false;
            try
            {
                //Считываем конфигурационный файл приложения
                //NameValueCollection configuration = ConfigurationSettings.AppSettings;
                groupAddress = IPAddress.Parse("235.5.5.11");
                localPort = ListsDB.users[0].LocalPort;
                ttl = int.Parse("32");
            }
            catch
            {
                MessageBox.Show(this, "Ошибка в конфигурационном файле!", "Ошибка");
                StartChatButton.IsEnabled = false;
            }

            //Getting members of chat
            ListsDB.members.Clear();      //предварительное очищение списка пользователелей
            string sqlExpression = "SELECT * FROM Users";

            using (SqlConnection connection = new SqlConnection(DataBase.connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string login = reader.GetString(0);
                    string password = reader.GetString(1);
                    string firstName = reader.GetString(2);
                    string sureName = reader.GetString(3);
                    string idPos = reader.GetString(4);
                    string avatar = reader.GetString(5);
                    int port = reader.GetInt32(8);

                    UserDB newUser = new UserDB(login, password, firstName, sureName, idPos, avatar, port);
                    ListsDB.members.Add(newUser);
                }

                reader.Close();
            }
            //All is done. Page starting properties now
            FullNameUserSTYLE.Content = ListsDB.users[0].ToString();

            listviewMembers.ItemsSource = ListsDB.members;
            string avt = ListsDB.users[0].AvatarPicture;
            string defaultImage = @"C:\Users\CATAT\AutoPark-OOP-Form\WpfApp1\WpfApp1\BLANK.jpg";
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

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ChatBot bot = new ChatBot();
                string answer = MessageField.Text;
                //from bot
                if (MessageField.Text == "/start")
                {
                    invokeBot = true;
                    answer = bot.PrepareAnswer(MessageField.Text);
                    byte[] data = encoding.GetBytes(name + "(bot): " + answer);
                    client.Send(data, data.Length, remoteEP);
                    MessageField.Clear();
                    MessageField.Focus();
                }
                else if (MessageField.Text == "/stop" && invokeBot == true)
                {
                    invokeBot = false;
                    answer = bot.PrepareAnswer(MessageField.Text);
                    byte[] data = encoding.GetBytes(name + "(bot): " + answer);
                    client.Send(data, data.Length, remoteEP);
                    MessageField.Clear();
                    MessageField.Focus();
                }
                else if (invokeBot == true)
                {
                    answer = bot.PrepareAnswer(MessageField.Text);
                    byte[] data = encoding.GetBytes(name + "(bot): " + answer);
                    client.Send(data, data.Length, remoteEP);
                    MessageField.Clear();
                    MessageField.Focus();
                }
                //from human
                else
                {
                    byte[] data = encoding.GetBytes(name + ": " + answer);
                    client.Send(data, data.Length, remoteEP);
                    ChatView.ItemsSource = null;
                    MessageField.Clear();
                    MessageField.Focus();

                    //теперь это сообщение нужно добавить в БД
                    SqlConnection cn = new SqlConnection();     // Объект-соединение
                    cn.ConnectionString = DataBase.connectionString;
                    // Открытие подключения
                    cn.Open();

                    try
                    {
                        Random qrandomizer = new Random();
                        double qrandomNumber = qrandomizer.Next(8000, 8999);

                        int qidMes = Convert.ToInt32(qrandomNumber);
                        string avatarPicture = Convert.ToString(ListsDB.users[0].AvatarPicture);
                        string message = Convert.ToString(answer);

                        string timeChart = DateTime.Now.ToString("T");
                        string time = timeChart;

                        // Создание SQL команды ввода
                        string strInsertChat = string.Format("INSERT INTO Chart VALUES ('{0}','{1}','{2}','{3}')", qidMes, avatarPicture, message, time);

                        // Создание объекта-команды
                        SqlCommand cmdInsertChat = new SqlCommand(strInsertChat, cn);

                        // Исполнение команды ввода
                        cmdInsertChat.ExecuteNonQuery();

                        string userMessage = ListsDB.users[0].FirstName + "\n" + timeChart + " " + answer;
                        ChatGroupMes newChart = new ChatGroupMes(qidMes, avatarPicture, userMessage);
                        ListsDB.chart.Add(newChart);
                        ChatView.ItemsSource = ListsDB.chart;
                    }
                    catch
                    {
                        MessageBox.Show("Some content was wrong or not input");
                    }
                }
                //Отправляем сообщение группе
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Ошибка!");
            }
        }
        private void EndChatButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult Result = MessageBox.Show("Прекратить общение?", "Message", MessageBoxButton.YesNo);
            if (Result == MessageBoxResult.Yes)
            {
                //Отправляем группе сообщение о выходе
                byte[] data = encoding.GetBytes(ListsDB.users[0].FirstName + " покинул чат");
                client.Send(data, data.Length, remoteEP);
                //Покидаем группу
                client.DropMulticastGroup(groupAddress);
                client.Close();
                //Останавливаем поток, получающий сообщения
                done = true;

                ListsDB.chart.Clear();
                ChatView.ItemsSource = ListsDB.chart;
                MessageField.IsReadOnly = false;
                Hide();
                this.Close();
            }
        }
        private void StartChatButton_Click(object sender, RoutedEventArgs e)
        {
            ListsDB.chart.Clear();
            ChatView.ItemsSource = null;
            try
            {
                // Присоединяемся к группе рассылки
                client = new UdpClient(localPort);
                client.JoinMulticastGroup(groupAddress, ttl);

                remoteEP = new IPEndPoint(groupAddress, ListsDB.members[listviewMembers.SelectedIndex].LocalPort);  //только при условии, если мы выьрали того, с кем общаемся
                MemberName.Content = ListsDB.members[listviewMembers.SelectedIndex].FirstName;
                string avt = ListsDB.members[listviewMembers.SelectedIndex].AvatarPicture;
                string defaultImage = @"C:\Users\CATAT\AutoPark-OOP-Form\WpfApp1\WpfApp1\BLANK.jpg";
                //Выгрузка изображения и ИН пользователя
                try
                {
                    ImageSource image = new BitmapImage(new Uri(avt, UriKind.Absolute));
                    MemberAvatar.Source = image;
                }
                catch
                {
                    ImageSource image = new BitmapImage(new Uri(defaultImage, UriKind.Absolute));
                    MemberAvatar.Source = image;
                }
                // Запускаем поток, получающий сообщения
                Thread receiver = new Thread(new ThreadStart(Listener));
                receiver.IsBackground = true;
                receiver.Start();

                //Отправляем первое сообщение группе
                byte[] data = encoding.GetBytes("Пользователь " + ListsDB.users[0].FirstName + " присоединился к чату");
                client.Send(data, data.Length, remoteEP);
                StartChatButton.IsEnabled = false;
                EndChatButton.IsEnabled = true;
                SendButton.IsEnabled = true;

                //здесь представлена логика загрузки чата по началу соединения 
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = DataBase.connectionString;
                cn.Open();
                string strSelectChart = "Select * From Chart";
                SqlCommand cmdSelectChart = new SqlCommand(strSelectChart, cn);

                SqlDataReader chartDataReader = cmdSelectChart.ExecuteReader();
                while (chartDataReader.Read())
                {
                    int id = chartDataReader.GetInt32(0);
                    string avatar = chartDataReader.GetString(1);
                    string message = chartDataReader.GetString(2);
                    string time = chartDataReader.GetString(3);

                    string res = time + " " + message;
                    // Формирование очередного объекта и помещение его в коллекцию
                    ChatGroupMes chat = new ChatGroupMes(id, avatar, res);
                    ListsDB.chart.Add(chat);
                }

                // Закрытие соединения
                cn.Close();
                ChatView.ItemsSource = ListsDB.chart;
            }
            catch (SocketException ex)
            {
                MessageBox.Show(this, ex.Message, "Ошибка");
            }
        }
        private void Listener()
        {
            done = false;
            try
            {
                while (!done)
                {
                    IPEndPoint ep = null;
                    byte[] buffer = client.Receive(ref ep);
                    message = encoding.GetString(buffer);
                    this.Dispatcher.Invoke(new Action(DisplayReceivedMessage));
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(this, ex.Message, "Ошибка!");
            }
        }
        private void DisplayReceivedMessage()
        {
            ChatView.ItemsSource = null;
            string time = DateTime.Now.ToString("T");
            string userMessage = ListsDB.members[listviewMembers.SelectedIndex].FirstName + "\n" + " " +  time + " " + message;
            ChatGroupMes chart = new ChatGroupMes(localPort, ListsDB.members[listviewMembers.SelectedIndex].AvatarPicture, userMessage);
            ListsDB.chart.Add(chart);
            ChatView.ItemsSource = ListsDB.chart;
            //statusBar.Text = "Последнее сообщение " + time;
        }

        private void ChatView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void MessageDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult Result = MessageBox.Show("Удалить сообщение?", "Message", MessageBoxButton.YesNo);
            try
            {
                ChatGroupMes boofer = (ChatGroupMes)ChatView.SelectedItem;
                int selectedID = 0;
                try
                {
                    selectedID = boofer.idChat;
                }
                catch { MessageBox.Show("Не были загружены элементы для удаления!", "Сообщение"); }

                if (boofer == null)
                {
                    MessageBox.Show("В селективном списке отсутсвуют объекты для удаления, либо не выбран нужный элемент", "Сообщение");
                }
                else
                {
                    ListsDB.chart.Remove(boofer);
                    ChatView.ItemsSource = ListsDB.chart;
                }
                //Удаление из БД
                SqlConnection cn = new SqlConnection();     // Объект-соединение

                cn.ConnectionString = DataBase.connectionString;
                // Открытие подключения
                cn.Open();
                string strDeleteChart = string.Format($"DELETE FROM Chart WHERE (IDChat = '{selectedID}')");

                SqlCommand cmdDeleteChart = new SqlCommand(strDeleteChart, cn);

                // Исполнение команды 
                try
                {
                    cmdDeleteChart.ExecuteNonQuery();
                }
                catch
                {
                    MessageBox.Show("Что-то пошло не так");
                }
                cn.Close();
            }
            catch
            {
                MessageBox.Show("Benda daun");
            }
        }
    }
}
