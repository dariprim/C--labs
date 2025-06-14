using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

// Перечисление для частоты выхода журнала
enum Frequency
{
    Weekly,   // Еженедельно
    Monthly,  // Ежемесячно
    Yearly    // Ежегодно
}

class Article
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
}

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
        Articles = Array.Empty<Article>();
    }

    // Свойства для доступа к полям журнала
    public string TitleOfMagazine
    {
        get => titleOfMagazine;
        set => titleOfMagazine = value;
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
            articlesList.Append(article.TitleOfArticle).Append(", ");
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

class Person
{
    // Закрытые поля для хранения данных человека
    private string name;
    private string surname;
    private DateTime birthday;

    // Конструктор с параметрами для инициализации всех полей
    public Person(string name, string surname, DateTime birthday)
    {
        Name = name;
        Surname = surname;
        Birthday = birthday;
    }

    // Конструктор без параметров, инициализирующий поля значениями по умолчанию
    public Person()
    {
        Name = "Name";
        Surname = "Surname";
        Birthday = DateTime.Now;
    }

    // Свойства для доступа к полям
    public string Name
    {
        get => name;
        set => name = value;
    }

    public string Surname
    {
        get => surname;
        set => surname = value;
    }

    public DateTime Birthday
    {
        get => birthday;
        set => birthday = value;
    }

    public int BirthYear
    {
        get => Birthday.Year;
        set
        {
            Birthday = new DateTime(value, Birthday.Month, Birthday.Day);
        }
    }

    // Переопределенный метод ToString для вывода информации о человеке
    public override string ToString()
    {
        return $"Имя: {Name}\nФамилия: {Surname}\nДень рождения: {Birthday}";
    }

    // Метод для вывода краткой информации о человеке
    public virtual string ToShortString()
    {
        return $"Имя: {Name}\nФамилия: {Surname}";
    }
}

class Program
{
    static void Main()
    {
        // Создание объекта журнала и вывод краткой информации
        Magazine magazine = new Magazine();
        Console.WriteLine(magazine.ToShortString());

        // Вывод значений индексатора для различных частот
        Console.WriteLine($"Еженедельно: {magazine[Frequency.Weekly]}\n");
        Console.WriteLine($"Ежемесячно: {magazine[Frequency.Monthly]}\n");
        Console.WriteLine($"Ежегодно: {magazine[Frequency.Yearly]}\n");

        // Изменение свойств журнала и добавление статьи
        magazine.TitleOfMagazine = "Новые звезды";
        magazine.Frequency = Frequency.Monthly;
        magazine.Date = DateTime.Now;
        magazine.Edition = 100;
        magazine.AddArticles(new Article(new Person("Полина", "Гагарина", new DateTime(1990, 5, 17)), "Статья 1", 4.5));

        // Вывод полной информации о журнале
        Console.WriteLine(magazine.ToString());
       


        // Добавление статей и вывод обновленной информации
        magazine.AddArticles(
            new Article(new Person("Джонсон", "Бобсон", new DateTime(1985, 8, 23)), "Статья 2", 3.8),
            new Article(new Person("Пи", "Дядя", new DateTime(1992, 11, 30)), "Статья 3", 4.2)
        );

        Console.WriteLine(magazine.ToString());
        // Вывод имен авторов статей
        Console.WriteLine("Авторы статей в журнале:");
        foreach (var article in magazine.Articles)
        {
            Console.WriteLine(article.ToString());
        }

        // Ввод размеров массивов и измерение времени выполнения операций
        Console.WriteLine("Введите число строк и столбцов через разделитель (например, '3;4'):");
        string input = Console.ReadLine();
        string[] dimensions = input.Split(';', ',', ' ');

        int nrow = int.Parse(dimensions[0]);
        int ncolumn = int.Parse(dimensions[1]);

        Article[] oneDimArray = new Article[nrow * ncolumn];
        Article[,] twoDimArray = new Article[nrow, ncolumn];
        Article[][] jaggedArray = new Article[nrow][];

        for (int i = 0; i < nrow; i++)
        {
            jaggedArray[i] = new Article[ncolumn];
        }

        Stopwatch stopwatch = new Stopwatch();

        // Измерение времени для одномерного массива
        stopwatch.Start();
        for (int i = 0; i < oneDimArray.Length; i++)
        {
            oneDimArray[i] = new Article();
            oneDimArray[i].TitleOfArticle = "Уандим";
        }
        stopwatch.Stop();
        Console.WriteLine($"Время выполнения для одномерного массива: {stopwatch.ElapsedMilliseconds} мс\n");

        // Измерение времени для двумерного прямоугольного массива
        stopwatch.Restart();
        for (int i = 0; i < nrow; i++)
        {
            for (int j = 0; j < ncolumn; j++)
            {
                twoDimArray[i, j] = new Article();
                twoDimArray[i, j].TitleOfArticle = "Тудим";
            }
        }
        stopwatch.Stop();
        Console.WriteLine($"Время выполнения для двумерного прямоугольного массива: {stopwatch.ElapsedMilliseconds} мс\n");

        // Измерение времени для двумерного ступенчатого массива
        stopwatch.Restart();
        for (int i = 0; i < nrow; i++)
        {
            for (int j = 0; j < ncolumn; j++)
            {
                jaggedArray[i][j] = new Article();
                jaggedArray[i][j].TitleOfArticle = "Туда-сюда дим";
            }
        }
        stopwatch.Stop();
        Console.WriteLine($"Время выполнения для двумерного ступенчатого массива: {stopwatch.ElapsedMilliseconds} мс\n");
    }
}
