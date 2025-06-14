using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    class Magazine
    {
        // Закрытые поля для хранения данных журнала
        private string titleOfMagazine;
        private Frequency frequency;
        private DateTime date;
        private int edition;
        private Article[] articles;

        // Конструктор с параметрами для инициализации всех полей
        public Magazine(string titleOfMagazine, Frequency frequency, DateTime date, int edition, Article[] articles)
        {
            TitleOfMagazine = titleOfMagazine;
            Frequency = frequency;
            Date = date;
            Edition = edition;
            Articles = articles;
        }

        // Конструктор без параметров, инициализирующий поля значениями по умолчанию
        public Magazine()
        {
            TitleOfMagazine = "SuperDuper";
            Frequency = Frequency.Weekly;
            Date = DateTime.Now;
            Edition = 0;
            Articles = Array.Empty<Article>(); //Это метод, который возвращает пустой массив указанного типа. В данном случае тип — это Article.

        }

        // Свойства для доступа к полям журнала
        public string TitleOfMagazine
        {
            get => titleOfMagazine;
            set => titleOfMagazine = value; //Это входной параметр метода. Ключевое слово value ссылается на значение, которое клиентский код пытается присвоить свойству или индексатору
        }

        public Frequency Frequency
        {
            get => frequency;
            set => frequency = value;
        }

        public DateTime Date
        {
            get => date;
            set => date = value;
        }

        public int Edition
        {
            get => edition;
            set => edition = value;
        }

        public Article[] Articles
        {
            get => articles;
            set => articles = value;
        }

        // Свойство для вычисления среднего рейтинга статей
        public double AverageRating
        {
            get
            {
                if (articles.Length == 0)
                    return 0.0;

                double totalRating = articles.Sum(article => article.Rating);
                //Это лямбда-выражение, которое определяет, какое значение должно быть суммировано для каждого объекта в коллекции.
                //В данном случае, для каждого объекта article берется значение свойства Rating
                return totalRating / articles.Length;
            }
        }

       

        // Индексатор для проверки частоты выхода журнала
        public bool this[Frequency index]
        {
            get => frequency == index;
        }

        // Метод для добавления статей в журнал
        //В C# для объединения строк можно использовать метод String.Concat
        public void AddArticles(params Article[] newArticles)
        {
            articles = articles.Concat(newArticles).ToArray();

        }

        // Переопределенный метод ToString для вывода информации о журнале и его статьях
        public override string ToString()
        {
            StringBuilder articlesList = new StringBuilder();

            foreach (var article in articles)
            {
                articlesList.Append(article.TitleOfArticle).Append(", "); //разделяем склеенные названия
            }

            // Удаляем последнюю запятую и пробел, если статьи есть
            if (articlesList.Length > 0)
            {
                articlesList.Length -= 2;
            }

            return $"Название журнала: {TitleOfMagazine}\nПериодичность: {Frequency}\nДата выхода: {Date}\nТираж: {Edition}\nСтатьи: {articlesList}\n";
        }

        // Метод для вывода краткой информации о журнале без списка статей, но с рейтингом
        public virtual string ToShortString()
        {
            return $"Название журнала: {TitleOfMagazine}\nПериодичность: {Frequency}\nДата выхода: {Date}\nТираж: {Edition}\nСредний рейтинг статей: {AverageRating}\n";
        }
    }

}
