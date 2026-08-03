using System;
using System.Collections.Generic;
using System.Text;

namespace Task3
{
    internal class LogFormatter
    {
        public static string Format(LogEntry entry)
        {
            if (entry == null) { throw new ArgumentNullException(nameof(entry)); }

            return $"{entry.datetime:yyyy-MM-dd}\t" + $"{entry.time}\t" + $"{entry.level}\t" + $"{entry.method}\t" + $"{entry.message}";
        }
    }
}
