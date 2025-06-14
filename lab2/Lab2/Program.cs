using Lab2;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Lab2
{
    class Program
    {
        static void Main()
        {
            // Создание объекта журнала и вывод краткой информации
            Magazine magazine = new Magazine();

            //тесты Equal
            Person person1 = new Person("a", "a", new DateTime(2025, 10, 23));
            Person person2 = new Person("a", "a", new DateTime(2025, 10, 23));
            Console.WriteLine(person1.Equals(person2));

            //тесты хэшкода
            var hashcode1 = person1.GetHashCode();
            var hashcode2 = person2.GetHashCode();
            Console.WriteLine($"person1.GetHashCode == person2.GetHashCode:{hashcode1 == hashcode2}");

            //тесты функции глубокого копирования
            Person person3 = person1.DeepCopy();
            var hashcode3 = person3.GetHashCode();
            //изменим исходник
            person1.Name = "b";
            Console.WriteLine($"Исходник:\n {person1} \nглубокая копия:\n {person3}");

            //// Изменение свойств журнала и добавление статьи
            //magazine.TitleOfMagazine = "Новые звезды";
            //magazine.Frequency = Frequency.Monthly;
            //magazine.Date = DateTime.Now;
            //magazine.Edition = 100;
            //magazine.AddArticles(new Article(new Person("Полина", "Гагарина", new DateTime(1990, 5, 17)), "Статья 1", 4.5));

            //// Добавление статей и вывод обновленной информации
            //magazine.AddArticles(
            //    new Article(new Person("Джонсон", "Бобсон", new DateTime(1985, 8, 23)), "Статья 2", 3.8),
            //    new Article(new Person("Пи", "Дядя", new DateTime(1992, 11, 30)), "Статья 3", 4.2)
            //);


        }
    }


}
