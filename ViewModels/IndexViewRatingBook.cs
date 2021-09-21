using BookReview.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookReview.Arhitecture.Domain.Models.RatingViews
{
    public class IndexViewRatingBook
    {
        public PageInfo PageInfo { get; set; }
        public IEnumerable<Book> Books { get; set; }
    }
}