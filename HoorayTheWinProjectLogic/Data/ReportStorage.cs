using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Security.Principal;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

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
        public List<Report> Reports { get; set; }

        public  ReportStorageExcel()
        {
            Reports = new List<Report>();
        }
        static async Task CreateExcel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var file = new FileInfo(@"C:\Users\WWW\DASHA.xlsx"); 
            //await SaveExcelFile(file);
            //List<Report> reportList = await LoadExcelFile(file);
        }

        public  async Task<List<Report>> LoadExcelFile(FileInfo file)
        {
            List<Report> output = new();

            using var package = new ExcelPackage(file);

            await package.LoadAsync(file);

            var ws = package.Workbook.Worksheets[0];

            int row = 2;
            int col = 1;

            while (string.IsNullOrWhiteSpace(ws.Cells[row, col].Value?.ToString()) == false)
            {
                Report p = new Report();
                p.Name = ws.Cells[row, col].Value.ToString();
                p.Question = ws.Cells[row, col + 1].Value.ToString();
                p.UserAnswer.Add(ws.Cells[row, col + 2].Value.ToString());
                output.Add(p);
                row += 1;
            }
            return output;
        }

        public async Task SaveExcelFile(FileInfo file, List<Report> reports)
        {
            //DeleteIfExists(file);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var package = new ExcelPackage(file);
            var ws = package.Workbook.Worksheets.Add("MainReport");
            
            List<Report> output = new()
            {
                new() { Name = "1" },
                new() { Name = "2" },
                new() { Name = "3" }
            };

            ws.Cells["A1"].LoadFromCollection(output, true);

            //ws.Cells["Name"].Value = "Our Cool Report";
            await package.SaveAsync();
        }

        public void DeleteIfExists(FileInfo file)
        {
            if (file.Exists)
            {
                file.Delete();
            }
        }

        public List<Report> GetSetupData()
        {
            List<Report> output = new()
            {
                new() { Name="1" },
                new() { Name = "2" },
                new() { Name = "3" }
            };

            return output;
        }
    }
}
