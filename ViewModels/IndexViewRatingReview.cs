using BookReview.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookReview.Arhitecture.Domain.Models.RatingViews
{
    public class IndexViewRatingReview
    {
        public PageInfo PageInfo { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
    }
}