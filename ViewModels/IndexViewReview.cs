using BookReview.Arhitecture.Domain.Models;
using System.Collections.Generic;

namespace BookReview.Domain.Models
{
    public class IndexViewReview
    {
        public IEnumerable<Review> Reviews { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
