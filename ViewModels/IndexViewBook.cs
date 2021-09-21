using BookReview.Arhitecture.Domain.Models;
using System.Collections.Generic;

namespace BookReview.Domain.Models
{
    public class IndexViewBook
    {
        public IEnumerable<Book> Books { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
