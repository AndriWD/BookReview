using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookReview.Domain.Models;
using BookReview.Models;
using BookReview.Domain.Interfaces;
using BookReview.Infrastructure.DAL;
using System.Threading;
using BookReview.Infrastructura.Bussines.Expensive_Method;
using BookReview.Arhitecture.Domain.Models;

namespace BookReview.Controllers
{
    /// <summary>
    /// Контролер Рецензій
    /// </summary>
    public class ReviewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        List<Review> Reviews;
        /// <summary>
        /// Конструктор
        /// </summary>
        public ReviewsController()
        {
            Reviews = db.Reviews.ToList();
        }

        // GET: Reviews
        public async Task<ActionResult> Index(int page = 1)
        {
            //показуємо новіші записи на перших сторінках
            IEnumerable<Review> reverseReview = Reviews.OrderByDescending(r => r);
            int pageSize = 10;
            IEnumerable<Review> reviewsPerPage = reverseReview.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo(page, pageSize, Reviews.Count);
            IndexViewReview reviewView = new IndexViewReview() { PageInfo = pageInfo, Reviews = Reviews };
            return View(reviewView);  
        }

        

        // GET: Reviews/Create
        /// <summary>
        /// Створити рецензію
        /// </summary>
        /// <returns>Нова рецензія</returns>
        public ActionResult Create()
        {
           
            return View();
        }

        
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Додати нову рецензію до БД
        /// </summary>
        /// <param name="review">Рецензія</param>
        /// <param name="NameOfBook">Назва книги</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Review review, string NameOfBook)
        {
            
                //перевірка наявністі книги
                if (db.Books.Where(b => b.NameOfBook.ToLower() == NameOfBook.ToLower()) == null)
                {
                    HttpContext.Response.Write("Книги, про яку ви пишете рецензію ще немає в нашій базі даних, допоможіть розвинути наш проект з зроблення людей трішки щасливішими;) добавте опис книги до нашої бази даних");
                    Thread.Sleep(4000);
                    RedirectToAction("Create", "Books");
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                    review.Book = (Book)db.Books.FirstOrDefault(b => b.NameOfBook.ToLower() == NameOfBook.ToLower());

                    review.Book.Reviews.Add(review);
                    db.Entry(review.Book).State = EntityState.Modified;

                    //задумка, що рецензії дають в три рази більше голосів чим лайки
                    review.Vote(review.IsPositive);
                    review.Vote(review.IsPositive);
                    review.Vote(review.IsPositive);


                    db.Reviews.Add(review);
                    await db.SaveChangesAsync();
                    HttpContext.Response.Write("Ваша рецензія додана, дякуємо, що ви з нами");
                    return RedirectToAction("Index");
                    } 
                }
            return View(review);
        }

        //// POST: Reviews/Edit/5
        //// Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        //// сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "ReviewId,TextOfReview,BookId,Vote,VoteCounter")] Review review)
        //{
        //    var currentUser = HttpContext.User;
        //    //додаємо перевірку чи то той самий користувач, що створював це || або адмін
        //    if ((review.AppUser.UserName != currentUser.Identity.Name) || (currentUser.IsInRole("admin")))
        //    {
        //        return View("Index"); //зашлушка
        //        //TODO знайти якийсь метод такий щоб помилку користувачу повертало, типу ти того не робив куда ручки пхаєш
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(review).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.BookId = new SelectList(db.Books, "BookId", "NameOfBook", review.BookId);
        //    return View(review);
        //}

        //// GET: Reviews/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{

        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Review review = await db.Reviews.FindAsync(id);
        //    if (review == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    var currentUser = HttpContext.User;
        //    //перевірка, рецензію може видалити лише або адмін або користувач, що створив її
        //    if ((review.AppUser.UserName != currentUser.Identity.Name) || (currentUser.IsInRole("admin")))
        //    {
        //        return View("Index");
        //        //TODO знайти якийсь метод такий щоб помилку користувачу повертало, що він немає доступу до цього методу
        //    }
        //    return View(review);
        //}

        //// POST: Reviews/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    Review review = await db.Reviews.FindAsync(id);
        //    db.Reviews.Remove(review);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
