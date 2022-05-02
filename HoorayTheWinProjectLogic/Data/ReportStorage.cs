using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Security.Principal;
using Excel = Microsoft.Office.Interop.Excel;
using ClosedXML.Excel;

namespace HoorayTheWinProjectLogic.Data
{
    /*[Serializable]
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

    }*/
    public class ReportStorageExcel
    {
        
        public List<Report> Reports { get; private set; }

        //private string filePath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\Report.json"


        public ReportStorageExcel()
        {
            Reports = new List<Report>();
        }

        public void CreateExcelTable()
        {
            Excel.Application xlApp = new Excel.Application();
            xlApp.SheetsInNewWorkbook = 2;
            Excel.Workbook workBook = xlApp.Workbooks.Add(Type.Missing);
            Excel.Worksheet sheet = (Excel.Worksheet)xlApp.Worksheets.get_Item(1);
            sheet.Name = "Говнокод";

            for (int i = 1; i <= 9; i++)
            {
                for (int j = 1; j < 9; j++)
                    sheet.Cells[i, j] = "nfkh";
            }

            workBook.Application.ActiveWorkbook.SaveAs("doc.xlsx", Type.Missing,
            Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange,
            Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);


        }





    }
}
