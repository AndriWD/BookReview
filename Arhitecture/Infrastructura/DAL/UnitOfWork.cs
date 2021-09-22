using BookReview.Arhitecture.Domain.Interfaces;
using BookReview.Infrastructure.DAL;
using BookReview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookReview.Arhitecture.Infrastructura.DAL
{
    /// <summary>
    /// Реалізація паттерну UnitOfWork для обєднання дани всіх репозиторіїв
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //реалізовано через використання паттерну Singelton        
        private Lazy<BookRepository> bookRepository;
        private Lazy<ReviewRepository> reviewRepository;
        private Lazy<AuthorRepository> authorRepository;

        //публічні свойство для роботи з екзмеплярами репозиторіїв
        /// <summary>
        /// Екземпляр репозиторія книг
        /// </summary>
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
        /// <summary>
        /// Екземпляр репозиторія рецензій
        /// </summary>
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
        /// <summary>
        /// Екземпляр репозиторія авторів
        /// </summary>
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
        /// <summary>
        /// Зберегти зміни в БД
        /// </summary>
        public void Save()
        {
            db.SaveChanges();
        }
        private bool disposed = false; 
        /// <summary>
        /// Очищення виділеної пам'яті під екземпляр доступу до БД
        /// </summary>
        /// <param name="disposing"></param>
        public virtual void Dispose(bool disposing)
        {
            if (this.disposed != true)
            {
                if (disposing == true)
                    db.Dispose();
                this.disposed = true;
            }
        }
        /// <summary>
        /// Звільнення базового фіналізатора
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}