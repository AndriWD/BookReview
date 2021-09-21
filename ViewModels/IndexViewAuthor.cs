using BookReview.Arhitecture.Domain.Models;
using System.Collections.Generic;

namespace BookReview.Domain.Models
{
    public class IndexViewAuthor
    {
        public IEnumerable<Author> Authors { get; set; }
        public PageInfo PageInfo { get; set; }
    }

}
