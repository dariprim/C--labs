using System;
using System.Collections;
using System.Text.Json.Serialization;

[Serializable]
public class Person
{
    private string firstName;
    private string lastName;
    private DateTime birthDate;

    public double Rating { get; private set; }

    // Конструктор с параметрами
    public Person(string firstName, string lastName, DateTime birthDate, double rating)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.birthDate = birthDate;
        Rating = rating;
    }

    // Конструктор без параметров
    public Person()
    {
        firstName = "Default";
        lastName = "Default";
        birthDate = new DateTime(2025, 1, 1);
    }

    // Свойства для доступа к полям
    public string FirstName
    {
        get => firstName;
        set => firstName = value;
    }

    public string LastName
    {
        get => lastName;
        set => lastName = value;
    }

    public DateTime BirthDate
    {
        get => birthDate;
        set => birthDate = value;
    }

    public int BirthYear
    {
        get => birthDate.Year;
        set => birthDate = new DateTime(value, birthDate.Month, birthDate.Day);
    }

    // Переопределенный метод ToString()
    public override string ToString()
    {
        return $"Имя: {firstName} {lastName}, Дата рождения: {birthDate.ToShortDateString()}";
    }

    // Виртуальный метод ToShortString()
    public virtual string ToShortString()
    {
        return $"{firstName} {lastName}";
    }
}
