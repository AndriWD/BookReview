using BookReview.Arhitecture.Domain.Models;
using System.Collections.Generic;

namespace BookReview.Domain.Models
{
    public class DetailsViewAuthorsAndBooks
    {
        public Author Author { get; set; }
        public IEnumerable<Book> Books { get; set; }
        public PageInfo PageInfo { get; set; }
    }

}
