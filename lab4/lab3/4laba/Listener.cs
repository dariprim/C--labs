using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    // Класс для отслеживания изменений в MagazineCollection
    public class Listener
    {
        private List<ListEntry> _changes = new List<ListEntry>();

        // Обработчик события изменения коллекции
        public void MagazineCollectionChanged(object source, MagazinesChangedEventArgs<string> args)
        {
            _changes.Add(new ListEntry(
                args.CollectionName,
                args.ChangeType,
                args.PropertyName,
                args.Key?.ToString() ?? "null"));
        }

        // Метод для получения всех записанных изменений
        public IEnumerable<ListEntry> GetChanges() => _changes.AsReadOnly();

        // Очистка списка изменений
        public void Clear() => _changes.Clear();

        public override string ToString()
        {
            if (_changes.Count == 0)
                return "No changes recorded";

            var sb = new StringBuilder();
            sb.AppendLine($"Total changes: {_changes.Count}");
            sb.AppendLine("Change log:");

            foreach (var entry in _changes)
            {
                sb.AppendLine(entry.ToString());
            }

            return sb.ToString();
        }
    }
}

