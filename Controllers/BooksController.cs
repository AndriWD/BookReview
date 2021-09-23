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
using System.Threading;
using BookReview.Arhitecture.Domain.Models;

namespace BookReview.Controllers
{
    /// <summary>
    /// Контролер Книг
    /// </summary>
    public class BooksController : Controller
    {
        ///функція Create Get + Post
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        private readonly List<Book> Books;
        /// <summary>
        /// Конструктор
        /// </summary>
        public BooksController()
        {
            Books = db.Books.ToList();
        }

        // GET: Books
        /// <summary>
        /// Початкова сторінка, Показує всі нові книги
        /// </summary>
        /// <param name="page">Сторінка</param>
        /// <returns>Всі книги в зворотньому порядку</returns>
        public async Task<ActionResult> Index(int page = 1)
        {
            IEnumerable<Book> reverseBooks = Books.OrderByDescending(b => b);
            int pageSize = 10;
            IEnumerable<Book> booksPerPages = reverseBooks.Skip((page - 1) * pageSize).Take(pageSize); // отримує ті 10 сторінок, які потрібно вивести
            PageInfo pageInfo = new PageInfo(page, pageSize, Books.Count);
            IndexViewBook bookView = new IndexViewBook() { Books = booksPerPages, PageInfo = pageInfo };

            return View(bookView);
        }

        // GET: Books/Details/5
        /// <summary>
        /// Отримати книгу за ідентифікатором
        /// </summary>
        /// <param name="id">Ідентифікатор</param>
        /// <param name="page">Сторінка</param>
        /// <returns>Конкретна книга</returns>
        public async Task<ActionResult> Details(int? id, int page = 1)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }

            var reviews = db.Reviews.Where(r => r.BookId == id);
            int pageSize = 5;
            IEnumerable<Review> reviewsPerPage = reviews.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo(page, pageSize, reviews.Count());
            DetailsViewBooksAndReviews viewBookReview = new DetailsViewBooksAndReviews() { Book = book, PageInfo = pageInfo, Reviews = reviews };


            return View(viewBookReview);
        }

        // GET: Books/Create
        /// <summary>
        /// Створити книгу (GET)
        /// </summary>
        /// <returns>Форма створення книги</returns>
        public ActionResult Create()
        {
            ViewBag.Genres = db.Genres.ToList();
            
            return View();
        }

        // POST: Books/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Створити книгу (POST)
        /// </summary>
        /// <param name="book">Книга</param>
        /// <param name="SurnameOfAuthor">Прізвище автора</param>
        /// <param name="ListOfGenre">Жанри</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Book book, string SurnameOfAuthor, List<Genre> ListOfGenre)
        {
            if (ModelState.IsValid)
            {
                //далі нам треба перевірити чи є вже створений даний автор, а якщо немає, то прийдеться його створити, а файл, що нам надіслали видалити уж вибачте
                if (db.Authors.Where(a => a.Surname.ToLower() == SurnameOfAuthor.ToLower()) == null)
                {
                    //видалення картинки
                    DeleteImage(book);

                    HttpContext.Response.Write("Даного автора ще не добавлено до бази даних, будь ласка допоможіть розвинути наш проект з зроблення людей трішки щасливішими;) добавте відомості про автора до нашої бази даних");
                    
                    Thread.Sleep(4000);
                    RedirectToAction("Create", "Authors");

                }
                else
                {                   
                    book.Author = db.Authors.FirstOrDefault(a => a.Surname.ToLower() == SurnameOfAuthor.ToLower());                  
                    book.Author.Books.Add(book);
                    db.Entry(book).State = EntityState.Modified;
                    db.Entry(book.Author).State = EntityState.Modified;
                    //добавили вибрані 
                    book.Genres = ListOfGenre;
                    db.Books.Add(book);
                    await db.SaveChangesAsync();
                    HttpContext.Response.Write("Книга успішно створена");
                    return RedirectToAction("Index");
                }    
            }
            return View(book);
        }

        private static void DeleteImage(Book book)
        {
            if(book.PathToImage == null)
            {
                return;
            }
            try
            {
                System.IO.File.Delete(book.PathToImage);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine("Неправильний путь до файла " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Невідома помилка" + ex.Message);
            }
        }

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
