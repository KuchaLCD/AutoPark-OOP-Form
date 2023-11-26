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
                localPort = int.Parse("8002");
                remotePort = int.Parse("8002");
                ttl = int.Parse("32");
            }
            catch
            {
                MessageBox.Show(this, "Ошибка в конфигурационном файле!", "Ошибка");
                StartChatButton.IsEnabled = false;
            }

            try
            {
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

                        UserDB newUser = new UserDB(login, password, firstName, sureName, idPos, avatar);
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
            catch
            {
                InitializeComponent();
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
                    MessageField.Clear();
                    MessageField.Focus();
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
            StopListener();

            MessageField.IsReadOnly = false;
        }

        private void StartChatButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Присоединяемся к группе рассылки
                client = new UdpClient(localPort);
                client.JoinMulticastGroup(groupAddress, ttl);

                remoteEP = new IPEndPoint(groupAddress, remotePort);
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

                ////теперь это сообщение нужно добавить в БД
                //SqlConnection cn = new SqlConnection();     // Объект-соединение
                //cn.ConnectionString = DataBase.connectionString;
                //// Открытие подключения
                //cn.Open();

                //try
                //{
                //    Random qrandomizer = new Random();
                //    double qrandomNumber = qrandomizer.Next(10000, 99999);

                //    int qidOrd = Convert.ToInt32(qrandomNumber);
                //    string qidCust = Convert.ToString(IDCustPicker.SelectedItem);
                //    DateTime qstartRent = (DateTime)startRentPicker.SelectedDate;
                //    DateTime qendRent = (DateTime)endRentPicker.SelectedDate;
                //    int qidTransp = Convert.ToInt32(ListsDB.transports[TransportPicker.SelectedIndex].RegisterNumberForPark);
                //    double qbill = ListsDB.orders[0].CalculateBill(qstartRent, qendRent);

                //    // Создание SQL команды ввода
                //    string strInsertOrder = string.Format("INSERT INTO Chart VALUES ('{0}','{1}','{2}','{3}')", qidOrd, qidCust, qstartRent, qendRent, qidTransp, qbill);

                //    // Создание объекта-команды
                //    SqlCommand cmdInsertOrder = new SqlCommand(strInsertOrder, cn);

                //    // Исполнение команды ввода
                //    cmdInsertOrder.ExecuteNonQuery();
                //}
                //catch
                //{
                //    MessageBox.Show("Some content was wrong or not input");
                //}

                //cn.Close();
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
                MessageBox.Show(this, ex.Message, "Ошибка!");
            }
        }
        private void DisplayReceivedMessage()
        {
            ChatView.ItemsSource = null;
            string time = DateTime.Now.ToString("t");
            string userMessage = time + " " + message + "\r\n" + MessageField.Text;
            ChatGroupMes chart = new ChatGroupMes(localPort, ListsDB.users[0].AvatarPicture, userMessage);
            ListsDB.chart.Add(chart);
            ChatView.ItemsSource = ListsDB.chart;
            //statusBar.Text = "Последнее сообщение " + time;
        }
        private void StopListener()
        {
            ChatView.ItemsSource = null;
            //Отправляем группе сообщение о выходе
            byte[] data = encoding.GetBytes($"{ListsDB.users[0].FirstName} покинул чат");
            client.Send(data, data.Length, remoteEP);
            ChatView.ItemsSource = ListsDB.chart;
            //Покидаем группу
            client.DropMulticastGroup(groupAddress);
            client.Close();
            //Останавливаем поток, получающий сообщения
            done = true;
            StartChatButton.IsEnabled = true;
            EndChatButton.IsEnabled = false;
            SendButton.IsEnabled = false;
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
                MessageBox.Show("Benda pidor");
            }
        }
    }
}
