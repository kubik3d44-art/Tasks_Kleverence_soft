using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Task3;

namespace Task3
{
    internal class LogParser
    {
        private static readonly Regex Format1 = new Regex(@"^(?<date>\d{2}\.\d{2}\.\d{4})\s+" +
            @"(?<time>\d{2}:\d{2}:\d{2}\.\d+)\s+" +
            @"(?<level>[A-Z]+)\s+" +
            @"(?<message>.+)$", RegexOptions.Compiled);

        private static readonly Regex Format2 = new Regex(@"^(?<date>\d{4}-\d{2}-\d{2})\s+" +
            @"(?<time>\d{2}:\d{2}:\d{2}\.\d+)\|\s*" +
            @"(?<level>[A-Z]+)\|\d+\|" +
            @"(?<method>[^|]+)\|\s*" +
            @"(?<message>.+)$", RegexOptions.Compiled);
        //////////////////////////////////////////////////////////////////////////////
        public static bool TryParse(string line, out LogEntry entry)
        {
            entry = null;

            if (ParseFormat1(line, out entry))
                return true;

            if (ParseFormat2(line, out entry))
                return true;

            return false;
        }
        //////////////////////////////////////////////////////////////////////////////
        private static bool ParseFormat1(string line, out LogEntry entry)
        {
            entry = null;

            Match match = Format1.Match(line);

            if (!match.Success)
            { return false; }

            string date = match.Groups["date"].Value;
            string time = match.Groups["time"].Value;

            if (!DateTime.TryParseExact($"{date} {time}", "dd.MM.yyyy HH:mm:ss.FFF", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTime))
            { return false; }

            entry = new LogEntry
            {
                datetime = dateTime,
                time = time,
                level = NormalizeLevel(match.Groups["level"].Value),
                method = "DEFAULT",
                message = match.Groups["message"].Value
            };

            return true;
        }
        //////////////////////////////////////////////////////////////////////////////
        private static bool ParseFormat2(string line, out LogEntry entry)
        {
            entry = null;

            Match match = Format2.Match(line);

            if (!match.Success)
            { return false; }

            string date = match.Groups["date"].Value;
            string time = match.Groups["time"].Value;

            if (!DateTime.TryParseExact($"{date} {time}", "yyyy-MM-dd HH:mm:ss.FFFF", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTime))
            {  return false; }

            entry = new LogEntry
            {
                datetime = dateTime,
                time = time,
                level = NormalizeLevel(match.Groups["level"].Value),
                method = match.Groups["method"].Value.Trim(),
                message = match.Groups["message"].Value
            };

            return true;
        }
        //////////////////////////////////////////////////////////////////////////////
        private static string NormalizeLevel(string level)
        {
            switch (level)
            {
                case "INFORMATION":
                    return "INFO";

                case "WARNING":
                    return "WARN";

                case "INFO":
                    return "INFO";

                case "WARN":
                    return "WARN";

                case "ERROR":
                    return "ERROR";

                case "DEBUG":
                    return "DEBUG";

                default:
                    return level;
            }
        }
        //////////////////////////////////////////////////////////////////////////////
    }
}