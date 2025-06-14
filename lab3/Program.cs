using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Создание объекта Magazine и сортировка статей
            Console.WriteLine("=== Задача 1: Сортировка статей в журнале ===");
            Magazine magazine = new Magazine("Tech Magazine", Frequency.Monthly, new DateTime(2025, 10, 1), 1000);

            // Добавление статей
            magazine.AddArticles(
                new Article(new Person("John", "Doe", new DateTime(1980, 1, 1)), "C# Programming", 4.5),
                new Article(new Person("Jane", "Amith", new DateTime(1990, 5, 15)), "Advanced C#", 3.8),
                new Article(new Person("Alice", "Bohnson", new DateTime(1975, 10, 20)), "Design Patterns", 4.2)
            );

            // Вывод до сортировки
            Console.WriteLine("До сортировки:");
            Console.WriteLine(magazine.ToString());

            // Сортировка по названию статьи
            magazine.SortArticlesByTitle();
            Console.WriteLine("После сортировки по названию:");
            Console.WriteLine(magazine.ToString());

            // Сортировка по фамилии автора
            magazine.SortArticlesByAuthor();
            Console.WriteLine("После сортировки по фамилии автора:");
            Console.WriteLine(magazine.ToString());

            // Сортировка по рейтингу статьи
            magazine.SortArticlesByRating();
            Console.WriteLine("После сортировки по рейтингу:");
            Console.WriteLine(magazine.ToString());

            // 2. Создание объекта MagazineCollection<string> и добавление элементов
            Console.WriteLine("\n=== Задача 2: Работа с MagazineCollection<string> ===");
            KeySelector<Guid> keySelector = mg => Guid.NewGuid();
            var magazineCollection = new MagazineCollection<Guid>(keySelector);

            // Добавление элементов
            magazineCollection.AddMagazines(
                new Magazine("Tech Magazine", Frequency.Monthly, new DateTime(2023, 10, 1), 1000)
                {
                    Articles =
                    [
            new Article(new Person("John", "Doe", new DateTime(1980, 1, 1)), "C# Programming", 4.5),
            new Article(new Person("Jane", "Smith", new DateTime(1990, 5, 15)), "Advanced C#", 3.8)
                    ]
                },
                new Magazine("Science Journal", Frequency.Weekly, new DateTime(2023, 9, 1), 1500)
                {
                    Articles =
                    [
            new Article(new Person("Alice", "Johnson", new DateTime(1975, 10, 20)), "Physics Today", 4.2),
            new Article(new Person("Bob", "Wilson", new DateTime(1985, 3, 10)), "Chemistry News", 3.9)
                    ]
                },
                new Magazine("Art Journal", Frequency.Yearly, new DateTime(2023, 8, 1), 500)
                {
                    Articles =
                    [
            new Article(new Person("Emily", "Davis", new DateTime(1995, 7, 22)), "Modern Art", 4.7),
            new Article(new Person("David", "Brown", new DateTime(1982, 11, 5)), "Classic Paintings", 4.0)
                    ]
                }
            );

            // Вывод коллекции
            Console.WriteLine("Содержимое MagazineCollection<string>:");
            Console.WriteLine(magazineCollection.ToString());

            // 3. Вызов методов MagazineCollection<string>
            Console.WriteLine("\n=== Задача 3: Операции с MagazineCollection<string> ===");

            // Вычисление максимального среднего рейтинга
            Console.WriteLine($"Максимальный средний рейтинг: {magazineCollection.MaxAverageRating}");

            // Выбор журналов с заданной периодичностью (Monthly)
            Console.WriteLine("\nЖурналы с периодичностью Monthly:");
            foreach (var kvp in magazineCollection.FrequencyGroup(Frequency.Monthly))
            {
                Console.WriteLine($"Ключ: {kvp.Key}, Название: {kvp.Value.Title}");
            }

            // Группировка по периодичности
            Console.WriteLine("\nГруппировка по периодичности:");
            foreach (var group in magazineCollection.GroupByFrequency)
            {
                Console.WriteLine($"Периодичность: {group.Key}");
                foreach (var kvp in group)
                {
                    Console.WriteLine($"  Ключ: {kvp.Key}, Название: {kvp.Value.Title}");
                }
            }


            // 4. Создание объекта TestCollection<Edition, Magazine> и тестирование поиска
            Console.WriteLine("\n=== Задача 4: Тестирование поиска в TestCollections ===");

            // Ввод числа элементов
            int count = 0;
            while (true)
            {
                Console.Write("Введите число элементов в коллекции: ");
                if (int.TryParse(Console.ReadLine(), out count) && count > 0)
                    break;
                Console.WriteLine("Ошибка ввода. Введите положительное целое число.");
            }

            // Создание TestCollection с заполненными журналами
            var testCollection = new TestCollections<Edition, Magazine>(count, (int j) =>
            {
                var edition = new Edition($"Edition{j}", new DateTime(2025, 1, 1), j * 100);

                var magazine = new Magazine($"Magazine{j}", Frequency.Monthly, new DateTime(2025, 1, 1), j * 1000)
                {
                    Articles =
                    [
                        new Article(new Person($"Author{j}_1", $"Lastname{j}_1", new DateTime(1980 + j, 1, 1)),
                       $"Article{j}_1", 3.5 + j * 0.1),
            new Article(new Person($"Author{j}_2", $"Lastname{j}_2", new DateTime(1985 + j, 1, 1)),
                       $"Article{j}_2", 4.0 + j * 0.1)
                    ]
                };

                return new KeyValuePair<Edition, Magazine>(edition, magazine);
            });

            // Измерение времени поиска
            Console.WriteLine("\nВремя поиска элементов:");
            testCollection.MeasureSearchTime();


        }
    }
}