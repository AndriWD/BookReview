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
using BookReview.Arhitecture.Domain.Models;
using System.Threading;

namespace BookReview.Controllers
{
    /// <summary>
    /// Контролер Авторів
    /// </summary>
    public class AuthorsController : Controller
    {
        ///функція Create Get + Post
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        private readonly List<Author> Authors;
        /// <summary>
        /// Конструктор
        /// </summary>
        public AuthorsController()
        {
            Authors = db.Authors.ToList();
        }

        // GET: Authors
        /// <summary>
        /// Сторінка перегляду авторів
        /// </summary>
        /// <param name="page">Необов'язковий параметр - Сторінка</param>
        /// <returns>Сторінку з авторами</returns>
        public async Task<ActionResult> Index(int page = 1)
        {
            
            int pageSize = 10; 
            IEnumerable<Author> authorPerPages = Authors.Skip((page - 1) * pageSize).Take(pageSize); // отримує ті 10 сторінок, які потрібно вивести
            PageInfo pageInfo = new PageInfo(page, pageSize, Authors.Count);
            IndexViewAuthor authorView = new IndexViewAuthor() { Authors = authorPerPages, PageInfo = pageInfo };

            return View(authorView);
        }

        // GET: Authors/Details/5
        /// <summary>
        /// Інформація про автора за ідентифікатором
        /// </summary>
        /// <param name="id">Ідентифікатор</param>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<ActionResult> Details(int? id, int page = 1)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = await db.Authors.FindAsync(id);
            if (author == null)
            {
                return HttpNotFound();
            }          
            IEnumerable<Book> authorsBooks = db.Books.Where(b => b.AuthorId == id);
            if (authorsBooks == null)
            {
                return HttpNotFound();
            }

           
            int pageSize = 10;
            IEnumerable<Book> BooksPerPage = authorsBooks.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo(page, pageSize, authorsBooks.Count());
            DetailsViewAuthorsAndBooks authorBookView = new DetailsViewAuthorsAndBooks() { Books = BooksPerPage, PageInfo = pageInfo, Author = author };

            return View(authorBookView);
        }

        // GET: Authors/Create
        /// <summary>
        /// Створення автора (GET)
        /// </summary>
        /// <returns>Автор</returns>
        public ActionResult Create()
        {
            ViewBag.Genres = db.Genres.ToList();
            return View();
        }

        // POST: Authors/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Створення автора (POST)
        /// </summary>
        /// <param name="author">Автора</param>
        /// <param name="ListOfGenre">Жанри</param>
        /// <returns>Додавання автора до БД</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Author author, List<Genre> ListOfGenre)
        {
            if (db.Authors.FirstOrDefault(a => a.Surname.ToLower() == author.Surname.ToLower()) != null)
            {
                HttpContext.Response.Write("Даний автор вже є в нашій базі даних, добавте йому книгу");
                Thread.Sleep(4000);
                RedirectToAction("Create", "Books");
            }
            else if (ModelState.IsValid)
            {
                author.Genres = ListOfGenre;

                db.Authors.Add(author);
                await db.SaveChangesAsync();
                HttpContext.Response.Write("Автор успішно створений, добавте йому першу книгу");
                return RedirectToAction("Create", "Books");
            }

            return View(author);
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