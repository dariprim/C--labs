using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    private static List<Magazine> magazines = new List<Magazine>();
    private static Magazine? currentMagazine = null;

    static void Main(string[] args)
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.WriteLine("\n=== Меню управления журналами ===");
            Console.WriteLine("1. Создать новый журнал");
            Console.WriteLine("2. Загрузить журнал из файла");
            Console.WriteLine("3. Сохранить текущий журнал в файл");
            Console.WriteLine("4. Добавить статью в журнал");
            Console.WriteLine("5. Вывести данные текущего журнала");
            Console.WriteLine("6. Создать копию текущего журнала");
            Console.WriteLine("7. Выбрать журнал для работы");
            Console.WriteLine("8. Показать список всех журналов");
            Console.WriteLine("9. Выйти");
            Console.Write("Выберите действие: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    currentMagazine = CreateNewMagazine();
                    magazines.Add(currentMagazine);
                    break;

                case "2":
                    currentMagazine = LoadMagazineFromFile();
                    if (currentMagazine != null)
                    {
                        magazines.Add(currentMagazine);
                    }
                    break;

                case "3":
                    if (currentMagazine != null)
                    {
                        SaveMagazineToFile(currentMagazine);
                    }
                    else
                    {
                        Console.WriteLine("Ошибка: Нет текущего журнала.");
                    }
                    break;

                case "4":
                    if (magazines.Count == 0)
                    {
                        Console.WriteLine("Нет доступных журналов.");
                        break;
                    }

                    Console.WriteLine("\nВыберите журнал:");
                    for (int i = 0; i < magazines.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {magazines[i].EditionName}");
                    }

                    Console.Write("Введите номер журнала: ");
                    if (int.TryParse(Console.ReadLine(), out int magNum) && magNum > 0 && magNum <= magazines.Count)
                    {
                        magazines[magNum - 1].AddFromConsole();
                    }
                    else
                    {
                        Console.WriteLine("Неверный номер журнала.");
                    }
                    break;

                case "5":
                    if (currentMagazine != null)
                    {
                        Console.WriteLine("\n=== Данные текущего журнала ===");
                        Console.WriteLine(currentMagazine.ToString());
                    }
                    else
                    {
                        Console.WriteLine("Ошибка: Нет текущего журнала.");
                    }
                    break;

                case "6":
                    if (currentMagazine != null)
                    {
                        var copy = (Magazine)currentMagazine.DeepCopy();
                        magazines.Add(copy);
                        Console.WriteLine("\nКопия журнала создана!");
                    }
                    else
                    {
                        Console.WriteLine("Ошибка: Нет текущего журнала.");
                    }
                    break;

                case "7":
                    if (magazines.Count == 0)
                    {
                        Console.WriteLine("Нет доступных журналов.");
                        break;
                    }

                    Console.WriteLine("\nВыберите журнал:");
                    for (int i = 0; i < magazines.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {magazines[i].EditionName}");
                    }

                    Console.Write("Введите номер журнала: ");
                    if (int.TryParse(Console.ReadLine(), out int selectedMag) && selectedMag > 0 && selectedMag <= magazines.Count)
                    {
                        currentMagazine = magazines[selectedMag - 1];
                        Console.WriteLine($"Выбран журнал: {currentMagazine.EditionName}");
                    }
                    else
                    {
                        Console.WriteLine("Неверный номер журнала.");
                    }
                    break;

                case "8":
                    if (magazines.Count == 0)
                    {
                        Console.WriteLine("Нет доступных журналов.");
                        break;
                    }

                    Console.WriteLine("\n=== Список журналов ===");
                    for (int i = 0; i < magazines.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {magazines[i].EditionName}");
                    }
                    break;

                case "9":
                    isRunning = false;
                    Console.WriteLine("Программа завершена.");
                    break;

                default:
                    Console.WriteLine("Неверный ввод. Попробуйте снова.");
                    break;
            }
        }
    }

    private static Magazine CreateNewMagazine()
    {
        Console.WriteLine("\n=== Создание нового журнала ===");

        Console.Write("Введите название журнала: ");
        string name = Console.ReadLine() ?? "Новый журнал";

        Console.Write("Введите дату выпуска (гггг-мм-дд): ");
        DateTime.TryParse(Console.ReadLine(), out DateTime date);

        Console.Write("Введите тираж: ");
        int.TryParse(Console.ReadLine(), out int tirage);

        Console.Write("Введите частоту выпуска (Weekly, Monthly, Yearly): ");
        Enum.TryParse(Console.ReadLine(), out Frequency frequency);

        return new Magazine(name, date, tirage, frequency, new List<Person>(), new List<Article>());
    }

    private static Magazine? LoadMagazineFromFile()
    {
        Console.Write("\nВведите путь к файлу: ");
        string? filename = Console.ReadLine();

        if (string.IsNullOrEmpty(filename))
        {
            Console.WriteLine("Не указан файл.");
            return null;
        }

        if (!File.Exists(filename))
        {
            Console.WriteLine("Файл не существует.");
            return null;
        }

        if (Magazine.Load(filename, out Magazine? loadedMagazine) && loadedMagazine != null)
        {
            Console.WriteLine("Журнал успешно загружен!");
            return loadedMagazine;
        }

        Console.WriteLine("Не удалось загрузить журнал.");
        return null;
    }

    private static void SaveMagazineToFile(Magazine magazine)
    {
        Console.Write("\nВведите имя для сохранения: ");
        string? filename = Console.ReadLine();

        if (string.IsNullOrEmpty(filename))
        {
            Console.WriteLine("Не указан файл.");
            return;
        }

        if (magazine.Save(filename))
        {
            Console.WriteLine("Журнал успешно сохранен!");
        }
        else
        {
            Console.WriteLine("Не удалось сохранить журнал.");
        }
    }
}