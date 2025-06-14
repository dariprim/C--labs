using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    class Article
    {
        // Автореализуемые свойства для данных автора, названия статьи и рейтинга
        //Автоматически реализованные свойства делают объявление свойств более кратким, если в методах доступа к свойствам не требуется другая логика.
        //Они также позволяют клиентскому коду создавать объекты. 
        public Person Data { get; set; }
        public string TitleOfArticle { get; set; }
        public double Rating { get; set; }

        // Конструктор с параметрами для инициализации всех свойств
        public Article(Person data, string titleOfArticle, double rating)
        {
            Data = data;
            TitleOfArticle = titleOfArticle;
            Rating = rating;
        }

        // Конструктор без параметров, инициализирующий свойства значениями по умолчанию
        public Article()
        {
            Data = new Person("Bob", "Job", DateTime.Now);
            TitleOfArticle = "SuperPuper";
            Rating = 0.0;
        }

        // Переопределенный метод ToString для вывода информации о статье
        public override string ToString()
        {
            return $"\nИнформация об авторе:\n{Data}\nНазвание статьи: {TitleOfArticle}\nРейтинг: {Rating}\n"; //используем ф-строку
        }
    }

}
