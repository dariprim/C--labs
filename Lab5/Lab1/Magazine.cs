using System;
using System.Collections;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

[Serializable]
public class Magazine : Edition
{
    // Закрытые поля
    private Frequency frequency; // Частота выпуска журнала
    private List<Person> editors; // Список редакторов
    private List<Article> articles; // Список статей

    // Конструктор с параметрами
    [JsonConstructor]
    public Magazine(string editionName, DateTime publicationDate, int tirage, Frequency frequency, List<Person> editors, List<Article> articles)
        : base(editionName, publicationDate, tirage)
    {
        this.frequency = frequency;
        this.editors = editors ?? new List<Person>();
        this.articles = articles ?? new List<Article>();
    }

    // Конструктор без параметров
    public Magazine() : base()
    {
        frequency = Frequency.Monthly;
        editors = new List<Person>();
        articles = new List<Article>();
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
        set => editors = value;
    }

    public List<Article> Articles
    {
        get => articles;
        set => articles = value;
    }

    // Метод для добавления статей
    public void AddArticles(params Article[] newArticles)
    {
        articles.AddRange(newArticles);
    }

    // Метод для добавления редакторов
    public void AddEditors(params Person[] newEditors)
    {
        editors.AddRange(newEditors);
    }

    // Переопределенный метод ToString
    public override string ToString()
    {
        string articlesInfo = string.Join("\n", articles.Select(a => a.ToString()));
        string editorsInfo = string.Join("\n", editors.Select(e => e.ToShortString()));
        return $"{base.ToString()}, Частота: {frequency}, Статьи: {articlesInfo}, Редакторы: {editorsInfo}";
    }

    // Экземплярный метод для создания полной копии объекта с использованием сериализации
    public object DeepCopy()
    {
        using var memoryStream = new MemoryStream();
        JsonSerializer.Serialize(memoryStream, this);
        memoryStream.Seek(0, SeekOrigin.Begin);
        return JsonSerializer.Deserialize<Magazine>(memoryStream) ?? throw new InvalidOperationException("Десериализация вернула null");
    }

    // Экземплярный метод для сохранения данных объекта в файл
    public bool Save(string filename)
    {
        try
        {
            using var fileStream = new FileStream(filename, FileMode.Create);
            JsonSerializer.Serialize(fileStream, this);
            Console.WriteLine($"Экземплярный метод Save: Magazine успешно сохранен в файл: {filename}");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Экземплярный метод Save: Ошибка при сохранении Magazine в файл: {filename}. Ошибка: {ex.Message}");
            return false;
        }
    }

    // Экземплярный метод для загрузки данных объекта из файла
    public bool Load(string filename)
    {
        try
        {
            using var fileStream = new FileStream(filename, FileMode.Open);
            var loadedMagazine = JsonSerializer.Deserialize<Magazine>(fileStream);
            if (loadedMagazine is not null)
            {
                frequency = loadedMagazine.frequency;
                editors = loadedMagazine.editors ?? new List<Person>();
                articles = loadedMagazine.articles ?? new List<Article>();
                EditionName = loadedMagazine.EditionName;
                PublicationDate = loadedMagazine.PublicationDate;
                Tirage = loadedMagazine.Tirage;
                Console.WriteLine($"Экземплярный метод Load: Magazine успешно загружен из файла: {filename}");
                return true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Экземплярный метод Load: Ошибка при загрузке Magazine из файла: {filename}. Ошибка: {ex.Message}");
        }
        return false;
    }

    // Статический метод для сохранения объекта в файл
    public static bool Save(string filename, Magazine obj)
    {
        try
        {
            using var fileStream = new FileStream(filename, FileMode.Create);
            JsonSerializer.Serialize(fileStream, obj);
            Console.WriteLine($"Статический метод Save: Magazine успешно сохранен в файл: {filename}");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Статический метод Save: Ошибка при сохранении Magazine в файл: {filename}. Ошибка: {ex.Message}");
            return false;
        }
    }

    // Статический метод для загрузки объекта из файла
    public static bool Load(string filename, out Magazine? obj)
    {
        try
        {
            using var fileStream = new FileStream(filename, FileMode.Open);
            var loadedObj = JsonSerializer.Deserialize<Magazine>(fileStream);
            if (loadedObj is not null)
            {
                obj = loadedObj;
                Console.WriteLine($"Статический метод Load: Magazine успешно загружен из файла: {filename}");
                return true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Статический метод Load: Ошибка при загрузке Magazine из файла: {filename}. Ошибка: {ex.Message}");
        }
        obj = null;
        return false;
    }

    // Метод для добавления в один из списков класса нового элемента, данные для которого вводятся с консоли
    public bool AddFromConsole()
    {
        while (true)
        {
            try
            {
                Console.WriteLine("Введите данные для новой статьи в формате: Название; Имя автора; Фамилия автора; Дата рождения автора (гггг-мм-дд); Рейтинг (0-10)");
                Console.WriteLine("Разделители: ';', ',' или ' '");

                string input = Console.ReadLine() ?? string.Empty;
                string[] parts = input.Split(new char[] { ';', ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != 5)
                {
                    Console.WriteLine("Неверный формат ввода. Ожидается 5 частей, разделенных ';', ',' или ' '. Попробуйте снова.");
                    continue;
                }

                string title = parts[0].Trim();
                string firstName = parts[1].Trim();
                string lastName = parts[2].Trim();
                if (!DateTime.TryParse(parts[3].Trim(), out DateTime birthDate))
                {
                    Console.WriteLine("Неверный формат даты. Попробуйте снова.");
                    continue;
                }
                if (!double.TryParse(parts[4].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out double rating) || rating < 0 || rating > 10)
                {
                    Console.WriteLine("Неверный формат рейтинга. Введите значение от 0 до 10. Попробуйте снова.");
                    continue;
                }

                var author = new Person(firstName, lastName, birthDate, rating);
                var newArticle = new Article(author, title, rating);

                AddArticles(newArticle);
                Console.WriteLine("Статья успешно добавлена.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}. Попробуйте снова.");
            }
        }
    }
}
