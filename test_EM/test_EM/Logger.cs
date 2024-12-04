using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_EM
{
    public class Logger
    {
        private readonly string logPath;
        public Logger(string logPath)
        {
            this.logPath = logPath;
        }
        public void Log(string message)
        {
            File.AppendAllText(logPath, $"{DateTime.Now}: {message}\n");
        }
    }
}
