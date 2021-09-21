using BookReview.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using BookReview.Domain.Models;
using BookReview.Models;

namespace BookReview.Infrastructure.DAL
{
    public class AuthorRepository : IRepository<Author>
    {

        ApplicationDbContext db;
        public AuthorRepository(ApplicationDbContext db)
        {
            db = new ApplicationDbContext();
        }

        public void Create(Author authors)
        {
            db.Authors.Add(authors);
        }

        public void Delate(int id)
        {
            var author = db.Authors.Find(id);
            if (author != null)
                db.Authors.Remove(author);
        }
        public IEnumerable<Author> GetAllItems()
        {

            return db.Authors.ToList();
        }

        public Author GetItem(int id)
        {
            return db.Authors.Find(id);
        }

        public void Update(Author authors)
        {
            db.Entry(authors).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
