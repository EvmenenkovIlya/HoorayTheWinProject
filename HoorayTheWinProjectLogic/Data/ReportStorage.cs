using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoorayTheWinProjectLogic.DataStorage
{
    public class ReportStorage
    {
        public List<Report> Reports { get; private set; }

        private static ReportStorage _instance;

        private ReportStorage()
        { 
            Reports = new List<Report>();
        }

        public static ReportStorage GetInstance()
        {
            return _instance;
        }

        public static void SaveInstance()
        {

        }

    }
}
