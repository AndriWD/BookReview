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
    /// <summary>
    /// Репозиторій Книг
    /// </summary>
    public class BookRepository : IRepository<Book>
    {
        private ApplicationDbContext db;
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="db">Екземпляр доступу до БД</param>
        public BookRepository(ApplicationDbContext db)
        {
            db = new ApplicationDbContext();
        }
        /// <summary>
        /// Добавити Книгу до БД
        /// </summary>
        /// <param name="book">Книга</param>
        public void Create(Book book)
        {
            db.Books.Add(book);
        }
        /// <summary>
        /// Видалити книгу з БД
        /// </summary>
        /// <param name="id">Ідентифікатор</param>
        public void Delate(int id)
        {
            var book = db.Books.Find(id);
            if (book != null)
                db.Books.Remove(book);
        }
        /// <summary>
        /// Отримати всі книги
        /// </summary>
        /// <returns>Всі Книги</returns>
        public IEnumerable<Book> GetAllItems()
        {
            return db.Books.ToList();
        }
        /// <summary>
        /// Отримати книгу по ідентифікатору
        /// </summary>
        /// <param name="id">Ідентифікатор</param>
        /// <returns>Книга</returns>
        public Book GetItem(int id)
        {
            return db.Books.Find(id);
        }
        /// <summary>
        /// Оновити дані книги в БД
        /// </summary>
        /// <param name="book">Книга</param>
        public void Update(Book book)
        {
            db.Entry(book).State = System.Data.Entity.EntityState.Modified;
        }        
    }
}
