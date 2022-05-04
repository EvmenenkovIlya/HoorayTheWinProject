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
            var file = new FileInfo(@"..\..\..\..\DLIV.xlsx"); 
            
        }

        public  async Task<List<Report>> LoadExcelFile(FileInfo file, User user)
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
                p.Questions.Add(ws.Cells[row, col + 1].Value.ToString());
                p.UserAnswer.Add(ws.Cells[row, col + 2].Value.ToString());
                output.Add(p);
                row += 10;
            }
            return output;
        }

        public async Task SaveExcelFile(FileInfo file, List <Report> reports)
        {
            DeleteIfExists(file);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var package = new ExcelPackage(file);
            var ws = package.Workbook.Worksheets.Add("MainReport");

            ws.Cells["A1"].Value = "Name";
            ws.Cells["B1"].Value = "Question";
            ws.Cells["C1"].Value = "Answers";
            int x = 2;
            foreach (var report in reports)
            {
                int count = 0;

                for (int i = 0; i < report.Questions.Count; i++)
                {
                    ws.Cells[x + i, 1].Value = report.Name;
                    ws.Cells[x + i, 2].Value = report.Questions[i];
                    if(i>=report.UserAnswer.Count())
                    {
                        ws.Cells[x + i, 3].Value = "-";
                    }
                    else
                    {
                        ws.Cells[x + i, 3].Value = report.UserAnswer[i];
                    }
                    
                    count++;
                }
                x += count;
            }

            await package.SaveAsync();
        }

        public void DeleteIfExists(FileInfo file)
        {
            if (file.Exists)
            {
                file.Delete();
            }
        }

    }
}
