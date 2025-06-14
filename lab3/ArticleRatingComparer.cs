using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    public class ArticleRatingComparer : IComparer<Article>
    {
        public int Compare(Article x, Article y)
        {
            if (x == null && y == null)
                return 0; // Оба объекта null, считаются равными.
            if (x == null)
                return -1; // Если x null, а y нет, x считается меньше.
            if (y == null)
                return 1; // Если y null, а x нет, y считается меньше.

            return x.Rating.CompareTo(y.Rating);
        }
    }
}
