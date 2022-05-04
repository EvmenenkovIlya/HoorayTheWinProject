using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoorayTheWinProjectLogic.Data
{
    public class TestToBot
    {
        public TestManager Manager { get; private set; }

        private static TestToBot _instance;

        private TestToBot(Test test)
        {
            Manager = new TestManager(test);
        }

        public static TestToBot GetInstance()
        {
            return _instance;
        }

        public static void CreateInstance(Test test)
        {
            _instance = new TestToBot(test);
        }

    }
}
