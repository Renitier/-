using System;
using System.Collections.Generic;
using System.IO;
using TodoList_Namespace;

/// <summary>
/// Переменная для выбора типа фильтрации
/// </summary>
enum ViewTasks_choice
{
    ViewAllTasks = 1,
    ViewCompletedTasks = 2,
    ViewIncompleteTasks = 3
}

class Task 
{
    /// <summary>
    /// Наименование задачи
    /// </summary>
    public string Task_name { get; set; }
    /// <summary>
    /// Выполнена задача или нет
    /// </summary>
    public bool Completed { get; set; }

    public Task(string task_name)
    { Task_name = task_name;
      Completed = false;
    }

}

class TodoList
{    /// <summary>
     /// Список задач
     /// </summary>
    private List<Task> tasks = new List<Task>();
    /// <summary>
    /// Добавление новой задачи в список
    /// </summary>
    public void AddTask(string taskName)
    {
        Task newTask = new Task(taskName);
        tasks.Add(newTask);
    }
    /// <summary>
    /// Просмотр задач
    /// </summary>
    public void ViewTasks() // Просмотр задач
    {
        ViewTasks_choice choice;
        while (true)
        {
            Console.WriteLine("Какие задачи вы хотите посмотреть?");
            Console.WriteLine("1. Все");
            Console.WriteLine("2. Только выполненные");
            Console.WriteLine("3. Только невыполненные");
            if (Enum.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case ViewTasks_choice.ViewAllTasks:
                        Console.WriteLine("\nВыполненные задачи отмечены знаком \"X\"");
                        for (int i = 0; i < tasks.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}.{tasks[i].Task_name} [{(tasks[i].Completed ? "X" : " ")}]");
                        }
                        break;
                    case ViewTasks_choice.ViewCompletedTasks:
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
                    case ViewTasks_choice.ViewIncompleteTasks:
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
                    default:
                        Console.Clear();
                        Console.WriteLine("Неверный номер команды");
                        break;
                }
                break;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Неверный номер команды");
            }
        }
    }
    /// <summary>
    /// Отметка задач как выполненных
    /// </summary>
    public void MarkTaskAsCompleted(int index)
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
    /// <summary>
    /// Удаление задачи
    /// </summary>
    public void DeleteTask(int index)
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
    /// <summary>
    /// Сохранение списка в файл
    /// </summary>
    public void SaveTodoListToFile(string fileName)
    {
        List<string> lines = tasks.Select(task => $"{task.Task_name},{task.Completed}").ToList();
        File.WriteAllLines(fileName, lines);
    }
    /// <summary>
    /// Загрузка списка в файл
    /// </summary>
    public void LoadTodoListFromFile(string fileName)
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