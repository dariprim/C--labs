using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    // универсальный ласс аргументов события для изменений в коллекции
    public class CollectionChangedEventArgs<T> : EventArgs
    {
        public ChangeType ChangeType { get; }

        // Элемент, с которым связано изменение
        public T Item { get; }

        // Имя измененного свойства (если ChangeType == ItemPropertyChanged)
        public string PropertyName { get; }

        public CollectionChangedEventArgs(ChangeType changeType, T item, string propertyName = null)
        {
            ChangeType = changeType;
            Item = item;
            PropertyName = propertyName;
        }
    }

}
