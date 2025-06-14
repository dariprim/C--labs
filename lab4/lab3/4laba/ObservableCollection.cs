using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    // Пользовательский делегат для событий коллекции
    public delegate void CollectionChangedEventHandler<T>(object sender, CollectionChangedEventArgs<T> e);
    // Основной класс наблюдаемой коллекции
    public class ObservableCollection<T> : IEnumerable<T> where T : class, INotifyPropertyChanged
    {
        // Внутренний список для хранения элементов
        private readonly List<T> _items = new List<T>();

        // Событие, возникающее при изменениях в коллекции
        public event CollectionChangedEventHandler<T> CollectionChanged;

        // Метод для вызова события CollectionChanged
        protected virtual void OnCollectionChanged(ChangeType changeType, T item, string propertyName = null)
        {
            // Проверка наличия подписчиков
            CollectionChanged?.Invoke(this, new CollectionChangedEventArgs<T>(changeType, item, propertyName));
        }

        // Обработчик изменений свойств элементов коллекции
        private void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is T item && _items.Contains(item))
            {
                // При изменении свойства элемента генерируем событие
                OnCollectionChanged(ChangeType.ItemPropertyChanged, item, e.PropertyName);
            }
        }

        // Добавление элемента в коллекцию
        public void Add(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            _items.Add(item);
            // Подписываемся на изменения свойств элемента
            item.PropertyChanged += OnItemPropertyChanged;

            // Уведомляем о добавлении
            OnCollectionChanged(ChangeType.Add, item);
        }

        // Удаление элемента из коллекции
        public bool Remove(T item)
        {
            if (item == null) return false;

            bool removed = _items.Remove(item);
            if (removed)
            {
                // Отписываемся от изменений свойств элемента
                item.PropertyChanged -= OnItemPropertyChanged;
                // Уведомляем об удалении
                OnCollectionChanged(ChangeType.Remove, item);
            }

            return removed;
        }

        // Индексатор для доступа к элементам
        public T this[int index]
        {
            get => _items[index];
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                if (index < 0 || index >= _items.Count) throw new ArgumentOutOfRangeException(nameof(index));

                T oldItem = _items[index];
                // Отписываемся от старого элемента
                oldItem.PropertyChanged -= OnItemPropertyChanged;

                _items[index] = value;
                // Подписываемся на новый элемент
                value.PropertyChanged += OnItemPropertyChanged;

                // Уведомляем о замене
                OnCollectionChanged(ChangeType.Replace, value);
            }
        }

        // Реализация IEnumerable<T>
        public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();

        // Реализация IEnumerable
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        // Очистка коллекции
        public void Clear()
        {
            // Отписываемся от всех элементов
            foreach (var item in _items)
            {
                item.PropertyChanged -= OnItemPropertyChanged;
            }

            _items.Clear();
            // Уведомление о полной очистке можно добавить при необходимости
        }
    }
}
