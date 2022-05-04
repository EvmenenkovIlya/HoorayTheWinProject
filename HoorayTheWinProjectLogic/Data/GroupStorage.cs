using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Telegram.Bot.Types;

namespace HoorayTheWinProjectLogic.Data
{
    public class GroupStorage
    {
        private const string filePath = @"..\..\..\..\Groups.json";

        private static GroupStorage _instance;
        private static Group _other = new Group("Other");
        public List<Group> groups { get; set; } = new List<Group>() {_other};
        private List<long> DataBase { get; set; }  = new List<long>();

        private GroupStorage()
        {
            groups = Load();
            foreach (var group in groups)
            {
                foreach (var item in group.Users)
                {
                    DataBase.Add(item.ChatId);
                }
            }
        }
        public bool IsInBase(long chatId)
        {
            return DataBase.Contains(chatId);
        }
        public void Add(long chatId)
        {
            DataBase.Add(chatId);
        }
        public static GroupStorage GetInstance()
        {
            if (_instance == null)
            {
                _instance = new GroupStorage();
            }
            return _instance;
        }
        public string Serialize()
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
        public void Save()
        {
            string json = Serialize();

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
