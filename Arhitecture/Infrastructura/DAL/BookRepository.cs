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
    public class BookRepository : IRepository<Book>
    {

        ApplicationDbContext db;

        public BookRepository(ApplicationDbContext db)
        {
            db = new ApplicationDbContext();
        }

        public void Create(Book book)
        {
            db.Books.Add(book);
        }

        public void Delate(int id)
        {
            var book = db.Books.Find(id);
            if (book != null)
                db.Books.Remove(book);
        }
        public IEnumerable<Book> GetAllItems()
        {
            return db.Books.ToList();
        }

        public Book GetItem(int id)
        {
            return db.Books.Find(id);
        }
        public void Update(Book book)
        {
            db.Entry(book).State = System.Data.Entity.EntityState.Modified;
        }

        public static implicit operator Lazy<object>(BookRepository v)
        {
            throw new NotImplementedException();
        }
    }
}
