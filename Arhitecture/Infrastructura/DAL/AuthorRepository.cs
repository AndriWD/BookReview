using BookReview.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using BookReview.Domain.Models;
using BookReview.Models;

namespace BookReview.Infrastructure.DAL
{
    /// <summary>
    /// Репозиторій Авторів
    /// </summary>
    public class AuthorRepository : IRepository<Author>
    {
        private ApplicationDbContext db;
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="db">Екземпляр доступу до БД</param>
        public AuthorRepository(ApplicationDbContext db)
        {
            db = new ApplicationDbContext();
        }
        /// <summary>
        /// Додавання автора до БД
        /// </summary>
        /// <param name="authors">Автор</param>
        public void Create(Author authors)
        {
            db.Authors.Add(authors);
        }
        /// <summary>
        /// Видалення автора з БД по ідентифікатору
        /// </summary>
        /// <param name="id">Ідентифікатор</param>
        public void Delate(int id)
        {
            var author = db.Authors.Find(id);
            if (author != null)
                db.Authors.Remove(author);
        }
        /// <summary>
        /// Отримати всіх авторів
        /// </summary>
        /// <returns>Всі Автори</returns>
        public IEnumerable<Author> GetAllItems()
        {

            return db.Authors.ToList();
        }
        /// <summary>
        /// Отримати автора за ідентифікатором
        /// </summary>
        /// <param name="id">Ідентифікатор</param>
        /// <returns>Автор</returns>
        public Author GetItem(int id)
        {
            return db.Authors.Find(id);
        }
        /// <summary>
        /// Оновити дані автора в БД
        /// </summary>
        /// <param name="authors">Автор</param>
        public void Update(Author authors)
        {
            db.Entry(authors).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
