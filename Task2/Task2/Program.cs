using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Reader(string name)
    {
        Console.WriteLine($"{name} хочет читать");
        int value = Server.GetCount();
        Console.WriteLine($"{name} прочитал значение {value}");
    }

    static void Writer(string name, int value)
    {
        Console.WriteLine($"{name} хочет добавить {value}");
        Server.AddToCount(value);
        Console.WriteLine($"{name} завершил запись");
    }

    static void Main()
    {
        Console.WriteLine("=== Начало тестирования ===");
        Console.WriteLine();

        Task[] tasks =
        {
            // Одновременно запускаем трех читателей
            Task.Run(() => Reader("Reader 1")),
            Task.Run(() => Reader("Reader 2")),
            Task.Run(() => Reader("Reader 3")),

            // Через полсекунды запускаем писателя
            Task.Run(() =>
            {
                Thread.Sleep(500);
                Writer("Writer 1", 10);
            }),

            // Пока писатель ждет - запускаем читателя
            Task.Run(() =>
            {
                Thread.Sleep(1000);
                Reader("Reader 4");
            }),

            // Второй писатель
            Task.Run(() =>
            {
                Thread.Sleep(1500);
                Writer("Writer 2", 5);
            }),

            // Еще один читатель
            Task.Run(() =>
            {
                Thread.Sleep(2000);
                Reader("Reader 5");
            })
        };

        Task.WaitAll(tasks);

        Console.WriteLine();
        Console.WriteLine("=== Все задачи завершены ===");
        Console.WriteLine($"Итоговое значение count = {Server.GetCount()}");

        Console.ReadKey();
    }
}
