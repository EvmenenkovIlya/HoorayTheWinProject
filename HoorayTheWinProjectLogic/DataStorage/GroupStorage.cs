using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace HoorayTheWinProjectLogic.DataStorage
{
    public class GroupStorage
    {
        private const string filePath = @"C:\Users\vit20\filefile.Popa";

        private static GroupStorage _instance;

        public List<Group> groups = new List<Group>();

        public static List<long> DataBase { get; set; } = new List<long>();
        private GroupStorage()
        {
            groups = new List<Group>();
        }

        public static GroupStorage GetInstance()
        {
            if (_instance == null)
            {
                _instance = new GroupStorage();
            }
            return _instance;
        }


        public string Serialize(List<Group> groups)
        {
            return JsonSerializer.Serialize<List<Group>>(groups);
        }

        public List<Group> Deserialize(string json)
        {
            if (json == null)
            {
                throw new ArgumentNullException("json");
            }
            else
            {
                return JsonSerializer.Deserialize<List<Group>>(json);
            }
        }
        public void Save(List<Group> groups)
        {
            string json = Serialize(groups);

            using (StreamWriter sw = new StreamWriter(filePath, false))
            {
                sw.WriteLine(json);
            }
        }

        public List<Group> Load()
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                string json = sr.ReadLine();
                return Deserialize(json);
            }
        }

    }
}
