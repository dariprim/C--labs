using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lab3
{
    public delegate TKey KeySelector<TKey>(Magazine mg);

    public class MagazineCollection<TKey>
    {
        private Dictionary<TKey, Magazine> _magazines;
        private KeySelector<TKey> _keySelector;

        // Событие изменения коллекции
        public event MagazinesChangedHandler<TKey> MagazinesChanged;

        // Название коллекции
        public string CollectionName { get; set; }

        // Конструктор с параметром типа KeySelector<TKey>
        public MagazineCollection(KeySelector<TKey> keySelector, string collectionName = "")
        {
            _keySelector = keySelector ?? throw new ArgumentNullException(nameof(keySelector));
            _magazines = new Dictionary<TKey, Magazine>();
            CollectionName = collectionName;
        }

        // Метод для вызова события изменения коллекции
        protected virtual void OnMagazinesChanged(ChangeType changeType, string propertyName, TKey key)
        {
            MagazinesChanged?.Invoke(this,
                new MagazinesChangedEventArgs<TKey>(CollectionName, changeType, propertyName, key));
        }

        // Подписка на изменения Magazine
        private void SubscribeToMagazineChanges(Magazine magazine, TKey key)
        {
            magazine.PropertyChanged += (sender, e) =>
            {
                if (_magazines.ContainsKey(key))
                {
                    OnMagazinesChanged(ChangeType.ItemPropertyChanged, e.PropertyName, key);
                }
            };
        }

        // Отписка от изменений Magazine
        private void UnsubscribeFromMagazineChanges(Magazine magazine)
        {
            // В .NET нет необходимости явно отписываться, если используется weak reference
            // или если коллекция и журналы имеют одинаковое время жизни
        }

        // Метод для добавления элементов по умолчанию
        public void AddDefaults()
        {
            AddMagazines(
                new Magazine("Tech Magazine", Frequency.Monthly, new DateTime(2023, 10, 1), 1000),
                new Magazine("Science Journal", Frequency.Weekly, new DateTime(2023, 9, 1), 1500)
            );
        }

        // Метод для добавления элементов в коллекцию
        public void AddMagazines(params Magazine[] magazines)
        {
            foreach (var magazine in magazines)
            {
                TKey key = _keySelector(magazine);
                _magazines[key] = magazine;
                SubscribeToMagazineChanges(magazine, key);
                OnMagazinesChanged(ChangeType.Add, null, key);
            }
        }

        // Метод замены элемента
        public bool Replace(Magazine oldMagazine, Magazine newMagazine)
        {
            var kvp = _magazines.FirstOrDefault(x => x.Value == oldMagazine);
            if (kvp.Equals(default(KeyValuePair<TKey, Magazine>)))
                return false;

            _magazines[kvp.Key] = newMagazine;
            UnsubscribeFromMagazineChanges(oldMagazine);
            SubscribeToMagazineChanges(newMagazine, kvp.Key);
            OnMagazinesChanged(ChangeType.Replace, null, kvp.Key);
            return true;
        }

        // Переопределенный метод ToString
        public override string ToString()
        {
            return TableFormatter.FormatAsTable(
                _magazines,
                ("Ключ", kvp => kvp.Key),
                ("Название журнала", kvp => kvp.Value.Title),
                ("Периодичность", kvp => kvp.Value.Frequency),
                ("Дата выхода", kvp => kvp.Value.ReleaseDate.ToString("yyyy-MM-dd")),
                ("Тираж", kvp => kvp.Value.Editions)
            );
        }

        // Метод ToShortString
        public string ToShortString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var kvp in _magazines)
            {
                sb.AppendLine($"Ключ: {kvp.Key}");
                sb.AppendLine(kvp.Value.ToShortString());
            }
            return sb.ToString();
        }

        // Свойство для получения максимального среднего рейтинга статей
        public double MaxAverageRating
        {
            get
            {
                if (_magazines.Count == 0)
                    return 0.0;

                return _magazines.Values.Max(magazine => magazine.AverageRating);
            }
        }

        // Метод для получения подмножества элементов с заданной периодичностью
        public IEnumerable<KeyValuePair<TKey, Magazine>> FrequencyGroup(Frequency value)
        {
            return _magazines.Where(kvp => kvp.Value.Frequency == value);
        }

        // Свойство для группировки элементов по периодичности
        public IEnumerable<IGrouping<Frequency, KeyValuePair<TKey, Magazine>>> GroupByFrequency
        {
            get
            {
                return _magazines.GroupBy(kvp => kvp.Value.Frequency);
            }
        }
    }
}