using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;

class Task 
{
    public string Task_name { get; set; } //Наименование задачи
    public bool Completed { get; set; }  //Выполнена ли она или нет

    public Task(string task_name)
    { Task_name = task_name;
      Completed = false;   // по умолчанию задача не выполнена
    }

}

class TodoList
{
    private List<Task> tasks = new List<Task>();

    public void AddTask(string taskName) // Добавление новой задачи в список
    {
        Task newTask = new Task(taskName);
        tasks.Add(newTask);
    }

    public void ViewTasks() // Просмотр задач
    {
        int choice;
        while (true)
        {
            Console.WriteLine("Какие задачи вы хотите посмотреть?");
            Console.WriteLine("1. Все");
            Console.WriteLine("2. Только выполненные");
            Console.WriteLine("3. Только невыполненные");
            choice = InputHelper.InputInt("Введите номер фильтрации");
            if (choice == 1 || choice == 2 || choice == 3)
            {
                if (choice == 1) // Просмотр всех задач
                {
                    Console.WriteLine("\nВыполненные задачи отмечены знаком \"X\"");
                    for (int i = 0; i < tasks.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}.{tasks[i].Task_name} [{(tasks[i].Completed ? "X" : " ")}]");
                    }
                    break;
                }
                else if (choice == 2) // Просмотр всех выполненных задач
                {
                    Console.WriteLine("\nВыполненные задачи отмечены знаком \"X\"");
                    for (int i = 0; i < tasks.Count; i++)
                    {
                        if (tasks[i].Completed == true)
                        {
                            Console.WriteLine($"{i + 1}.{tasks[i].Task_name} [{(tasks[i].Completed ? "X" : " ")}]");
                        }
                        else continue;
                    }
                    break;
                }
                else // Просмотр всех невыполненных задач
                {
                    Console.WriteLine("\nВыполненные задачи отмечены знаком \"X\"");
                    for (int i = 0; i < tasks.Count; i++)
                    {
                        if (tasks[i].Completed == false)
                        {
                            Console.WriteLine($"{i + 1}.{tasks[i].Task_name} [{(tasks[i].Completed ? "X" : " ")}]");
                        }
                        else continue;
                    }
                    break;
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Неверный номер команды");
            }
        }
    }


    public void MarkTaskAsCompleted(int index) // Отметка задачи как выполненной
    {
        if (index >= 0 && index < tasks.Count)
        {
            tasks[index].Completed = true;
        }
        else
        {
            Console.WriteLine("Неверный номер задачи");
        }
    }

    public void DeleteTask(int index) // удаление задачи
    {
        if (index >=0 && index < tasks.Count)
        {
            Console.WriteLine($"Вы удалили задачу {index + 1}.{tasks[index].Task_name}");
            tasks.RemoveAt(index);
        }
        else
        {
            Console.WriteLine("Неверный номер задачи");
        }
    }

    public void SaveTodoListToFile(string fileName) //Сохранение списка в файл
    {
        List<string> lines = tasks.Select(task => $"{task.Task_name},{task.Completed}").ToList();
        File.WriteAllLines(fileName, lines);
    }

    public void LoadTodoListFromFile(string fileName) //Загрузка списка из файла
    {
        if (File.Exists(fileName))
        {
            string[] lines = File.ReadAllLines(fileName);
            tasks.Clear();
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                string task_name = parts[0];
                bool completed = bool.Parse(parts[1]);
                Task task = new Task(task_name) { Completed = completed };
                tasks.Add(task);
            }
        }
    }
}
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
internal class Program
{
    private static void Main()
    {
        TodoList todoList = new TodoList();

        bool exit = false;
        while (!exit)
        {   // Меню приложения
            Console.WriteLine("\nВыберите действие:");
            Console.WriteLine("1. Добавить задачу");
            Console.WriteLine("2. Посмотреть задачи");
            Console.WriteLine("3. Отметить задачу как выполненную");
            Console.WriteLine("4. Удалить задачу");
            Console.WriteLine("5. Сохранить задачи в файл");
            Console.WriteLine("6. Загрузить задачи из файла");
            Console.WriteLine("7. Выйти");

            int choice = InputHelper.InputInt("Введите номер пункта меню");

            switch (choice) // Обработка выбора пользователя
            {
                case 1:
                    Console.Clear();
                    string newTask;
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("Введите новую задачу");
                        Console.WriteLine("Но учтите, нельзя вводить пустую строку!");
                        Console.Write("Наименование задачи: ");
                        newTask = Console.ReadLine();
                    } while (string.IsNullOrEmpty(newTask));
                    todoList.AddTask(newTask);
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("Список задач:");
                    todoList.ViewTasks();
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("Какую задачу отметить как выполненную?");
                    int completedIndex = InputHelper.InputInt("Номер задачи") - 1;
                    todoList.MarkTaskAsCompleted(completedIndex);
                    break;
                case 4:
                    Console.Clear();
                    Console.WriteLine("Какую задачу удалить?");
                    int deleteIndex = InputHelper.InputInt("Номер задачи") - 1;
                    todoList.DeleteTask(deleteIndex);
                    break;
                case 5:
                    Console.Clear();
                    todoList.SaveTodoListToFile("TodoList.txt");
                    Console.WriteLine("Задачи сохранены в файле TodoList.txt");
                    break;
                case 6:
                    Console.Clear();
                    todoList.LoadTodoListFromFile("TodoList.txt");
                    Console.WriteLine("Задачи загружены из файла TodoList.txt");
                    break;
                case 7:
                    exit = true;
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Некорректный выбор, попробуйте еще раз");
                    break;
            }
        }
    }
}
