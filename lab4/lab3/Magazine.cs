using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lab3
{
    public class Magazine : Edition, IRateAndCopy
    {
        private Frequency frequency;
        private List<Person> editors;
        private List<Article> articles;

        // Конструктор с параметрами для инициализации всех полей
        //base Ключевое слово используется для доступа к членам базового класса из производного класса
        public Magazine(string title, Frequency frequency, DateTime releaseDate, int editions)
            : base(title, releaseDate, editions)
        {
            this.frequency = frequency;
            this.editors = new List<Person>();
            this.articles = new List<Article>();
        }

        // Конструктор без параметров, инициализирующий поля значениями по умолчанию
        public Magazine()
            : base()
        {
            this.frequency = Frequency.Weekly;
            this.editors = new List<Person>();
            this.articles = new List<Article>();
        }

        // Свойства для доступа к полям
        public Frequency Frequency
        {
            get => frequency;
            set => frequency = value;
        }

        public List<Person> Editors
        {
            get => editors;
            set => editors = value ?? throw new ArgumentNullException(nameof(value), "Список редакторов не может быть null.");
        }

        public List<Article> Articles
        {
            get => articles;
            set => articles = value ?? throw new ArgumentNullException(nameof(value), "Список статей не может быть null.");
        }

        // Свойство для вычисления среднего рейтинга статей
        public double AverageRating
        {
            get
            {
                if (articles.Count == 0)
                    return 0.0;

                return articles.Average(article => article.Rating);
            }
        }

        // Метод для добавления статей в журнал
        public void AddArticles(params Article[] newArticles)
        {
            if (newArticles == null)
                throw new ArgumentNullException(nameof(newArticles), "Массив статей не может быть null.");

            foreach (var article in newArticles)
            {
                // Добавляем статью
                articles.Add(article);

                // Добавляем автора статьи в список редакторов, если его там еще нет
                if (!editors.Contains(article.Data))
                {
                    editors.Add(article.Data);
                }
            }
        }

        // Метод для добавления редакторов в журнал
        public void AddEditors(params Person[] newEditors)
        {
            if (newEditors == null)
                throw new ArgumentNullException(nameof(newEditors), "Массив редакторов не может быть null.");

            editors.AddRange(newEditors);
        }

        // Переопределенный метод ToString для вывода информации о журнале и его статьях
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            // Основная информация о журнале
            sb.AppendLine($"Название журнала: {Title}");
            sb.AppendLine($"Периодичность: {Frequency}");
            sb.AppendLine($"Дата выхода: {ReleaseDate:yyyy-MM-dd}");
            sb.AppendLine($"Тираж: {Editions}");
            sb.AppendLine($"Средний рейтинг: {AverageRating:F2}");

            // Таблица статей
            if (articles.Count != 0)
            {
                sb.AppendLine("\nСтатьи:");
                sb.AppendLine(TableFormatter.FormatAsTable(
                    articles,
                    ("Название", a => a.TitleOfArticle),
                    ("Автор", a => $"{a.Data.Surname} {a.Data.Name}"),
                    ("Рейтинг", a => $"{a.Rating:F2}")
                ));
            }

            // Таблица редакторов
            if (editors.Count != 0)
            {
                sb.AppendLine("\nРедакторы:");
                sb.AppendLine(TableFormatter.FormatAsTable(
                    editors,
                    ("Фамилия", e => e.Surname),
                    ("Имя", e => e.Name),
                    ("Дата рождения", e => e.Birthday.ToString("yyyy-MM-dd"))
                ));
            }

            return sb.ToString();
        }

        // Метод для вывода краткой информации о журнале без списка статей, но с рейтингом
        public string ToShortString()
        {
            return $"Название журнала: {Title}\nПериодичность: {Frequency}\nДата выхода: {ReleaseDate}\nТираж: {Editions}\nСредний рейтинг статей: {AverageRating}\n";
        }

        // Переопределение метода DeepCopy для создания глубокой копии объекта
        public override object DeepCopy()
        {
            Magazine copy = new Magazine(Title, Frequency, ReleaseDate, Editions)
            {
                Editors = Editors.Select(editor => (Person)editor.DeepCopy()).ToList(),
                Articles = Articles.Select(article => (Article)article.DeepCopy()).ToList()
            };
            return copy;
        }

        // Итератор для перебора статей с рейтингом больше заданного значения
        public IEnumerable<Article> GetArticlesByRating(double minRating)
        {
            return articles.Where(article => article.Rating > minRating);
        }

        // Итератор для перебора статей, в названии которых есть заданная строка
        public IEnumerable<Article> GetArticlesByTitle(string titlePart)
        {
            if (string.IsNullOrEmpty(titlePart))
                throw new ArgumentException("Часть названия статьи не может быть пустой или null.", nameof(titlePart));

            return articles.Where(article => article.TitleOfArticle.Contains(titlePart, StringComparison.OrdinalIgnoreCase));
        }

        // Реализация свойства Rating из интерфейса IRateAndCopy
        public double Rating => AverageRating;

        // Методы для сортировки статей
        public void SortArticlesByTitle()
        {
            articles.Sort((a1, a2) => string.Compare(a1.TitleOfArticle, a2.TitleOfArticle, StringComparison.OrdinalIgnoreCase)); //Указывает, какой язык, региональные параметры, регистр и правила сортировки должны использовать определенные перегрузки методов Compare(String, String)
        }

        public void SortArticlesByAuthor()
        {
            articles.Sort((a1, a2) => string.Compare(a1.Data.Surname, a2.Data.Surname, StringComparison.OrdinalIgnoreCase));
        }

        public void SortArticlesByRating()
        {
            articles.Sort((a1, a2) => a2.Rating.CompareTo(a1.Rating));
        }
    }
}