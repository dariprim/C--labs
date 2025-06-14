using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    public class Article : IRateAndCopy, IComparable<Article>, IComparer<Article>
    {
        // Автореализуемые свойства для данных автора, названия статьи и рейтинга
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
            return $"\nИнформация об авторе:\n{Data}\nНазвание статьи: {TitleOfArticle}\nРейтинг: {Rating}\n";
        }

        // Реализация метода DeepCopy из интерфейса IRateAndCopy
        public virtual object DeepCopy()
        {
            return new Article((Person)Data.DeepCopy(), TitleOfArticle, Rating);
        }

        // Реализация IComparable<Article>
        public int CompareTo(Article other)
        {
            if (other == null)
                return 1; // Если другой объект null, текущий объект считается больше.

            return string.Compare(this.TitleOfArticle, other.TitleOfArticle, StringComparison.Ordinal);
        }

        public int Compare(Article x, Article y)
        {
            if (x == null && y == null)
                return 0; // Оба объекта null, считаются равными.
            if (x == null)
                return -1; // Если x null, а y нет, x считается меньше.
            if (y == null)
                return 1; // Если y null, а x нет, y считается меньше.

            return string.Compare(x.Data.Surname, y.Data.Surname, StringComparison.Ordinal);
        }
    }
}
