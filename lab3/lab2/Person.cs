using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
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

        public override bool Equals(object obj)
        {
            if (obj == null || obj is not Person) 
                return false;
            Person other = (Person)obj;

            return name == other.name &&
                surname ==other.surname &&
                birthday == other.birthday;

        }

        public override int GetHashCode()
        {
      
            return HashCode.Combine(name, surname, birthday, Name, Surname, Birthday, BirthYear);
        }
    }



}
