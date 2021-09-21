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
    public class Author
    {
        public int AuthorId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        //голосування, добавлення голосів буде реалізовано в контроллері
        public int Vote { get; set; }
        public int VoteCounter { get; set; }
        public double RaitingInPersent => (double)Vote / (double)VoteCounter;

        //жанр книги
        public List<string> Genre;


        //додаткове
        public string PathToImage { get; set; }
        [Column(TypeName = "Text")]
        [MaxLength(2000, ErrorMessage ="Опис автора не може бути більшим ніж 2000 знаків")]
        public string AboutOfAuthor { get; set; }

        //конструктор
        public Author()
        {
            Genre = new List<string>();
        }
    }
    public class IndexViewAuthor
    {
        public IEnumerable<Author> Authors { get; set; }
        public PageInfo PageInfo { get; set; }
    }
    public class DetailsViewAuthorsAndBooks
    {
        public Author Author { get; set; }
        public IEnumerable<Book> Books { get; set; }
        public PageInfo PageInfo { get; set; }
    }

}
