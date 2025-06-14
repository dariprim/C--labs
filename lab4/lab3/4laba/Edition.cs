using System;
using System.ComponentModel;

namespace lab3
{
    public class Edition : INotifyPropertyChanged
    {
        protected string title;
        protected DateTime releaseDate;
        protected int editions;

        // Событие, возникающее при изменении свойств
        public event PropertyChangedEventHandler PropertyChanged;

        // Конструктор с параметрами для инициализации всех полей
        public Edition(string title, DateTime releaseDate, int editions)
        {
            Title = title;
            ReleaseDate = releaseDate;
            Editions = editions;
        }

        // Конструктор без параметров, инициализирующий поля значениями по умолчанию
        public Edition()
        {
            Title = "Default Title";
            ReleaseDate = DateTime.Now;
            Editions = 0;
        }

        // Метод для вызова события PropertyChanged
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Свойства для доступа к полям с уведомлениями об изменениях
        public string Title
        {
            get => title;
            set
            {
                if (title != value)
                {
                    title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }

        public DateTime ReleaseDate
        {
            get => releaseDate;
            set
            {
                if (releaseDate != value)
                {
                    releaseDate = value;
                    OnPropertyChanged(nameof(ReleaseDate));
                }
            }
        }

        public int Editions
        {
            get => editions;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Тираж не может быть отрицательным.");

                if (editions != value)
                {
                    editions = value;
                    OnPropertyChanged(nameof(Editions));
                }
            }
        }

        // Метод для создания глубокой копии объекта
        public virtual object DeepCopy()
        {
            return new Edition(title, releaseDate, editions);
        }

        // Переопределение метода Equals для сравнения объектов по значению
        public override bool Equals(object obj)
        {
            if (obj == null || obj is not Edition)
                return false;

            Edition other = (Edition)obj;
            return title == other.title &&
                   releaseDate == other.releaseDate &&
                   editions == other.editions;
        }

        // Переопределение метода GetHashCode для получения хеш-кода объекта
        public override int GetHashCode()
        {
            return (title.GetHashCode() ^ releaseDate.GetHashCode() ^ editions.GetHashCode());
        }

        // Перегрузка операторов == и != для сравнения объектов по значению
        public static bool operator ==(Edition edition1, Edition edition2)
        {
            if (ReferenceEquals(edition1, edition2))
                return true;

            if (edition1 is null || edition2 is null)
                return false;

            return edition1.Equals(edition2);
        }

        public static bool operator !=(Edition edition1, Edition edition2)
        {
            return !(edition1 == edition2);
        }

        // Переопределение метода ToString для вывода информации об издании
        public override string ToString()
        {
            return $"Название издания: {Title}\nДата выхода: {ReleaseDate.ToShortDateString()}\nТираж: {Editions}";
        }
    }
}