using BookReview.Arhitecture.Domain.Models;
using System.Collections.Generic;

namespace BookReview.Domain.Models
{
    public class DetailsViewBooksAndReviews
    {
        public Book Book { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
