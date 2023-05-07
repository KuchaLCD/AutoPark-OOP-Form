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
using System.Data.SqlClient;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MotoRegister.xaml
    /// </summary>
    public partial class MotoRegister : Window
    {
        string MotoFileName;
        public MotoRegister()
        {
            InitializeComponent();

            SqlConnection cn = new SqlConnection();     // Объект-соединение
            cn.ConnectionString = DataBase.connectionString;
            // Открытие подключения
            cn.Open();
            string strSelectTransport = "Select * From Models WHERE IDModel BETWEEN 4000 AND 4999";
            SqlCommand cmdSelectTransport = new SqlCommand(strSelectTransport, cn);
            SqlDataReader transportsDataReader = cmdSelectTransport.ExecuteReader();

            while (transportsDataReader.Read())
            {
                string model = transportsDataReader.GetString(1);
                MotoModelRow.Items.Add(model);
            }
            // Закрытие соединения
            cn.Close();
        }

        private void BusModelRow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (MotoModelRow.SelectedItem)
            {
                case "Ducati-1098":
                    MotoNaming.Text = "Ducati-1098";
                    MotoMaxSpeed.Text = "200";     //макс. скорость 
                    MotoVolumeOfEngine.Text = "1,5";     //объем двигателя
                    MotoMass.Text = "173";     //масса
                    MotoWidth.Text = "1,5";     //ширина
                    break;
                case "Honda Blackbird CBR1100XX":
                    MotoNaming.Text = "Honda Blackbird CBR1100XX";
                    MotoMaxSpeed.Text = "290";
                    MotoVolumeOfEngine.Text = "1,2";
                    MotoMass.Text = "225";
                    MotoWidth.Text = "1,4";
                    break;
                case "BMW S1000 RR":
                    MotoNaming.Text = "BMW S1000 RR";
                    MotoMaxSpeed.Text = "300";
                    MotoVolumeOfEngine.Text = "1,9";
                    MotoMass.Text = "270";
                    MotoWidth.Text = "1,9";
                    break;
                case "Yamaha YZF-R1":
                    MotoNaming.Text = "Yamaha YZF-R1";
                    MotoMaxSpeed.Text = "340";
                    MotoVolumeOfEngine.Text = "2,1";
                    MotoMass.Text = "269";
                    MotoWidth.Text = "1,7";
                    break;
                case "Ninja ZX-14":
                    MotoNaming.Text = "Ninja ZX-14";
                    MotoMaxSpeed.Text = "345";
                    MotoVolumeOfEngine.Text = "2,1";
                    MotoMass.Text = "240";
                    MotoWidth.Text = "1,5";
                    break;
                case "MV Agusta F4 CC":
                    MotoNaming.Text = "MV Agusta F4 CC";
                    MotoMaxSpeed.Text = "306";
                    MotoVolumeOfEngine.Text = "2,2";
                    MotoMass.Text = "245";
                    MotoWidth.Text = "1,3";
                    break;
                case "Suzuki Hayabusa":
                    MotoNaming.Text = "Suzuki Hayabusa";
                    MotoMaxSpeed.Text = "330";
                    MotoVolumeOfEngine.Text = "2,3";
                    MotoMass.Text = "320";
                    MotoWidth.Text = "1,2";
                    break;
                case "МТТ Turbine Superbike":
                    MotoNaming.Text = "МТТ Turbine Superbike";
                    MotoMaxSpeed.Text = "365";
                    MotoVolumeOfEngine.Text = "2,3";
                    MotoMass.Text = "225";
                    MotoWidth.Text = "1,9";
                    break;
                case "МТТ Street Fighter":
                    MotoNaming.Text = "МТТ Street Fighte";
                    MotoMaxSpeed.Text = "402";
                    MotoVolumeOfEngine.Text = "2,5";
                    MotoMass.Text = "380";
                    MotoWidth.Text = "1,7";
                    break;
                case "Dodge Tomahawk":
                    MotoNaming.Text = "Dodge Tomahawk";
                    MotoMaxSpeed.Text = "480";
                    MotoVolumeOfEngine.Text = "2,3";
                    MotoMass.Text = "680";
                    MotoWidth.Text = "2,3";
                    break;
            }
        }
        private void TextBox_TextChanged_3(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //Выборка изображения из папки
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "JPG files (*.jpg)|*.jpg";
            openFile.ShowDialog();
            MotoFileName = openFile.FileName;

            try
            {
                ImageSource image = new BitmapImage(new Uri(MotoFileName, UriKind.Absolute));
                MotoImagePicker.Source = image;
            }
            catch
            {
                MessageBox.Show("You dont choose a picture");
            }
        }

        private void CommentPicker_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Производим внесение в статический список элемента
            try
            {
                string name = Convert.ToString(MotoNaming.Text);

                Random randomizer = new Random();
                double randomNumber = randomizer.Next(100000, 999999);

                int registerNumberForPark = Convert.ToInt32(randomNumber);
                double maxSpeed = Convert.ToDouble(MotoMaxSpeed.Text);
                double volumeOfEngine = Convert.ToDouble(MotoVolumeOfEngine.Text);
                double mass = Convert.ToDouble(MotoMass.Text);
                double whidth = Convert.ToDouble(MotoWidth.Text);
                string roadNumber = MotoRoadNumber.Text;

                DateTime timeOfRegistrForPark = (DateTime)MotoRegDatePicker.SelectedDate;
                DateTime stayTime = (DateTime)MotoUpcomingDatePicker.SelectedDate;

                string picture = MotoFileName;
                TextRange textRange = new TextRange(
                // TextPointer to the start of content in the RichTextBox.
                MotoCommentPicker.Document.ContentStart,
                // TextPointer to the end of content in the RichTextBox.
                MotoCommentPicker.Document.ContentEnd
                );
                string notes = textRange.Text;

                Motocicle moto = new Motocicle(volumeOfEngine, maxSpeed, roadNumber, name, registerNumberForPark, mass, whidth, timeOfRegistrForPark, stayTime, picture, notes);
                ListsDB.transports.Add(moto);

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

            try
            {
                Random trandomizer = new Random();
                double trandomNumber = trandomizer.Next(100000, 999999);

                int tid = Convert.ToInt32(trandomNumber);
                string tnaming = MotoNaming.Text;
                string tmaxSpeed = MotoMaxSpeed.Text.Replace(",", ".");
                string tvolumeOfEngine = MotoVolumeOfEngine.Text.Replace(",", ".");
                string tmass = MotoMass.Text.Replace(",", ".");
                string twhidth = MotoWidth.Text.Replace(",", ".");
                int twheelCount = Convert.ToInt32(2);
                string troadNumber = MotoRoadNumber.Text;

                DateTime ttimeOfRegistrForPark = (DateTime)MotoRegDatePicker.SelectedDate;
                DateTime tstayTime = (DateTime)MotoUpcomingDatePicker.SelectedDate;

                string convStrDateRegistrForPark = Convert.ToString(ttimeOfRegistrForPark);
                string convStrDateStayTime = Convert.ToString(tstayTime);

                string tpicture = MotoFileName;

                TextRange ttextRange = new TextRange(
                // TextPointer to the start of content in the RichTextBox.
                MotoCommentPicker.Document.ContentStart,
                // TextPointer to the end of content in the RichTextBox.
                MotoCommentPicker.Document.ContentEnd
                );
                string tnotes = ttextRange.Text;

                // Создание SQL команды ввода
                string strInsertTransport = string.Format("INSERT INTO Transport VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')", tid, tnaming, tmaxSpeed, tvolumeOfEngine, tmass, twhidth, twheelCount, convStrDateRegistrForPark, convStrDateStayTime, troadNumber, tpicture, tnotes);

                // Создание объекта-команды
                SqlCommand cmdInsertTransport = new SqlCommand(strInsertTransport, cn);

                // Исполнение команды ввода
                cmdInsertTransport.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Some content was wrong or not input");
            }

            cn.Close();

            MotoNaming.Clear();
            MotoMaxSpeed.Clear();
            MotoVolumeOfEngine.Clear();
            MotoMass.Clear();
            MotoWidth.Clear();
            MotoRoadNumber.Clear();
            MotoImagePicker.Source = null;

            FlowDocument document = new FlowDocument();
            Paragraph paragraph = new Paragraph();          //нужно для очистки комментария
            paragraph.Inlines.Add(new Run(""));
            document.Blocks.Add(paragraph);

            MotoCommentPicker.Document = document;
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
