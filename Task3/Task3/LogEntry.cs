using System;
using System.Collections.Generic;
using System.Text;

namespace Task3
{
    internal class LogEntry
    {
        public DateTime datetime { get; set; }
        public string time { get; set; }
        public string level { get; set; }
        public string method { get; set; }
        public string message { get; set; }
    }
}
