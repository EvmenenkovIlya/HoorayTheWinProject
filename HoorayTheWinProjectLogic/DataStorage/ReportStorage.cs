using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace HoorayTheWinProjectLogic.DataStorage
{
    [Serializable]
    public class ReportStorage
    {
        private const string filePath = @"hoorayProject.";
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

        public string Serialize(List<Report> reports)
        {
            return JsonSerializer.Serialize<List<Report>>(reports);
        }

        public List<Report> Decerialize(string json)
        {
            if(json== null)
            {
                throw new ArgumentException("json");
            }
            return JsonSerializer.Deserialize<List<Report>>(json);

        }

        public static void SaveInstance()
        {

        }

    }
}
