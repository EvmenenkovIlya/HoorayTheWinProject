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
        public List<Group> groups { get; set; } = new List<Group>() { _other };
        private Dictionary<long, User> Base { get; set; } = new Dictionary<long, User>();
        private GroupStorage()
        {
            groups = Load();
            foreach (var group in groups)
            {
                foreach (var user in group.Users)
                {
                    Base.Add(user.ChatId, user);
                }
            }
        }
        public int ReturnUserTmp(long chatId)
        {
            return Base[chatId].tmp;
        }
        public void ChangeUserTmp(long chatId, int index)
        {
            Base[chatId].tmp = index;
        }
        public bool IsInBase(long chatId)
        {
            return Base.ContainsKey(chatId);
        }
        public Dictionary<long, User> ReturnCopyBase()
        {
            Dictionary<long, User> CopyBase = new Dictionary<long, User>();
            foreach (var person in Base)
            {
                CopyBase.Add(person.Key, person.Value);
            }
            return CopyBase;
        }
        public void AddChatId(long chatId, User user)
        {

            Base.Add(chatId, user);
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
