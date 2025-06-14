using System;
using System.Collections.Generic;
using System.Diagnostics;
//Событие — это именованный делегат, при вызове которого, будут запущены все подписавшиеся на момент вызова события методы заданной сигнатуры.
namespace lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Создаем две коллекции с разными названиями
            var keySelector = new KeySelector<string>(mg => mg.Title);
            var collection1 = new MagazineCollection<string>(keySelector, "Science Journals");
            var collection2 = new MagazineCollection<string>(keySelector, "Tech Publications");

            // 2. Создаем слушатель(подписчика) и подписываем на обе коллекции
            var listener = new Listener();
            collection1.MagazinesChanged += listener.MagazineCollectionChanged;
            collection2.MagazinesChanged += listener.MagazineCollectionChanged;

            Console.WriteLine("=== Начало изменений ===");

            // 3. Вносим изменения в первую коллекцию
            // Добавляем элементы
            var mag1 = new Magazine("Nature", Frequency.Monthly, new DateTime(2025, 1, 1), 5000);
            var mag2 = new Magazine("Science", Frequency.Weekly, new DateTime(2025, 2, 1), 8000);
            collection1.AddMagazines(mag1, mag2);

            // Изменяем свойства элементов
            mag1.Title = "Nature International";
            mag1.Editions = 8500;
            mag1.ReleaseDate = new DateTime(2025, 3, 1);

            // Заменяем элемент
            var mag3 = new Magazine("New Science", Frequency.Weekly, new DateTime(2025, 4, 1), 9000);
            bool replaced = collection1.Replace(mag2, mag3);
            Console.WriteLine($"Замена выполнена: {replaced}");

            // Изменяем данные в удаленном элементе, который заменили (mag2)
            mag2.Title = "Old Science";
            mag2.Editions = 7000;

            // Вносим изменения во вторую коллекцию
            var techMag1 = new Magazine("Tech Today", Frequency.Monthly, new DateTime(2025, 5, 1), 3000);
            collection2.AddMagazines(techMag1);
            techMag1.Frequency = Frequency.Yearly;

            // 4. Выводим данные слушателя
            Console.WriteLine("\n=== Итоговый отчет об изменениях ===");
            Console.WriteLine(listener.ToString());
            //проверяем, равны ли ссылки 
            //Console.WriteLine(mag2.Equals(mag3));
            
            Console.WriteLine("\n=== Итоги ===");
            var formattedChanges = TableFormatter.FormatAsTable(
                listener.GetChanges().Select((e, i) => new { Index = i + 1, Entry = e }),
                ("№", x => x.Index),
                ("Коллекция", x => x.Entry.CollectionName),
                ("Тип изменения", x => x.Entry.ChangeType),
                ("Измененное свойство", x => x.Entry.PropertyName),
                ("Ключ", x => x.Entry.Key)
            );
            Console.WriteLine(formattedChanges);

            // Вывод текущего состояния коллекций
            Console.WriteLine("\n=== Текущее состояние Science Journals ===");
            Console.WriteLine(collection1.ToString());

            Console.WriteLine("\n=== Текущее состояние Tech Publications ===");
            Console.WriteLine(collection2.ToString());
        }
    }
}