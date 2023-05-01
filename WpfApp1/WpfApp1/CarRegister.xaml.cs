using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
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
    /// Логика взаимодействия для CarRegister.xaml
    /// </summary>
    public partial class CarRegister : Window
    {
        string CarFileName;
        public CarRegister()
        {
            InitializeComponent();

            SqlConnection cn = new SqlConnection();     // Объект-соединение
            cn.ConnectionString = DataBase.connectionString;
            // Открытие подключения
            cn.Open();
            string strSelectTransport = "Select * From Models WHERE IDModel BETWEEN 2000 AND 2999";
            SqlCommand cmdSelectTransport = new SqlCommand(strSelectTransport, cn);
            SqlDataReader transportsDataReader = cmdSelectTransport.ExecuteReader();

            while (transportsDataReader.Read())
            {
                string model = transportsDataReader.GetString(1);
                CarModelRow.Items.Add(model);
            }
            // Закрытие соединения
            cn.Close();
        }

        private void BusModelRow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (CarModelRow.SelectedItem)
            {
                case "Toyota Landcruser Prada":
                    CarNaming.Text = "Toyota Landcruser Prada";
                    CarMaxSpeed.Text = "255";     //макс. скорость 
                    CarVolumeOfEngine.Text = "3,5";     //объем двигателя
                    CarMass.Text = "1700";     //масса
                    CarWidth.Text = "2,5";     //ширина
                    break;
                case "Mercedes S300":
                    CarNaming.Text = "Mercedes S300";
                    CarMaxSpeed.Text = "270";
                    CarVolumeOfEngine.Text = "2,5";
                    CarMass.Text = "1650";
                    CarWidth.Text = "2,4";
                    break;
                case "Renault Captur":
                    CarNaming.Text = "Renault Captur";
                    CarMaxSpeed.Text = "250";
                    CarVolumeOfEngine.Text = "1,9";
                    CarMass.Text = "1550";
                    CarWidth.Text = "2,1";
                    break;
                case "Kia Cerato":
                    CarNaming.Text = "Kia Cerato";
                    CarMaxSpeed.Text = "240";
                    CarVolumeOfEngine.Text = "2,1";
                    CarMass.Text = "1700";
                    CarWidth.Text = "2,3";
                    break;
                case "Honda Civic":
                    CarNaming.Text = "Honda Civic";
                    CarMaxSpeed.Text = "300";
                    CarVolumeOfEngine.Text = "2,8";
                    CarMass.Text = "1730";
                    CarWidth.Text = "2,4";
                    break;
                case "Ford Mustang":
                    CarNaming.Text = "Ford Mustang";
                    CarMaxSpeed.Text = "400";
                    CarVolumeOfEngine.Text = "2,6";
                    CarMass.Text = "1830";
                    CarWidth.Text = "1,9";
                    break;
                case "Volkswagen Passat B7":
                    CarNaming.Text = "Volkswagen Passat B7";
                    CarMaxSpeed.Text = "240";
                    CarVolumeOfEngine.Text = "1,8";
                    CarMass.Text = "1600";
                    CarWidth.Text = "1,9";
                    break;
                case "Audi TT":
                    CarNaming.Text = "Audi TT";
                    CarMaxSpeed.Text = "290";
                    CarVolumeOfEngine.Text = "1,8";
                    CarMass.Text = "1600";
                    CarWidth.Text = "1,9";
                    break;
                case "Audi A7":
                    CarNaming.Text = "Audi A7";
                    CarMaxSpeed.Text = "380";
                    CarVolumeOfEngine.Text = "2,1";
                    CarMass.Text = "1700";
                    CarWidth.Text = "2,3";
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
            CarFileName = openFile.FileName;

            ImageSource image = new BitmapImage(new Uri(CarFileName, UriKind.Absolute));
            CarImagePicker.Source = image;
        }

        private void CommentPicker_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Производим внесение в статический список элемента
            try
            {
                string name = Convert.ToString(CarNaming.Text);

                Random randomizer = new Random();
                double randomNumber = randomizer.Next(100000, 999999);

                int registerNumberForPark = Convert.ToInt32(randomNumber);
                double maxSpeed = Convert.ToDouble(CarMaxSpeed.Text);
                double volumeOfEngine = Convert.ToDouble(CarVolumeOfEngine.Text);
                double mass = Convert.ToDouble(CarMass.Text);
                double whidth = Convert.ToDouble(CarWidth.Text);
                string roadNumber = CarRoadNumber.Text;

                DateTime timeOfRegistrForPark = (DateTime)CarRegDatePicker.SelectedDate;
                DateTime stayTime = (DateTime)CarUpcomingDatePicker.SelectedDate;

                string picture = CarFileName;
                TextRange textRange = new TextRange(
                // TextPointer to the start of content in the RichTextBox.
                CarCommentPicker.Document.ContentStart,
                // TextPointer to the end of content in the RichTextBox.
                CarCommentPicker.Document.ContentEnd
                );
                string notes = textRange.Text;

                Car car = new Car(volumeOfEngine, maxSpeed, roadNumber, name, registerNumberForPark, mass, whidth, timeOfRegistrForPark, stayTime, picture, notes);
                ListsDB.transports.Add(car);

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
            string tnaming = CarNaming.Text;
            double tmaxSpeed = Convert.ToDouble(CarMaxSpeed.Text);
            double tvolumeOfEngine = Convert.ToDouble(CarVolumeOfEngine.Text);
            double tmass = Convert.ToDouble(CarMass.Text);
            double twhidth = Convert.ToDouble(CarWidth.Text);
            int twheelCount = Convert.ToInt32(4);
            string troadNumber = CarRoadNumber.Text;

            DateTime ttimeOfRegistrForPark = (DateTime)CarRegDatePicker.SelectedDate;
            DateTime tstayTime = (DateTime)CarUpcomingDatePicker.SelectedDate;

            string convStrDateRegistrForPark = Convert.ToString(ttimeOfRegistrForPark);
            string convStrDateStayTime = Convert.ToString(tstayTime);

            string tpicture = CarFileName;

            TextRange ttextRange = new TextRange(
            // TextPointer to the start of content in the RichTextBox.
            CarCommentPicker.Document.ContentStart,
            // TextPointer to the end of content in the RichTextBox.
            CarCommentPicker.Document.ContentEnd
            );
            string tnotes = ttextRange.Text;

            // Создание SQL команды ввода
            string strInsertTransport = string.Format("INSERT INTO Transport VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')", tid, tnaming, tmaxSpeed, tvolumeOfEngine, tmass, twhidth, twheelCount, convStrDateRegistrForPark, convStrDateStayTime, troadNumber, tpicture, tnotes);

            // Создание объекта-команды
            SqlCommand cmdInsertTransport = new SqlCommand(strInsertTransport, cn);

            // Исполнение команды ввода
            cmdInsertTransport.ExecuteNonQuery();

            cn.Close();

            CarNaming.Clear();
            CarMaxSpeed.Clear();
            CarVolumeOfEngine.Clear();
            CarMass.Clear();
            CarWidth.Clear();
            CarRoadNumber.Clear();
            CarImagePicker.Source = null;

            FlowDocument document = new FlowDocument();
            Paragraph paragraph = new Paragraph();          //нужно для очистки комментария
            paragraph.Inlines.Add(new Run(""));
            document.Blocks.Add(paragraph);

            CarCommentPicker.Document = document;
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
