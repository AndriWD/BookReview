using BookReview.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookReview.Arhitecture.Domain.Models
{
    /// <summary>
    /// Модель представлення для пошуку автори/книги/рецензії
    /// </summary>
    public class SearchResult
    {
        /// <summary>
        /// Знайдені Автори
        /// </summary>
        public IEnumerable<Author> Authors { get; set; }
        /// <summary>
        /// Знйдені Книги
        /// </summary>
        public IEnumerable<Book> Books { get; set; }
        /// <summary>
        /// Знайдені Рецензії
        /// </summary>
        public IEnumerable<Review> Reviews { get; set; }
    }
}