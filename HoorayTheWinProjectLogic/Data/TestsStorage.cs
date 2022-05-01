using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
//using System.Reflection;
using Newtonsoft.Json;

namespace HoorayTheWinProjectLogic.Data
{
    public class TestsStorage
    {
        public List<Test> Tests { get; private set; }

        private static TestsStorage _instance;


        private const string filePath = @"..\..\..\..\Tests.json";
        private TestsStorage()
        {
            //Tests = Load();
            Tests = DataMock.tests;
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
            //var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto, Formatting = Formatting.Indented };
            //return JsonSerializer.Create(settings);
            return System.Text.Json.JsonSerializer.Serialize<List<Test>>(Tests);
        }
        //private List<Test> Deserialize(string json)
        //{
        //    if (json == null)
        //    {
        //        throw new ArgumentNullException("json");
        //    }
        //    else
        //    {
        //        return JsonSerializer.Deserialize<List<Test>>(json)!;
        //    }          
        //}

        //private List<Test> Load()
        //{
        //    using (StreamReader sr = new StreamReader(filePath))
        //    {
        //        string json = sr.ReadLine();
        //        return Deserialize(json);
        //    }
        //}


    }
}
