# BykiAndKorovi
Игра "Быки и коровы"

Описание:
Это консольная игра, где игрок должен угадать 4-значное число. За каждую попытку программа сообщает количество "быков" и "коров", после чего игрок продолжает угадывать число до максимального количества попыток (5 попыток). Игра имеет систему авторизации и сохраняет статистику для каждого игрока.

![Screnshot](https://github.com/iskkk1/BykiAndKorovi/blob/main/Снимок%20экрана%202024-11-12%20192735.png)

Функции:
1. Авторизация пользователей (ввод логина и пароля).
2. Возможность создания нового аккаунта.
3. Генерация случайного 4-значного числа.
4. Игровой процесс с подсчётом "быков" и "коров".
5. Сохранение статистики игры в файл (победа или поражение, количество попыток).
6. Просмотр статистики игрока.

Как использовать:
1. Запустите программу.
2. Введите логин и пароль (если аккаунта нет — создайте новый).
3. Угадайте 4-значное число за 5 попыток.
4. После игры программа выведет результаты и сохранит их в файл с именем `{login}_stats.txt`.

'''

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
'''

Файлы:
- `users.txt` — файл с логинами и паролями.
- `{login}_stats.txt` — файл с результатами игр для каждого игрока.

Примечания:
- Файлы будут сохраняться в той же папке, где находится исполняемый файл программы.
- Для работы программы требуется .NET runtime.

Автор программы: Кристина Синявская
