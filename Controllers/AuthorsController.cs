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
    public class AuthorsController : Controller
    {
        ///функція Create Get + Post
        private ApplicationDbContext db = new ApplicationDbContext();
        List<Author> Authors;
        public AuthorsController()
        {
            Authors = db.Authors.ToList();
        }

        // GET: Authors
        public async Task<ActionResult> Index(int page = 1)
        {
            //ща ми пропишемо, щоб поверталася конкретна кількість авторів, книжок і так далі
            int pageSize = 10; //скільки обєктів буде на сторінці(авторів)
            IEnumerable<Author> authorPerPages = Authors.Skip((page - 1) * pageSize).Take(pageSize); // отримує ті 10 сторінок, які потрібно вивести
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = Authors.Count };
            IndexViewAuthor authorView = new IndexViewAuthor() { Authors = authorPerPages, PageInfo = pageInfo };

            return View(authorView);
        }

        // GET: Authors/Details/5
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
            //нам потрібно разом з автором показати ще книжки тому тут також буде зроблена пагінація інтересненько правда?
            IEnumerable<Book> authorsBooks = db.Books.Where(b => b.AuthorId == id);
            if (authorsBooks == null)
            {
                return HttpNotFound();
            }

            //да ми передаємо вигляд моделі книги, але це щоб не створювати нової лишньої сущності 
            int pageSize = 10;
            IEnumerable<Book> BooksPerPage = authorsBooks.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo() { PageNumber = page, PageSize = pageSize, TotalItems = authorsBooks.Count() };
            DetailsViewAuthorsAndBooks authorBookView = new DetailsViewAuthorsAndBooks() { Books = BooksPerPage, PageInfo = pageInfo, Author = author };

            return View(authorBookView);
        }

        // GET: Authors/Create
        public ActionResult Create()
        {
            ViewBag.Genre = Enum.GetNames(typeof(Genre));
            return View();
        }

        // POST: Authors/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Author author, List<string> ListOfGenre)
        {
            if (db.Authors.FirstOrDefault(a => a.Surname.ToLower() == author.Surname.ToLower()) != null)
            {
                HttpContext.Response.Write("<h2>Даний автор вже є в нашій базі даних, добавте йому книжку</h2>");
                Thread.Sleep(4000);
                RedirectToAction("Create", "Books");
            }
            else if (ModelState.IsValid)
            {
                author.Genre = ListOfGenre;

                db.Authors.Add(author);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(author);
        }

        // GET: Authors/Edit/5
        public async Task<ActionResult> Edit(int? id)
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
            return View(author);
        }

        // POST: Authors/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AuthorId,Name,SurName,Vote,VoteCounter,PathToImage,AboutOfAuthor")] Author author)
        {
            if (ModelState.IsValid)
            {
                db.Entry(author).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(author);
        }

        // GET: Authors/Delete/5
        public async Task<ActionResult> Delete(int? id)
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
            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Author author = await db.Authors.FindAsync(id);
            db.Authors.Remove(author);
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