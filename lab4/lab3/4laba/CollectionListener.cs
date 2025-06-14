using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    // Базовый класс слушателя изменений коллекции
    public abstract class CollectionListener<T> where T : class, INotifyPropertyChanged
    {
        // Наблюдаемая коллекция
        protected ObservableCollection<T> Collection { get; }

        // Конструктор с автоматической подпиской на события
        protected CollectionListener(ObservableCollection<T> collection)
        {
            Collection = collection ?? throw new ArgumentNullException(nameof(collection));
            Collection.CollectionChanged += OnCollectionChanged;
        }

        // Абстрактный метод обработки изменений 
        protected abstract void OnCollectionChanged(object sender, CollectionChangedEventArgs<T> e);

        // Метод для отписки от событий
        public void Detach()
        {
            Collection.CollectionChanged -= OnCollectionChanged;
        }
    }
}
