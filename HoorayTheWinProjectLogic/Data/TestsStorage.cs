using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json;
using HoorayTheWinProjectLogic.Questions;

namespace HoorayTheWinProjectLogic.Data
{
    public class TestsStorage
    {
        public List<Test> Tests { get; private set; }

        private static TestsStorage _instance;


        private const string filePath = @"..\..\..\..\Tests.json";
        private TestsStorage()
        {           
            Tests = Load();
        }

        public static TestsStorage GetInstance()
        {
            if (_instance == null)
            {
                _instance = new TestsStorage();
            }                               
            return _instance;
        }

        public void Save()
        {
            string json = Serialize();

            using (StreamWriter sw = new StreamWriter(filePath, false))
            {
                sw.WriteLine(json);
            }
        }
        private string Serialize()
        {
            string json = "";
            foreach (Test test in Tests)
            {
                json += $"{test.NameTest}\r\n";
                foreach (AbstractQuestion qs in test.AbstractQuestions)
                {
                    json += $"{System.Text.Json.JsonSerializer.Serialize<AbstractQuestion>(qs)}\r\n";
                }
            }
            return json;
        }

        private List<Test> Load()
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                List<Test> list = new List<Test>();
                string[] js = File.ReadAllLines(filePath);
                js = js[0..(js.Length - 1)];
                foreach (string s in js)
                {                   
                    if (!s.Contains("TypeQuestion"))
                    {
                        list.Add(new Test(s));
                    }
                    else
                    {
                        list.Last().AbstractQuestions.Add(DeserializeQuestion(s));
                    }
                }               
                return list;
            }
        }

        private AbstractQuestion DeserializeQuestion(string json)
        {
            if (json == null)
            {
                throw new ArgumentNullException("json");
            }
            else
            {
                if (json.Contains("\"TypeQuestion\":0"))
                {
                    return System.Text.Json.JsonSerializer.Deserialize<ChooseNumber>(json)!;
                }
                else if (json.Contains("\"TypeQuestion\":1"))
                {
                    return System.Text.Json.JsonSerializer.Deserialize<ChooseOne>(json)!;
                }
                else if (json.Contains("\"TypeQuestion\":2"))
                {
                    return System.Text.Json.JsonSerializer.Deserialize<EnteringAResponse>(json)!;
                }
                else if (json.Contains("\"TypeQuestion\":3"))
                {
                    return System.Text.Json.JsonSerializer.Deserialize<InSeries>(json)!;
                }
                else
                {
                    return System.Text.Json.JsonSerializer.Deserialize<YesNo>(json)!;
                }
            }
        }
    }
}
