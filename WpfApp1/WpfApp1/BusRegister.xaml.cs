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
using System.IO;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для BusRegister.xaml
    /// </summary>
    public partial class BusRegister : Window
    {
        string BusFileName;
        public BusRegister()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Производим внесение в статический список элемента
            try
            {
                string name = Convert.ToString(BusNaming.Text);

                Random randomizer = new Random();
                double randomNumber = randomizer.Next(100000, 999999);

                int registerNumberForPark = Convert.ToInt32(randomNumber);
                double maxSpeed = Convert.ToDouble(BusMaxSpeed.Text);
                double volumeOfEngine = Convert.ToDouble(BusVolumeOfEngine.Text);
                double mass = Convert.ToDouble(BusMass.Text);
                double whidth = Convert.ToDouble(BusWidth.Text);
                int wheelCount = Convert.ToInt32(BusWheelCount.Text);
                string roadNumber = BusRoadNumber.Text;

                DateTime timeOfRegistrForPark = (DateTime)BusRegDatePicker.SelectedDate;
                DateTime stayTime = (DateTime)BusUpcomingDatePicker.SelectedDate;

                string picture = BusFileName;
                TextRange textRange = new TextRange(
                // TextPointer to the start of content in the RichTextBox.
                BusCommentPicker.Document.ContentStart,
                // TextPointer to the end of content in the RichTextBox.
                BusCommentPicker.Document.ContentEnd
                );
                string notes = textRange.Text;

                Bus bus = new Bus(volumeOfEngine, maxSpeed, roadNumber, name, registerNumberForPark, mass, whidth, timeOfRegistrForPark, stayTime, picture, notes, wheelCount);
                ListsDB.transports.Add(bus);

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
            string tnaming = BusNaming.Text;
            double tmaxSpeed = Convert.ToDouble(BusMaxSpeed.Text);
            double tvolumeOfEngine = Convert.ToDouble(BusVolumeOfEngine.Text);
            double tmass = Convert.ToDouble(BusMass.Text);
            double twhidth = Convert.ToDouble(BusWidth.Text);
            int twheelCount = Convert.ToInt32(BusWheelCount.Text);
            string troadNumber = BusRoadNumber.Text;

            DateTime ttimeOfRegistrForPark = (DateTime)BusRegDatePicker.SelectedDate;
            DateTime tstayTime = (DateTime)BusUpcomingDatePicker.SelectedDate;

            string convStrDateRegistrForPark = Convert.ToString(ttimeOfRegistrForPark);
            string convStrDateStayTime = Convert.ToString(tstayTime);

            string tpicture = BusFileName;

            TextRange ttextRange = new TextRange(
            // TextPointer to the start of content in the RichTextBox.
            BusCommentPicker.Document.ContentStart,
            // TextPointer to the end of content in the RichTextBox.
            BusCommentPicker.Document.ContentEnd
            );
            string tnotes = ttextRange.Text;

            // Создание SQL команды ввода
            string strInsertTransport = string.Format("INSERT INTO Transport VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')", tid, tnaming, tmaxSpeed, tvolumeOfEngine, tmass, twhidth, twheelCount, convStrDateRegistrForPark, convStrDateStayTime, troadNumber, tpicture, tnotes);

            // Создание объекта-команды
            SqlCommand cmdInsertTransport = new SqlCommand(strInsertTransport, cn);

            // Исполнение команды ввода
            cmdInsertTransport.ExecuteNonQuery();

            cn.Close();

            BusNaming.Clear();
            BusMaxSpeed.Clear();
            BusVolumeOfEngine.Clear();
            BusMass.Clear();
            BusWidth.Clear();
            BusWheelCount.Clear();
            BusRoadNumber.Clear();
            BusImagePicker.Source = null;

            FlowDocument document = new FlowDocument();
            Paragraph paragraph = new Paragraph();          //нужно для очистки комментария
            paragraph.Inlines.Add(new Run(""));
            document.Blocks.Add(paragraph);

            BusCommentPicker.Document = document;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //Выборка изображения из папки
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "JPG files (*.jpg)|*.jpg";
            openFile.ShowDialog();
            BusFileName = openFile.FileName;

            ImageSource image = new BitmapImage(new Uri(BusFileName, UriKind.Absolute));
            BusImagePicker.Source = image;
        }

        private void CommentPicker_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_3(object sender, TextChangedEventArgs e)
        {

        }

        private void BusModelRow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void GoOut_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult Result = MessageBox.Show("Прекратить регистрацию?", "Message", MessageBoxButton.YesNo);
            if (Result == MessageBoxResult.Yes)
            {
                Hide();
                this.Close();
            }
        }
    }
}
