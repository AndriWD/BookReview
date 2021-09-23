using BookReview.Arhitecture.Domain.Models;
using BookReview.Domain.Models;
using BookReview.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BookReview.Controllers
{
    public class SearchController : Controller, IDisposable
    {
        ApplicationDbContext db;
        public SearchController()
        {
            db = new ApplicationDbContext();
        }
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> Search(string searchQuery)
        {
            if (searchQuery == null || searchQuery =="")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //а це прикол шукати серед двох полів
            var authorsSearchResult = await db.Authors.Where(a => $"{a.Name} {a.Surname}".ToLower().Contains(searchQuery.ToLower())).Take(10).ToListAsync();
            var booksSearchResult = await db.Books.Where(b => b.NameOfBook.ToLower().Contains(searchQuery.ToLower())).Take(10).ToListAsync();
            var reviewsSearchResult = await db.Reviews.Where(r => r.Book.NameOfBook.ToLower().Contains(searchQuery.ToLower())).Take(3).ToListAsync();
            if(authorsSearchResult == null && booksSearchResult == null && reviewsSearchResult == null)
            {
                return HttpNotFound();
            }

            SearchResult searchResult = new SearchResult() { Authors = authorsSearchResult, Books = booksSearchResult };
            return PartialView(searchResult);


        }
        [HttpGet]
        public ActionResult AdvancedFilter()
        {
            ViewBag.Genres = db.Genres;
            return View();
        }
        [HttpPost]
        public void AdvancedFilter(string type, string searchQuery, string genre, int page = 1)
        {
            ViewBag.genre = genre;
            ViewBag.searchQuery = searchQuery;
            if(type == "author")
            {
               SearchAuthor(searchQuery, genre, page);
            }
            else if(type == "book")
            {
                SearchBook(searchQuery, genre, page);
            }
            else if(type == "review")
            {
                SearchReview(searchQuery, genre, page);
            }
            else
            {
                HttpContext.Response.Write("Виберіть тип пошукового запиту");
            }
        }
        private async Task<ActionResult> SearchAuthor(string searchQuery, string genre, int page)
        {
            List<Author> resultFiltersOfAuthors;
            if((searchQuery == null || searchQuery =="") && (genre == null || genre == ""))
            {
                resultFiltersOfAuthors = await db.Authors.ToListAsync();
                IndexViewAuthor viewResultFilterOfAuthor = CreateIndexViewAuthor(page, resultFiltersOfAuthors);
                return PartialView(viewResultFilterOfAuthor);
            }
            else if(genre == null || genre == "")
            {
                resultFiltersOfAuthors = await db.Authors.Where(a => $"{a.Name} {a.Surname}".ToLower().Contains(searchQuery.ToLower())).ToListAsync();
                IndexViewAuthor viewResultFilterOfAuthor = CreateIndexViewAuthor(page, resultFiltersOfAuthors);
                return PartialView(viewResultFilterOfAuthor);
            }
            else if(searchQuery == null || searchQuery == "")
            {
                var currentGenre = await db.Genres.FirstOrDefaultAsync(g => g.Name.Equals(genre, StringComparison.OrdinalIgnoreCase));
                resultFiltersOfAuthors = (List<Author>)currentGenre.Authors;
                IndexViewAuthor viewResultFilterOfAuthor = CreateIndexViewAuthor(page, resultFiltersOfAuthors);
                return PartialView(viewResultFilterOfAuthor);
            }
            else
            {
                var currentGenre = await db.Genres.FirstOrDefaultAsync(g => g.Name.Equals(genre, StringComparison.OrdinalIgnoreCase));
                resultFiltersOfAuthors = (List<Author>)currentGenre.Authors.Where(a => $"{a.Name} {a.Surname}".ToLower().Contains(searchQuery.ToLower()));                           
                IndexViewAuthor viewResultFilterOfAuthor = CreateIndexViewAuthor(page, resultFiltersOfAuthors);
                return PartialView(viewResultFilterOfAuthor);
            }
        }
        private async Task<ActionResult> SearchBook(string searchQuery, string genre, int page)
        {
            List<Book> resultFiltersOfBook;
            if ((searchQuery == null || searchQuery == "") && (genre == null || genre == ""))
            {
                resultFiltersOfBook = await db.Books.ToListAsync();
                IndexViewBook viewResultFilterOfBook = CreateIndexViewBook(page, resultFiltersOfBook);
                return PartialView(viewResultFilterOfBook);
            }
            else if (genre == null || genre == "")
            {
                resultFiltersOfBook = await db.Books.Where(b => b.NameOfBook.ToLower().Contains(searchQuery.ToLower())).ToListAsync();
                IndexViewBook viewResultFilterOfBook = CreateIndexViewBook(page, resultFiltersOfBook);
                return PartialView(viewResultFilterOfBook);
            }
            else if (searchQuery == null || searchQuery == "")
            {
                var currentGenre = await db.Genres.FirstOrDefaultAsync(g => g.Name.Equals(genre, StringComparison.OrdinalIgnoreCase));
                resultFiltersOfBook = (List<Book>)currentGenre.Books;
                IndexViewBook viewResultFilterOfBook = CreateIndexViewBook(page, resultFiltersOfBook);
                return PartialView(viewResultFilterOfBook);
            }
            else
            {
                var currentGenre = await db.Genres.FirstOrDefaultAsync(g => g.Name.Equals(genre, StringComparison.OrdinalIgnoreCase));
                resultFiltersOfBook = (List<Book>)currentGenre.Books.Where(b => b.NameOfBook.ToLower().Contains(searchQuery.ToLower()));
                IndexViewBook viewResultFilterOfBook = CreateIndexViewBook(page, resultFiltersOfBook);
                return PartialView(viewResultFilterOfBook);
            }
        }
        private async Task<ActionResult> SearchReview(string searchQuery, string genre, int page)
        {
            List<Review> resultFiltersOfReviews;
            if ((searchQuery == null || searchQuery == "") && (genre == null || genre == ""))
            {
                resultFiltersOfReviews = await db.Reviews.ToListAsync();
                IndexViewReview viewResultFilterOfReview = CreateIndexViewReview(page, resultFiltersOfReviews);
                return PartialView(viewResultFilterOfReview);
            }
            else if (genre == null || genre == "")
            {
                resultFiltersOfReviews = await db.Reviews.Where(r => r.Book.NameOfBook.ToLower().Contains(searchQuery.ToLower())).ToListAsync();
                IndexViewReview viewResultFilterOfReview = CreateIndexViewReview(page, resultFiltersOfReviews);
                return PartialView(viewResultFilterOfReview);
            }
            else if (searchQuery == null || searchQuery == "")
            {
                var currentGenre = await db.Genres.FirstOrDefaultAsync(g => g.Name.Equals(genre, StringComparison.OrdinalIgnoreCase));
                resultFiltersOfReviews = (List<Review>)currentGenre.Books.Select(b => b.Reviews);
                IndexViewReview viewResultFilterOfReview = CreateIndexViewReview(page, resultFiltersOfReviews);
                return PartialView(viewResultFilterOfReview);
            }
            else
            {
                var currentGenre = await db.Genres.FirstOrDefaultAsync(g => g.Name.Equals(genre, StringComparison.OrdinalIgnoreCase));
                resultFiltersOfReviews = (List<Review>)currentGenre.Books.Where(b => b.NameOfBook.ToLower().Contains(searchQuery.ToLower())).Select(b => b.Reviews);              
                IndexViewReview viewResultFilterOfReview = CreateIndexViewReview(page, resultFiltersOfReviews);
                return PartialView(viewResultFilterOfReview);
            }
        }

        private static IndexViewAuthor CreateIndexViewAuthor(int page, List<Author> resultFiltersOfAuthors)
        {
            int pageSize = 10;
            PageInfo pageInfo = new PageInfo(page, pageSize, resultFiltersOfAuthors.Count);
            var resultFiltersOfAuthorsPerPage = resultFiltersOfAuthors.Skip((page - 1) * pageSize).Take(pageSize);
            IndexViewAuthor viewResultFilterOfAuthor = new IndexViewAuthor() { PageInfo = pageInfo, Authors = resultFiltersOfAuthorsPerPage };
            return viewResultFilterOfAuthor;
        }
        private static IndexViewBook CreateIndexViewBook(int page, List<Book> resultFiltersOfBooks)
        {
            int pageSize = 10;
            PageInfo pageInfo = new PageInfo(page, pageSize, resultFiltersOfBooks.Count);
            var resultFiltersOfBooksPerPage = resultFiltersOfBooks.Skip((page - 1) * pageSize).Take(pageSize);
            IndexViewBook viewResultFilterOfBook = new IndexViewBook() { PageInfo = pageInfo, Books = resultFiltersOfBooksPerPage };
            return viewResultFilterOfBook;
        }
        private static IndexViewReview CreateIndexViewReview(int page, List<Review> resultFiltersOfReviews)
        {
            int pageSize = 10;
            PageInfo pageInfo = new PageInfo(page, pageSize, resultFiltersOfReviews.Count);
            var resultFiltersOfReviewsPerPage = resultFiltersOfReviews.Skip((page - 1) * pageSize).Take(pageSize);
            IndexViewReview viewResultFilterOfReviews = new IndexViewReview() { PageInfo = pageInfo, Reviews = resultFiltersOfReviewsPerPage };
            return viewResultFilterOfReviews;
        }
    }
}