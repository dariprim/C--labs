using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    public class Person : IRateAndCopy
    {
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

        // Свойство для получения и установки года рождения
        public int BirthYear
        {
            get => Birthday.Year;
            set
            {
                Birthday = new DateTime(value, Birthday.Month, Birthday.Day);
            }
        }

        // Реализация свойства Rating из интерфейса IRateAndCopy
        public double Rating => throw new NotImplementedException();

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

        // Переопределение метода Equals для сравнения объектов по значению
        public override bool Equals(object obj)
        {
            if (obj == null || obj is not Person)
                return false;

            Person other = (Person)obj;
            return name == other.name &&
                   surname == other.surname &&
                   birthday == other.birthday;
        }

        // Переопределение метода GetHashCode для получения хеш-кода объекта
        public override int GetHashCode()
        {
            return (name.GetHashCode() ^ surname.GetHashCode() ^ birthday.GetHashCode());
        }

        // Перегрузка операторов == и != для сравнения объектов по значению
        public static bool operator ==(Person person1, Person person2)
        {
            if (ReferenceEquals(person1, person2))
                return true;

            if (person1 is null || person2 is null)
                return false;

            return person1.Equals(person2);
        }

        public static bool operator !=(Person person1, Person person2)
        {
            return !(person1 == person2);
        }

        // Реализация метода DeepCopy из интерфейса IRateAndCopy
        public virtual object DeepCopy()
        {
            return new Person(name, surname, birthday);
        }
    }
}
