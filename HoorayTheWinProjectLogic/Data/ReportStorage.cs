using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Security.Principal;

namespace HoorayTheWinProjectLogic.Data
{
    [Serializable]
    public class ReportStorage
    {
        private string filePath= Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)+@"\Report.json";
        public List<Report> Reports { get; private set; }

        private static ReportStorage _instance;

        private ReportStorage()
        { 
            Reports = new List<Report>();
        }

        public static ReportStorage GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ReportStorage();
            }
            return _instance;
        }

        public string Serialize()
        {
            return JsonSerializer.Serialize<List<Report>>(Reports);
        }

        public List<Report> Decerialize(string json)
        {
            if(json== null)
            {
                throw new ArgumentException("json");
            }
            return JsonSerializer.Deserialize<List<Report>>(json);

        }

        public void Save()
        {
            string json = Serialize();

            using (StreamWriter sw = new StreamWriter(filePath, false))
            {
                sw.WriteLine(json);
            }
        }

        public List<Report> Load()
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                string json = sr.ReadLine();
                return Decerialize(json);
            }
        }

    }
}
