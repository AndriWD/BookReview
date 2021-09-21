using BookReview.Domain.Interfaces;
using BookReview.Domain.Models;
using BookReview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReview.Infrastructure.DAL
{
    public class ReviewRepository : IRepository<Review>
    {

        ApplicationDbContext db;
        public ReviewRepository(ApplicationDbContext db)
        {
            db = new ApplicationDbContext();
        }
        public void Create(Review review)
        {
            db.Reviews.Add(review);
        }
        public void Delate(int id)
        {
            var review = db.Reviews.Find(id);
            if (review != null)
                db.Reviews.Remove(review);

        }
        public IEnumerable<Review> GetAllItems()
        {
            return db.Reviews.ToList();
        }
        public Review GetItem(int id)
        {
            return db.Reviews.Find(id);
        }
        public void Update(Review review)
        {
            db.Entry(review).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
