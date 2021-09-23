using BookReview.Arhitecture.Domain.Models;
using BookReview.Arhitecture.Domain.Models.RatingViews;
using BookReview.Domain.Models;
using BookReview.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BookReview.Controllers
{
    /// <summary>
    /// Контроль рейтингів
    /// </summary>
    public class RatingsController : Controller, IDisposable
    {
        ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// Сторінка рейтингів
        /// </summary>
        /// <returns></returns>
        // GET: Ratings
        public  ActionResult Index()
        {

            return View();
        }
        //Всі методи працюють за допомогою аяксу
        /// <summary>
        /// Отримати авторів за рейтингами
        /// </summary>
        /// <param name="page">Сторінка</param>
        /// <returns>Відсортовані по рейтингу автори</returns>
        [HttpPost]
        public async Task<ActionResult> RatingAuthor(int page = 1)
        {
           
                List<Author> authorsSorted = await db.Authors.OrderByDescending(a => a.RaitingInPersent).ToListAsync();
                if (authorsSorted.Count <= 0)
                {
                    return HttpNotFound();
                }
            //створюємо пагінацію
            int pageSize = page;
            IEnumerable<Author> authorsSortedPerPage = authorsSorted.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo(page, pageSize, authorsSorted.Count);
            IndexViewRatingAuthor viewRatingAuthor = new IndexViewRatingAuthor() { PageInfo = pageInfo, Authors = authorsSorted };

                return PartialView(viewRatingAuthor);

        }
        /// <summary>
        /// Отримати книги ща рейтингом
        /// </summary>
        /// <param name="page">Сторінка</param>
        /// <returns>Відсортовані по рейтингу книги</returns>
        [HttpPost]
        public async Task<ActionResult> RatingBook(int page = 1)
        {
           
                List<Book> booksSorted = await db.Books.OrderByDescending(a => a.RaitingInPersent).ToListAsync();
                if (booksSorted.Count <= 0)
                {
                    return HttpNotFound();
                }

            //пагінація
            int pageSize = 10;
            IEnumerable<Book> bookSortedPerPage = booksSorted.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo(page, pageSize, booksSorted.Count);
            IndexViewRatingBook viewRatingBook = new IndexViewRatingBook() { PageInfo = pageInfo, Books = bookSortedPerPage };
                return PartialView(viewRatingBook);
        }
        /// <summary>
        /// Отримати рецензії ха рейтингом
        /// </summary>
        /// <param name="page">Сторінка</param>
        /// <returns>Відсортовані за рейтингом рецензії</returns>
        [HttpPost]
        public async Task<ActionResult> RatingReview(int page = 1)
        {

            List<Review> reviewsSorted = await db.Reviews.OrderByDescending(a => a.Vote).ToListAsync();
            if (reviewsSorted.Count <= 0)
            {
                return HttpNotFound();
            }

            //пагінація
            int pageSize = 10;
            IEnumerable<Review> reviewSortedPerPage = reviewsSorted.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo() { PageNumber = page, PageSize = 10, TotalItems = reviewsSorted.Count };
            IndexViewRatingReview viewRatingReview = new IndexViewRatingReview() { PageInfo = pageInfo, Reviews=reviewSortedPerPage};
            return PartialView(viewRatingReview);
        }



    }
}
