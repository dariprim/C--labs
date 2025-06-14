using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
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
        public static bool operator ==(Person lhs, Person rhs)
        {
            // Если оба null, или оба не null и равны
            if (ReferenceEquals(lhs, rhs))
            {
                return true;
            }

            // Если один из них null, но не оба
            if (lhs is null || rhs is null)
            {
                return false;
            }

            // Возвращаем результат метода Equals
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Person lhs, Person rhs)
        {
            return !(lhs == rhs);
        }

        public override int GetHashCode()
        {
            //Выполнение побитовой операции OR над сгенерированными значениями хэш-кода
            //Если соответствующие биты отличаются, это дает 1.
            //Если соответствующие биты совпадают, это дает 0.
            return name.GetHashCode() ^ surname.GetHashCode() ^ birthday.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            
            if (obj == null)
            {
                return false;
            }
            
            if (!(obj is Person))
            {
                return false;
            }
            return (this.name == ((Person)obj).name)
 && (this.surname == ((Person)obj).surname) && (this.birthday == ((Person)obj).birthday);
        }

        public Person DeepCopy()
        {
            return new Person(this.name,this.surname,this.birthday);
                               
        }


    }
}




