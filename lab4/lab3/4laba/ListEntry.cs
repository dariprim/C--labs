using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    // Класс для хранения информации об  изменении
    public class ListEntry
    {
        public string CollectionName { get; }
        public ChangeType ChangeType { get; }
        public string PropertyName { get; }
        public string Key { get; }

        public ListEntry(string collectionName, ChangeType changeType, string propertyName, string key)
        {
            CollectionName = collectionName;
            ChangeType = changeType;
            PropertyName = propertyName ?? string.Empty;
            Key = key;
        }

        public override string ToString()
        {
            return $"Collection: {CollectionName}, " +
                   $"Change: {ChangeType}, " +
                   $"Property: {(string.IsNullOrEmpty(PropertyName) ? "N/A" : PropertyName)}, " +
                   $"Key: {Key}";
        }
    }

}
