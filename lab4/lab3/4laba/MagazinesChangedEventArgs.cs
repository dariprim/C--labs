using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    // Делегат для события изменения коллекции
    public delegate void MagazinesChangedHandler<TKey>(object source, MagazinesChangedEventArgs<TKey> args);

    // Класс аргументов события изменения коллекции
    public class MagazinesChangedEventArgs<TKey> : EventArgs
    {
        public string CollectionName { get; }
        public ChangeType ChangeType { get; }
        public string PropertyName { get; }
        public TKey Key { get; }

        public MagazinesChangedEventArgs(string collectionName, ChangeType changeType, string propertyName, TKey key)
        {
            CollectionName = collectionName;
            ChangeType = changeType;
            PropertyName = propertyName ?? string.Empty;
            Key = key;
        }

        public override string ToString()
        {
            return $"Collection: {CollectionName}, ChangeType: {ChangeType}, " +
                   $"Property: {PropertyName}, Key: {Key}";
        }
    }
}
