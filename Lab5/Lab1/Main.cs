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
            Console.WriteLine("\n=== ���� ���������� ��������� ===");
            Console.WriteLine("1. ������� ����� ������");
            Console.WriteLine("2. ��������� ������ �� �����");
            Console.WriteLine("3. ��������� ������� ������ � ����");
            Console.WriteLine("4. �������� ������ � ������");
            Console.WriteLine("5. ������� ������ �������� �������");
            Console.WriteLine("6. ������� ����� �������� �������");
            Console.WriteLine("7. ������� ������ ��� ������");
            Console.WriteLine("8. �������� ������ ���� ��������");
            Console.WriteLine("9. �����");
            Console.Write("�������� ��������: ");

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
                        Console.WriteLine("������: ��� �������� �������.");
                    }
                    break;

                case "4":
                    if (magazines.Count == 0)
                    {
                        Console.WriteLine("��� ��������� ��������.");
                        break;
                    }

                    Console.WriteLine("\n�������� ������:");
                    for (int i = 0; i < magazines.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {magazines[i].EditionName}");
                    }

                    Console.Write("������� ����� �������: ");
                    if (int.TryParse(Console.ReadLine(), out int magNum) && magNum > 0 && magNum <= magazines.Count)
                    {
                        magazines[magNum - 1].AddFromConsole();
                    }
                    else
                    {
                        Console.WriteLine("�������� ����� �������.");
                    }
                    break;

                case "5":
                    if (currentMagazine != null)
                    {
                        Console.WriteLine("\n=== ������ �������� ������� ===");
                        Console.WriteLine(currentMagazine.ToString());
                    }
                    else
                    {
                        Console.WriteLine("������: ��� �������� �������.");
                    }
                    break;

                case "6":
                    if (currentMagazine != null)
                    {
                        var copy = (Magazine)currentMagazine.DeepCopy();
                        magazines.Add(copy);
                        Console.WriteLine("\n����� ������� �������!");
                    }
                    else
                    {
                        Console.WriteLine("������: ��� �������� �������.");
                    }
                    break;

                case "7":
                    if (magazines.Count == 0)
                    {
                        Console.WriteLine("��� ��������� ��������.");
                        break;
                    }

                    Console.WriteLine("\n�������� ������:");
                    for (int i = 0; i < magazines.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {magazines[i].EditionName}");
                    }

                    Console.Write("������� ����� �������: ");
                    if (int.TryParse(Console.ReadLine(), out int selectedMag) && selectedMag > 0 && selectedMag <= magazines.Count)
                    {
                        currentMagazine = magazines[selectedMag - 1];
                        Console.WriteLine($"������ ������: {currentMagazine.EditionName}");
                    }
                    else
                    {
                        Console.WriteLine("�������� ����� �������.");
                    }
                    break;

                case "8":
                    if (magazines.Count == 0)
                    {
                        Console.WriteLine("��� ��������� ��������.");
                        break;
                    }

                    Console.WriteLine("\n=== ������ �������� ===");
                    for (int i = 0; i < magazines.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {magazines[i].EditionName}");
                    }
                    break;

                case "9":
                    isRunning = false;
                    Console.WriteLine("��������� ���������.");
                    break;

                default:
                    Console.WriteLine("�������� ����. ���������� �����.");
                    break;
            }
        }
    }

    private static Magazine CreateNewMagazine()
    {
        Console.WriteLine("\n=== �������� ������ ������� ===");

        Console.Write("������� �������� �������: ");
        string name = Console.ReadLine() ?? "����� ������";

        Console.Write("������� ���� ������� (����-��-��): ");
        DateTime.TryParse(Console.ReadLine(), out DateTime date);

        Console.Write("������� �����: ");
        int.TryParse(Console.ReadLine(), out int tirage);

        Console.Write("������� ������� ������� (Weekly, Monthly, Yearly): ");
        Enum.TryParse(Console.ReadLine(), out Frequency frequency);

        return new Magazine(name, date, tirage, frequency, new List<Person>(), new List<Article>());
    }

    private static Magazine? LoadMagazineFromFile()
    {
        Console.Write("\n������� ���� � �����: ");
        string? filename = Console.ReadLine();

        if (string.IsNullOrEmpty(filename))
        {
            Console.WriteLine("�� ������ ����.");
            return null;
        }

        if (!File.Exists(filename))
        {
            Console.WriteLine("���� �� ����������.");
            return null;
        }

        if (Magazine.Load(filename, out Magazine? loadedMagazine) && loadedMagazine != null)
        {
            Console.WriteLine("������ ������� ��������!");
            return loadedMagazine;
        }

        Console.WriteLine("�� ������� ��������� ������.");
        return null;
    }

    private static void SaveMagazineToFile(Magazine magazine)
    {
        Console.Write("\n������� ��� ��� ����������: ");
        string? filename = Console.ReadLine();

        if (string.IsNullOrEmpty(filename))
        {
            Console.WriteLine("�� ������ ����.");
            return;
        }

        if (magazine.Save(filename))
        {
            Console.WriteLine("������ ������� ��������!");
        }
        else
        {
            Console.WriteLine("�� ������� ��������� ������.");
        }
    }
}