using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
//Делегат — это тип, который представляет ссылки на методы с определенным списком параметров и типом 
//возвращаемого значения.При создании экземпляра делегата этот экземпляр можно связать с любым методом 
//с совместимой сигнатурой и типом возвращаемого значения.Метод можно вызвать (активировать) с помощью экземпляра делегата.
//Делегаты используются для передачи методов в качестве аргументов к другим методам.Обработчики событий — 
//это ничто иное, как методы, вызываемые с помощью делегатов. 
    public delegate KeyValuePair<TKey, TValue> GenerateElement<TKey, TValue>(int j);
    class TestCollections<TKey, TValue>
    {
        private List<TKey> listTKey;
        private List<string> listString;
        private Dictionary<TKey, TValue> dictTKeyTValue;
        private Dictionary<string, TValue> dictStringTValue;
        private GenerateElement<TKey, TValue> generateElement;

        // Конструктор
        public TestCollections(int count, GenerateElement<TKey, TValue> generator)
        {
            if (count <= 0)
                throw new ArgumentException("Количество элементов должно быть больше 0.");

            generateElement = generator;

            // Инициализация коллекций
            listTKey = new List<TKey>();
            listString = new List<string>();
            dictTKeyTValue = new Dictionary<TKey, TValue>();
            dictStringTValue = new Dictionary<string, TValue>();

            // Заполнение коллекций
            for (int i = 0; i < count; i++)
            {
                var element = generateElement(i);
                listTKey.Add(element.Key);
                listString.Add(element.Key.ToString());
                dictTKeyTValue.Add(element.Key, element.Value);
                dictStringTValue.Add(element.Key.ToString(), element.Value);
            }
        }

        // Метод для измерения времени выполнения
        private void MeasureTime(Action action, string description)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            action();
            watch.Stop();
            Console.WriteLine($"{description}: {watch.ElapsedTicks} тиков");
        }

        // Метод для измерения времени поиска
        public void MeasureSearchTime()
        {
            // Элементы для поиска: первый, центральный, последний, отсутствующий
            int[] indices = { 0, listTKey.Count / 2, listTKey.Count - 1, listTKey.Count };

            foreach (int index in indices)
            {
                TKey keyToSearch;
                string stringKeyToSearch;
                TValue valueToSearch;
                //Если индекс находится в пределах коллекции, используются существующие ключи и значения
                //Если индекс выходит за пределы коллекции, генерируется элемент с помощью делегата generateElement.
                if (index < listTKey.Count)
                {
                    keyToSearch = listTKey[index];
                    stringKeyToSearch = listString[index];
                    valueToSearch = dictTKeyTValue[keyToSearch];
                }
                else
                {
                    // Генерация отсутствующего элемента
                    var element = generateElement(index);
                    keyToSearch = element.Key;
                    stringKeyToSearch = element.Key.ToString();
                    valueToSearch = element.Value;
                }

                // Поиск в List<TKey>
                MeasureTime(() => listTKey.Contains(keyToSearch), $"List<TKey> поиск элемента {keyToSearch}");

                // Поиск в List<string>
                MeasureTime(() => listString.Contains(stringKeyToSearch), $"List<string> поиск элемента {stringKeyToSearch}");

                // Поиск по ключу в Dictionary<TKey, TValue>
                MeasureTime(() => dictTKeyTValue.ContainsKey(keyToSearch), $"Dictionary<TKey, TValue> поиск по ключу {keyToSearch}");

                // Поиск по ключу в Dictionary<string, TValue>
                MeasureTime(() => dictStringTValue.ContainsKey(stringKeyToSearch), $"Dictionary<string, TValue> поиск по ключу {stringKeyToSearch}");

                // Поиск по значению в Dictionary<TKey, TValue>
                MeasureTime(() => dictTKeyTValue.ContainsValue(valueToSearch), $"Dictionary<TKey, TValue> поиск по значению {valueToSearch}");
            }
        }

        
    }

}

