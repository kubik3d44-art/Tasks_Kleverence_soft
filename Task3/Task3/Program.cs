using System;
using System.IO;
//using Task3;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFile = "input.txt";
            string outputFile = "output.txt";
            string problemsFile = "problems.txt";

            using (StreamWriter outputWriter = new StreamWriter(outputFile, false))
            using (StreamWriter problemsWriter = new StreamWriter(problemsFile, false))
            {
                foreach (string line in File.ReadLines(inputFile))
                {
                    if (string.IsNullOrWhiteSpace(line))
                    { continue; }

                    if (LogParser.TryParse(line, out LogEntry entry))
                    { outputWriter.WriteLine(LogFormatter.Format(entry)); }

                    else
                    { problemsWriter.WriteLine(line); }
                }
                outputWriter.Close();
                problemsWriter.Close();
            }

            Console.WriteLine("Обработка завершена.");
            Console.WriteLine($"Результат сохранен в: {outputFile}");
            Console.WriteLine($"Ошибочные записи сохранены в: {problemsFile}");
        }
    }
}