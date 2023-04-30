﻿using System;
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
        }

        private void BusModelRow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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

            ImageSource image = new BitmapImage(new Uri(MotoFileName, UriKind.Absolute));
            MotoImagePicker.Source = image;
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

            Random trandomizer = new Random();
            double trandomNumber = trandomizer.Next(100000, 999999);

            int tid = Convert.ToInt32(trandomNumber);
            string tnaming = MotoNaming.Text;
            double tmaxSpeed = Convert.ToDouble(MotoMaxSpeed.Text);
            double tvolumeOfEngine = Convert.ToDouble(MotoVolumeOfEngine.Text);
            double tmass = Convert.ToDouble(MotoMass.Text);
            double twhidth = Convert.ToDouble(MotoWidth.Text);
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
