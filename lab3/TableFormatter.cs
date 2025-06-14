using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    public static class TableFormatter
    {
        public static string FormatAsTable<T>(
            IEnumerable<T> items,
            params (string Header, Func<T, object> Selector)[] columns)
        {
            if (items == null || !items.Any() || columns == null || columns.Length == 0)
                return string.Empty;

            // Получаем все строки данных
            object[][] dataRows = items.Select(item =>
                columns.Select(col => col.Selector(item)).ToArray()
            ).ToArray();

            // Вычисляем ширину каждой колонки
            int[] columnWidths = columns.Select((col, i) =>
                Math.Max(
                    col.Header.Length,
                    dataRows.Select(row => row[i]?.ToString()?.Length ?? 0).DefaultIfEmpty(0).Max()
                )
            ).ToArray();

            // Строим горизонтальную линию
            string horizontalLine = "+" + string.Join("+",
                columnWidths.Select(w => new string('-', w + 2))) + "+";

            // Формируем таблицу
            StringBuilder sb = new StringBuilder();

            // Заголовок
            sb.AppendLine(horizontalLine);
            sb.AppendLine("| " + string.Join(" | ",
                columns.Select((col, i) => col.Header.PadRight(columnWidths[i]))) + " |");
            sb.AppendLine(horizontalLine);

            // Данные
            foreach (var row in dataRows)
            {
                sb.AppendLine("| " + string.Join(" | ",
                    row.Select((val, i) => (val?.ToString() ?? "").PadRight(columnWidths[i]))) + " |");
            }

            sb.AppendLine(horizontalLine);

            return sb.ToString();
        }
    }
}
