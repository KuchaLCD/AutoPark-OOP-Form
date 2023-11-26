using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WpfApp1
{
    class ChatBot
    {
        private string answer;
        public List<string> botCommands = new List<string> { "/start", "/stop", "погода", "скажи", "привет" };
        public string getAnswer() { return this.answer; }
        public string FindCommand(string s)
        {
            try
            {
                string[] parts = s.Split();
                string command = parts[0];
                return command;
            }
            catch (FormatException)
            {
                return "";
            }
        }
        public string PrepareAnswer(string message)
        {
            string command = FindCommand(message);
            string result = "";
            int index = botCommands.FindIndex(x => x == command);
            switch (index)
            {
                case 0:
                    result = "Hello! I'm bot of this chat. I can cunsult you for some qwestions)";
                    this.answer = result;
                    break;
                case 1:
                    result = "Was glad to help you!)";
                    this.answer = result;
                    break;
                case 2:
                    //-------------------------
                    string id_city = "26063"; // Санкт-Петербург

                    var t = XDocument.Load(string.Format("http://export.yandex.ru/weather-ng/forecasts/{0}.xml", id_city));

                    XNamespace ya = "http://weather.yandex.ru/forecast";
                    var fact = t.Document.Root.Element(ya + "fact");
                    result = fact.Element(ya + "station").Value + fact.Element(ya + "temperature").Value + " градусов" + fact.Element(ya + "weather_type").Value;
                    this.answer = result;
                    break;
                case 3:
                    String[] parts = message.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    string res = "";
                    for (int i = 0; i < parts.Length - 1; i++)
                    {
                        res += " " + parts[i + 1];
                        message = res;
                    }
                    result = message;
                    this.answer = result;
                    break;
                case 4:
                    result = "Здарова, шершень)))";
                    this.answer = result;
                    break;
                default:
                    result = "This command I don't know(((";
                    this.answer = result;
                    break;
            }
            return this.answer;
        }
    }
}
