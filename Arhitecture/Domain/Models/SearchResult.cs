using BookReview.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookReview.Arhitecture.Domain.Models
{
    public class SearchResult
    {
        public IEnumerable<Author> Authors { get; set; }
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
    }
}