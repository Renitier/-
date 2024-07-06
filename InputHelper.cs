using System;
using System.Collections.Generic;
using System.Text;

namespace TodoList_Namespace
{
    class InputHelper // Класс для ограничения ввода
    {
        public static bool ValidateInt(char c) //Проверка на ввод числа
        {
            return char.IsDigit(c);
        }

        public static int InputInt(string prompt) // Метод, позволяющий ввести только число
        {

            string input = "";
            char c;

            while (true)
            {
                c = Console.ReadKey(true).KeyChar;
                // Если пользователь нажал Enter и строка не пуста, то завершаем ввод
                if (c == '\r' || c == '\n')
                {
                    if (string.IsNullOrEmpty(input))
                    {
                        continue; //Перезапуск цикла, чтобы пользователь ввел число
                    }
                    break;
                }
                // Если нажат пробел, то игнорируем
                if (c == ' ')
                {
                    continue;
                }
                // Если нажат Backspace, то удаляем последний символ
                if (c == '\b')
                {
                    if (!string.IsNullOrEmpty(input))
                    {
                        input = input.Substring(0, input.Length - 1);
                        Console.Write("\b \b");
                    }
                }
                else if (!ValidateInt(c)) //Если введенный символ не цифра, то игнорируем
                {
                    continue;
                }
                else //Иначе выводим введенный символ и добавляем его в строку
                {
                    Console.Write(c);
                    input += c;
                }
            }
            // Если строка пуста, возвращаем 0
            if (string.IsNullOrEmpty(input))
            {
                return 0;
            }

            Console.WriteLine();
            return int.Parse(input);
        }
    }
}