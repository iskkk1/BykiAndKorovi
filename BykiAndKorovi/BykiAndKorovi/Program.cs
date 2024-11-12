using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Random rand = new Random();
        string login, password;
        int totalAttempts = 0, wins = 0, losses = 0;
        int maxAttempts = 5;
        bool contains;

        while (true)
        {
            // Запрос логина и пароля
            Console.Write("Введите логин (Если нет аккаунта, введите r): ");
            login = Console.ReadLine();

            if (login == "r")
            {
                Console.Write("Введите новый логин: ");
                login = Console.ReadLine();
                Console.Write("Введите новый пароль: ");
                password = Console.ReadLine();

                if (File.Exists("users.txt"))
                {
                    var lines = File.ReadAllLines("users.txt");
                    foreach (var line in lines)
                    {
                        if (line.Split(',')[0].Split(':')[1].Trim() == login)
                        {
                            Console.WriteLine("Такой логин уже существует!");
                            continue;
                        }
                    }
                }

                using (StreamWriter sw = File.AppendText("users.txt"))
                {
                    sw.WriteLine($"Логин: {login}, Пароль: {password}");
                }

                Console.WriteLine("Аккаунт создан!");
                continue;
            }

            Console.Write("Введите пароль: ");
            password = Console.ReadLine();

            bool userFound = false;
            string storedPassword = "";

            if (File.Exists("users.txt"))
            {
                var lines = File.ReadAllLines("users.txt");
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    if (parts[0].Split(':')[1].Trim() == login)
                    {
                        storedPassword = parts[1].Split(':')[1].Trim();
                        userFound = true;
                        break;
                    }
                }
            }

            if (!userFound || storedPassword != password)
            {
                Console.WriteLine("Неверный логин или пароль.");
                continue;
            }

            Console.WriteLine($"Добро пожаловать, {login}!");

            // Генерация случайного числа для игры
            int[] x = new int[4];
            string s;
            for (int i = 0; i < 4; i++)
            {
                do
                {
                    contains = false;
                    x[i] = rand.Next(1, 10);
                    for (int j = 0; j < i; j++)
                    {
                        if (x[j] == x[i])
                            contains = true;
                    }
                } while (contains);
            }

            s = x[0].ToString() + x[1].ToString() + x[2].ToString() + x[3].ToString();

            // Игровой процесс
            while (totalAttempts < maxAttempts)
            {
                Console.WriteLine($"Попытка #{totalAttempts + 1}. Введите 4-значное число: ");
                string input = Console.ReadLine();

                if (input.Length != 4)
                {
                    Console.WriteLine("Введенное число должно быть 4-значным.");
                }
                else
                {
                    totalAttempts++;

                    int byki = 0, korovi = 0;

                    for (int i = 0; i < 4; i++)
                    {
                        if (s.Contains(input[i]))
                        {
                            if (s[i] == input[i])
                                byki++;
                            else
                                korovi++;
                        }
                    }

                    Console.WriteLine($"({x[0]}{x[1]}{x[2]}{x[3]}) БЫКИ: {byki}, КОРОВЫ: {korovi}");

                    if (byki == 4)
                    {
                        Console.WriteLine("Вы выиграли!");
                        wins++;
                        break;
                    }
                }
            }

            if (totalAttempts == maxAttempts)
            {
                Console.WriteLine($"Вы проиграли. Необходимо было угадать число за {maxAttempts} попыток.");
                losses++;
            }

            string statsFilePath = $"{login}_stats.txt";
            string result = wins > losses ? "Победа" : "Поражение";
            string statsLine = $"Игрок: {login}, {result}, Количество попыток: {totalAttempts} ";

            using (StreamWriter sw = new StreamWriter(statsFilePath, true))
            {
                sw.WriteLine(statsLine);
            }

            Console.WriteLine("\n--- Статистика игрока ---");
            DisplayPlayerStats(login);

            totalAttempts = 0;
            wins = 0;
            losses = 0;
        }
    }

    static void DisplayPlayerStats(string login)
    {
        string statsFilePath = $"{login}_stats.txt";
        if (File.Exists(statsFilePath))
        {
            var lines = File.ReadAllLines(statsFilePath);
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
        }
        else
        {
            Console.WriteLine("Файл статистики не найден.");
        }
    }
}
