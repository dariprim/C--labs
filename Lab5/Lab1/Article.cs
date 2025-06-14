using System;
using System.Collections;
using System.Text.Json.Serialization;

[Serializable]
public class Article
{
    // ��������������� ��������
    public Person Author { get; set; }
    public string Title { get; set; }
    private double ratingValue;

    // ����������� � �����������
    public Article(Person author, string title, double rating)
    {
        Author = author;
        Title = title;
        Rating = rating;
    }

    // ����������� ��� ����������
    public Article()
    {
        Author = new Person();
        Title = "Default";
        Rating = 0.0;
    }

    // ���������� �������� Rating �� ���������� IRateAndCopy
    public double Rating
    {
        get => ratingValue;
        set => ratingValue = value;
    }

    // ������������� ����� ToString()
    public override string ToString()
    {
        return $"�����: {Author.ToShortString()}, ��������: {Title}, �������: {Rating}";
    }
}