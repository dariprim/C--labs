using System;
using System.ComponentModel;

using System.Text.Json.Serialization;

[Serializable]
public class Edition
{
    protected string editionName;
    protected DateTime publicationDate;
    protected int tirage;

    // Конструктор с параметрами для инициализации полей
    public Edition(string editionName, DateTime publicationDate, int tirage)
    {
        this.editionName = editionName;
        this.publicationDate = publicationDate;
        this.tirage = tirage;
    }

    // Конструктор без параметров для инициализации по умолчанию
    public Edition()
    {
        editionName = "Unknown";
        publicationDate = DateTime.MinValue;
        tirage = 0;
    }

    // Свойства с методами get и set для доступа к полям
    public string EditionName
    {
        get => editionName;
        set
        {
            if (editionName != value)
            {
                editionName = value;
            }
        }
    }

    public DateTime PublicationDate
    {
        get => publicationDate;
        set
        {
            if (publicationDate != value)
            {
                publicationDate = value;
            }
        }
    }

    public int Tirage
    {
        get => tirage;
        set
        {
            if (value < 0)
                throw new ArgumentException("Тираж не может быть отрицательным.");
            if (tirage != value)
            {
                tirage = value;
            }
        }
    }

    // Переопределение метода ToString для вывода строки с данными
    public override string ToString()
    {
        return $"Название издания: {editionName}, Дата публикации: {publicationDate.ToShortDateString()}, Тираж: {tirage}";
    }
}
