using BookReview.Arhitecture.Domain.Interfaces;
using BookReview.Infrastructure.DAL;
using BookReview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookReview.Arhitecture.Infrastructura.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //реалізовано Singelton
        private Lazy<BookRepository> bookRepository;
        private Lazy<ReviewRepository> reviewRepository;
        private Lazy<AuthorRepository> authorRepository;

        //створюємо public свойства для наших репозиторіїв 
        public BookRepository Books
        {
            get
            {
                if (bookRepository == null)
                {
                    bookRepository = new Lazy<BookRepository>(() => new BookRepository(db));
                    return bookRepository.Value;
                       
                }
                return bookRepository.Value;
            }
        }
        public ReviewRepository Reviews
        {
            get
            {
                if (reviewRepository == null)
                {
                    reviewRepository = new Lazy<ReviewRepository>(() => new ReviewRepository(db));
                    return reviewRepository.Value;
                }
                return reviewRepository.Value;
            }
        }
        public AuthorRepository Author
        {
            get
            {
                if (authorRepository == null)
                {
                    authorRepository = new Lazy<AuthorRepository>(() => new AuthorRepository(db));
                    return authorRepository.Value;
                }
                return authorRepository.Value;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }
        private bool disposed = false; 
        public virtual void Dispose(bool disposing)
        {
            if (this.disposed != true)
            {
                if (disposing == true)
                    db.Dispose();
                this.disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}