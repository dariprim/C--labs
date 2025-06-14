using System;
using System.Collections;
using System.Text.Json.Serialization;

[Serializable]
public class Article
{
    // Автореализуемые свойства
    public Person Author { get; set; }
    public string Title { get; set; }
    private double ratingValue;

    // Конструктор с параметрами
    public Article(Person author, string title, double rating)
    {
        Author = author;
        Title = title;
        Rating = rating;
    }

    // Конструктор без параметров
    public Article()
    {
        Author = new Person();
        Title = "Default";
        Rating = 0.0;
    }

    // Реализация свойства Rating из интерфейса IRateAndCopy
    public double Rating
    {
        get => ratingValue;
        set => ratingValue = value;
    }

    // Перегруженный метод ToString()
    public override string ToString()
    {
        return $"Автор: {Author.ToShortString()}, Название: {Title}, Рейтинг: {Rating}";
    }
}