using BookReview.Arhitecture.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReview.Domain.Models
{
    public class Book
    {
        public int BookId { get; set; }
        [Required]
        public string NameOfBook { get; set; }

        //залежності
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }

        //жанр книги
        public List<string> Genre;


        //голосування, добавлення голосів буде реалізовано в контроллері
        public int Vote { get; set; }
        public int VoteCounter { get; set; }
        [NotMapped]
        public double RaitingInPersent => (double)Vote / (double)VoteCounter;

        //додаткове
        public string PathToImage { get; set; }
        [Column(TypeName = "Text")]
        [MaxLength(2000, ErrorMessage = "Опис книги не може бути більшим ніж 1000 знаків")]
        public string AboutOfBook { get; set; }
        public int CountOfPage { get; set; }
        public int Year { get; set; }

        //конструктор
        public Book()
        {
            Genre = new List<string>();
        }
    }
    public class IndexViewBook
    {
        public IEnumerable<Book> Books { get; set; }
        public PageInfo PageInfo { get; set; }
    }
    public class DetailsViewBooksAndReviews
    {
        public Book Book { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
