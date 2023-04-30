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
    /// Логика взаимодействия для LidVersion.xaml
    /// </summary>
    public partial class LidVersion : Window
    {
        string FileName;
        public LidVersion()
        {
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

        private void Button_Click_1(object sender, RoutedEventArgs e)   //INSERT TO DB
        {
            //Производим внесение в статический список элемента
            try
            {
                string name = Convert.ToString(Naming.Text);

                Random randomizer = new Random();
                double randomNumber = randomizer.Next(100000, 999999);

                int registerNumberForPark = Convert.ToInt32(randomNumber);
                double mass = Convert.ToDouble(Mass.Text);
                double whidth = Convert.ToDouble(Width.Text);

                DateTime timeOfRegistrForPark = (DateTime)RegDatePicker.SelectedDate;
                DateTime stayTime = (DateTime)UpcomingDatePicker.SelectedDate;

                string picture = FileName;
                TextRange textRange = new TextRange(
                // TextPointer to the start of content in the RichTextBox.
                CommentPicker.Document.ContentStart,
                // TextPointer to the end of content in the RichTextBox.
                CommentPicker.Document.ContentEnd
                );
                string notes = textRange.Text;

                Transport tr = new Transport(registerNumberForPark, name, mass, whidth, timeOfRegistrForPark, stayTime, picture, notes);
                ListsDB.transports.Add(tr);

            }
            catch
            {
                MessageBox.Show("Некоторые поля были не заполнены при внесении в базу!", "Сообщение");
            }

            //Теперь начинаем внесение в базу данных

            SqlConnection cn = new SqlConnection();     // Объект-соединение
            cn.ConnectionString = DataBase.connectionString;
            // Открытие подключения
            cn.Open();
            Random trandomizer = new Random();
            double trandomNumber = trandomizer.Next(100000, 999999);

            int tid = Convert.ToInt32(trandomNumber);
            string tnaming = Naming.Text;
            double tmass = Convert.ToDouble(Mass.Text);
            double twhidth = Convert.ToDouble(Width.Text);

            DateTime ttimeOfRegistrForPark = (DateTime)RegDatePicker.SelectedDate;
            DateTime tstayTime = (DateTime)UpcomingDatePicker.SelectedDate;

            string convStrDateRegistrForPark = Convert.ToString(ttimeOfRegistrForPark);
            string convStrDateStayTime = Convert.ToString(tstayTime);

            string tpicture = FileName;

            TextRange ttextRange = new TextRange(
            // TextPointer to the start of content in the RichTextBox.
            CommentPicker.Document.ContentStart,
            // TextPointer to the end of content in the RichTextBox.
            CommentPicker.Document.ContentEnd
            );
            string tnotes = ttextRange.Text;

            // Создание SQL команды ввода
            string strInsertTransport = string.Format("INSERT INTO Transport VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')", tid, tnaming, 0, 0, tmass, twhidth, 0, convStrDateRegistrForPark, convStrDateStayTime, "0", tpicture, tnotes);
             
            // Создание объекта-команды
            SqlCommand cmdInsertTransport = new SqlCommand(strInsertTransport, cn);

            // Исполнение команды ввода
            cmdInsertTransport.ExecuteNonQuery();

            cn.Close();

            //---------------Функция автообновления-----------------------
            ListsDB.transports.Clear();        //данная опреация необходима на случай пустой таблицы в бд, т.к. если внести элемент без добавления его в бд, то некоторые данные могут быть случайно продублированы(речь идёт об одних и тех же элементах)

            GridInfo.ItemsSource = null;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = DataBase.connectionString;
            con.Open();
            string strSelectTransport = "Select * From Transport";
            SqlCommand cmdSelectTransport = new SqlCommand(strSelectTransport, con);

            SqlDataReader transportsDataReader = cmdSelectTransport.ExecuteReader();
            while (transportsDataReader.Read())
            {
                int id = transportsDataReader.GetInt32(0);
                string naming = transportsDataReader.GetString(1);
                double mass = transportsDataReader.GetDouble(4);
                double wight = transportsDataReader.GetDouble(5);
                DateTime timeOfRegistrForPark = Convert.ToDateTime(transportsDataReader.GetString(7));
                DateTime stayTime = Convert.ToDateTime(transportsDataReader.GetString(8));
                string picture = transportsDataReader.GetString(10);
                string notes = transportsDataReader.GetString(11);

                // Формирование очередного объекта и помещение его в коллекцию
                Transport tr = new Transport(id, naming, mass, wight, timeOfRegistrForPark, stayTime, picture, notes);
                ListsDB.transports.Add(tr);
            }
            // Закрытие соединения
            con.Close();
            GridInfo.ItemsSource = ListsDB.transports;

            //Очищаем вводимые поля
            Naming.Clear();
            Mass.Clear();
            Width.Clear();
            ImagePicker.Source = null;

            FlowDocument document = new FlowDocument();
            Paragraph paragraph = new Paragraph();          //нужно для очистки комментария
            paragraph.Inlines.Add(new Run(""));
            document.Blocks.Add(paragraph);

            CommentPicker.Document = document;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //Выборка изображения из папки
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "JPG files (*.jpg)|*.jpg";
            openFile.ShowDialog();
            FileName = openFile.FileName;

            ImageSource image = new BitmapImage(new Uri(FileName, UriKind.Absolute));
            ImagePicker.Source = image;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //Вывод инфы и парке
            FlowDocument document = new FlowDocument();
            Paragraph paragraph = new Paragraph();
            paragraph.Inlines.Add(new Run(Park.park.About()));
            document.Blocks.Add(paragraph);
            Output.Document = document;
            //ShowingImage.Source = (ImageSource)FileName;      //not work
            for (int i = 0; i < ListsDB.transports.Count; i++)
            {
                BitmapImage jpg = new BitmapImage();
                jpg.BeginInit();
                jpg.UriSource = new Uri(ListsDB.transports[i].Picture);
                jpg.EndInit();
                ShowingImage.Source = jpg;   //функция - показать изображение
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            //Вывести информацию об отдельно выбранном элементе
            try
            {
                FlowDocument document = new FlowDocument();
                Paragraph paragraph = new Paragraph();
                paragraph.Inlines.Add(new Run(ListsDB.transports[GridInfo.SelectedIndex].InfoString()));
                document.Blocks.Add(paragraph);
                Output.Document = document;
            }
            catch
            {
                MessageBox.Show("Вами не был выбран элемент для вывода информации");
            }
        }

        private void Output_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ClearOutput_Click(object sender, RoutedEventArgs e)
        {
            //Очистка вывода
            FlowDocument document = new FlowDocument();
            Paragraph paragraph = new Paragraph();
            paragraph.Inlines.Add(new Run(""));
            document.Blocks.Add(paragraph);
            Output.Document = document;
            ShowingImage.Source = null;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            //Обновление инфы о парке
            ListsDB.transports.Clear();        //данная опреация необходима на случай пустой таблицы в бд, т.к. если внести элемент без добавления его в бд, то некоторые данные могут быть случайно продублированы(речь идёт об одних и тех же элементах)

            GridInfo.ItemsSource = null;
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = DataBase.connectionString;
            cn.Open();
            string strSelectTransport = "Select * From Transport";
            SqlCommand cmdSelectTransport = new SqlCommand(strSelectTransport, cn);

            SqlDataReader transportsDataReader = cmdSelectTransport.ExecuteReader();
            //ListForTransport.transports.Clear();    // очистка списка 
            while (transportsDataReader.Read())
            {
                int id = transportsDataReader.GetInt32(0);
                string naming = transportsDataReader.GetString(1);
                double mass = transportsDataReader.GetDouble(4);
                double wight = transportsDataReader.GetDouble(5);
                DateTime timeOfRegistrForPark = Convert.ToDateTime(transportsDataReader.GetString(7));
                DateTime stayTime = Convert.ToDateTime(transportsDataReader.GetString(8));
                string picture = transportsDataReader.GetString(10);
                string notes = transportsDataReader.GetString(11);

                // Формирование очередного объекта и помещение его в коллекцию
                Transport tr = new Transport(id, naming, mass, wight, timeOfRegistrForPark, stayTime, picture, notes);
                ListsDB.transports.Add(tr);
            }

            // Закрытие соединения
            cn.Close();
            GridInfo.ItemsSource = ListsDB.transports;
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {

        }

        private void CommentPicker_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            if (ListsDB.transports.Count == 0)
            {
                FlowDocument document = new FlowDocument();
                Paragraph paragraph = new Paragraph();          //нужно для очистки комментария
                paragraph.Inlines.Add(new Run("В парке отсутствует транспорт\nПрибыль парка = 0"));
                document.Blocks.Add(paragraph);

                Output.Document = document;
            }
            else
            {
                FlowDocument document = new FlowDocument();
                Paragraph paragraph = new Paragraph();          //нужно для очистки комментария
                paragraph.Inlines.Add(new Run(ListsDB.transports[0].CalculateIncome()));
                document.Blocks.Add(paragraph);

                Output.Document = document;
            }
        }

        private void OutLine_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            //var cellInfo = GridInfo.SelectedCells[0];
            //string selectedID = "gg";
            //if (cellInfo.IsValid)
            //{
            //    selectedID = GridInfo.CurrentCell.Item.ToString();
            //    OutLine.Text = selectedID;
            //}

            var cellInfo = GridInfo.SelectedCells[0];

            var content = cellInfo.Column.GetCellContent(cellInfo.Item);
            OutLine.Text = Convert.ToString(content);
        }

        public void GridInfo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            Deleting Del = new Deleting();
            Del.ShowDialog();
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            if (ListsDB.transports.Count == 0)
            {
                MessageBox.Show("В парке отсутствует транспорт");
            }
            else
            {
                FlowDocument document = new FlowDocument();
                Paragraph paragraph = new Paragraph();          //нужно для очистки комментария
                paragraph.Inlines.Add(new Run(ListsDB.transports[GridInfo.SelectedIndex].CalculateOwn()));
                document.Blocks.Add(paragraph);

                Output.Document = document;
            }
        }

        private void Car_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            BusRegister BusReg = new BusRegister();
            BusReg.ShowDialog();
        }
    }
}
