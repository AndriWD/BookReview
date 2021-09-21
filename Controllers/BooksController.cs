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
    public class BooksController : Controller
    {
        ///функція Create Get + Post
        private ApplicationDbContext db = new ApplicationDbContext();
        List<Book> Books;
        public BooksController()
        {
            Books = db.Books.ToList();
        }

        // GET: Books
        public async Task<ActionResult> Index(int page = 1)
        {
            IEnumerable<Book> reverseBooks = Books.OrderByDescending(b => b);
            int pageSize = 10;
            IEnumerable<Book> booksPerPages = reverseBooks.Skip((page - 1) * pageSize).Take(pageSize); // отримує ті 10 сторінок, які потрібно вивести
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = Books.Count };
            IndexViewBook bookView = new IndexViewBook() { Books = booksPerPages, PageInfo = pageInfo };

            return View(bookView);
        }

        // GET: Books/Details/5
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
            PageInfo pageInfo = new PageInfo() { PageNumber = page, PageSize = pageSize, TotalItems = reviews.Count() };
            DetailsViewBooksAndReviews viewBookReview = new DetailsViewBooksAndReviews() { Book = book, PageInfo = pageInfo, Reviews = reviews };


            return View(viewBookReview);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            ViewBag.Genre = Enum.GetNames(typeof(Genre));
            
            return View();
        }

        // POST: Books/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Book book, string SurnameOfAuthor, List<string> ListOfGenre)
        {
            if (ModelState.IsValid)
            {
                //далі нам треба перевірити чи є вже створений даний автор, а якщо немає, то прийдеться його створити, а файл, що нам надіслали видалити уж вибачте
                if (db.Authors.Where(a => a.Surname.ToLower() == SurnameOfAuthor.ToLower()) == null)
                {
                    //видалення картинки
                    DelateIMage(book);

                    HttpContext.Response.Write("<h2>Даного автора ще не добавлено до бази даних, будь ласка допоможіть розвинути наш проект з зроблення людей трішки щасливішими;) добавте відомості про автора до нашої бази даних </h2>");
                    
                    Thread.Sleep(4000);
                    RedirectToAction("Create", "Authors");

                }
                else
                {
                    //я забув добавити автора
                    book.Author = db.Authors.FirstOrDefault(a => a.Surname.ToLower() == SurnameOfAuthor.ToLower());
                    //але нам ще тепер треба всановити силку автору на книгу
                    book.Author.Books.Add(book);
                    db.Entry(book.Author).State = EntityState.Modified;
                    await db.SaveChangesAsync();

                    //добавили вибрані 
                    book.Genre = ListOfGenre;

                    db.Books.Add(book);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }    
            }
            return View(book);
        }

        private static void DelateIMage(Book book)
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
                Console.WriteLine("Неідома помилка" + ex.Message);
            }
        }

        // GET: Books/Edit/5
        public async Task<ActionResult> Edit(int? id)
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
            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "Name", book.AuthorId);
            return View(book);
        }

        // POST: Books/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "BookId,NameOfBook,AuthorId,Vote,VoteCounter,PathToImage")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "Name", book.AuthorId);
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<ActionResult> Delete(int? id)
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
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Book book = await db.Books.FindAsync(id);
            db.Books.Remove(book);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
