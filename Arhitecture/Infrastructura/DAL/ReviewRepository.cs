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
    /// Репозиторій рецензій
    /// </summary>
    public class ReviewRepository : IRepository<Review>
    {

        private ApplicationDbContext db;
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="db">Екземпляр доступу до БД</param>
        public ReviewRepository(ApplicationDbContext db)
        {
            db = new ApplicationDbContext();
        }
        /// <summary>
        /// Добавити рецензію до БД
        /// </summary>
        /// <param name="review">Рецензія</param>
        public void Create(Review review)
        {
            db.Reviews.Add(review);
        }
        /// <summary>
        /// Видалити рецензію з БД
        /// </summary>
        /// <param name="id">Ідентифікатор</param>
        public void Delate(int id)
        {
            var review = db.Reviews.Find(id);
            if (review != null)
                db.Reviews.Remove(review);

        }
        /// <summary>
        /// Отримати всі Рецензії
        /// </summary>
        /// <returns>Всі рецензії</returns>
        public IEnumerable<Review> GetAllItems()
        {
            return db.Reviews.ToList();
        }
        /// <summary>
        /// Отримати рецензію за ідентифікатором
        /// </summary>
        /// <param name="id">Ідентифікатор</param>
        /// <returns>Рецензія</returns>
        public Review GetItem(int id)
        {
            return db.Reviews.Find(id);
        }
        /// <summary>
        /// Оновити Рецензію в БД
        /// </summary>
        /// <param name="review"></param>
        public void Update(Review review)
        {
            db.Entry(review).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
