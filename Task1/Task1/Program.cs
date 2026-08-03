using System;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Task1
{
    class Program
    {

        ////////////////////////// Метод компрессии ////////////////////////////////////////////////
        public static System.String Compression(System.String strIn)
        {
            //валидация полученной строки (маленькие буквы латинского алфавита)
            bool notLatin = Regex.IsMatch(strIn, @"[^a-z]");
            if (notLatin == true) return $"Строка \"{strIn}\" задана не верно!";

            //объявление счётчика букв и строки результата
            System.Int32 countk = 0; System.String strOut = "";
            
            //модификация строки и её переборка
            strIn = strIn + " ";
            for (int i = 0; i < strIn.Length; i++)
            {
                if (i != 0) 
                {
                    if (strIn[i] != strIn[i - 1])
                    {
                        strOut = strOut + strIn[i - 1];
                        if (countk > 1)
                        {
                            strOut = strOut + Convert.ToString(countk);
                        }
                        countk = 0; //обнуление счётчика после получения 
                    }
                }
                countk++;
            }
            return strOut; //возврат сжатой строки
        }


        ////////////////////////// Метод декомпрессии ////////////////////////////////////////////////
        public static System.String DeCompression(System.String strIn)
        {
            //проверка на корректность полученной сжатой строки
            bool notLatin = Regex.IsMatch(strIn, @"[^a-z2-9]"); 
            if (notLatin == false) //проверка строки на наличие исключительно маленьких букв латинского алфавита и цифр
            {
                if (Regex.IsMatch(strIn, @"^[a-z]") == false) { return $"Строка \"{strIn}\" задана не верно!"; } //проверка на вхождение первого символа строки в качестве маленькой буквы латинского алфавита
                for (int i = 0; i < strIn.Length - 1; i++)
                {
                    if (strIn[i] == strIn[i + 1]) //проверка на два одинаковых символа, идущих друг за другом
                    {
                        return $"Строка \"{strIn}\" задана не верно!";
                    }
                }
            }
            else { return $"Строка \"{strIn}\" задана не верно!"; }

            //объявление счётчика букв, строки результата и символа проверки на цифру
            System.String strOut = ""; System.Int32 countk = 0; //System.String s;

            //переборка сжатой строки с возвращением к исходной форме
            for (int i = 0; i < strIn.Length; i++)
            {
                System.Char chCheck = strIn[i];
                System.String sCheck = chCheck.ToString();
                if ((Regex.IsMatch(sCheck, "^[2-9]")) == true) //проверка: является ли символ строки числом
                {
                    //добавление в строку количества букв, указанного в сжатом формате
                    int count = Convert.ToInt32(sCheck);
                    for (int j = 0; j < count-1; j++) //добавляем на одну букву меньше, т.к. в предыдущей итерации цикла, согласно следущей проверке на принадлежность к букве, один символ был записан в строку 
                    { strOut = strOut + strIn[i - 1]; }
                }
                else if ((Regex.IsMatch(sCheck, "^[a-z]")) == true) //проверка: является ли проверяемый символ строки маленькой буквой латинского алфавита
                { strOut = strOut + strIn[i]; }
            }
            return strOut;
        }

        static void Main()
        {
            bool run1 = true;
            while (run1)
            {
                Console.Clear();
                Console.WriteLine("Выберите метод демонстрации работы алгоритмов:\n1.Через подготовленные примеры\n2.Путём ручного ввода строки\n\nДля выхода нажмите кнопку \"Q\"");
                ConsoleKeyInfo keyInput = Console.ReadKey();
                System.Char key = keyInput.KeyChar;
                switch (key)
                {
                    case '1':
                        Console.Clear();
                        Console.WriteLine("Реализация алгоритма компрессии:" +
                            "\n aaabbcccdde -> {0}" +
                            "\n abcccdee -> {1}" +
                            "\n 1ertt44 -> {2}", Compression("aaabbcccdde"), Compression("abcccdee"), Compression("1ertt44") + "\n");
                        //Console.WriteLine(Compression(DeCompression("a2b3ce")));

                        Console.WriteLine("Реализация алгоритма декомпрессии:" +
                            "\n a2b3ce -> {0}" +
                            "\n cbrtd -> {1}" +
                            "\n 4rt6y2 -> {2}", DeCompression("a2b3ce"), DeCompression("cbrtd"), DeCompression("4rt6y2"));
                        //Console.WriteLine(DeCompression(Compression("aaabbcccdde")) + "\n");
                        Console.WriteLine("Для возврата нажмите любую клавишу");
                        keyInput = Console.ReadKey();
                        break;
                    case '2':
                        bool run2 = true;
                        while (run2)
                        {
                            Console.Clear();
                            Console.Write("Выберите алгоритм:\n 1 - Компрессии\n 2 - Декомпрессии\n 3 - Вернуться назад ");
                            keyInput = Console.ReadKey();
                            key = keyInput.KeyChar;
                            switch (key)
                            {
                                case '1':
                                    Console.Clear();
                                    Console.Write("Введите строку для проверки алгоритма компрессии: ");
                                    System.String strInCom = Console.ReadLine();
                                    Console.WriteLine(Compression(strInCom) + "\n");
                                    Console.WriteLine("Для возврата нажмите любую клавишу");
                                    keyInput = Console.ReadKey();
                                    break;
                                case '2':
                                    Console.Clear();
                                    Console.Write("Введите строку для проверки алгоритма декомпрессии: ");
                                    System.String strInDeCom = Console.ReadLine();
                                    Console.WriteLine(DeCompression(strInDeCom) + "\n");
                                    Console.WriteLine("Для возврата нажмите любую клавишу");
                                    keyInput = Console.ReadKey();
                                    break;
                                default:
                                    Console.Clear();
                                    break;
                            }
                            if (key == '3') { run2 = false; }
                        }
                        break;
                    case 'q':
                        Console.Clear();
                        run1 = false;
                        break;
                }
            }
        }
    }
}
