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
    /// <summary>
    /// Автор
    /// </summary>
    public class Author
    {
        /// <summary>
        /// Ідентифікатор
        /// </summary>
        public int AuthorId { get; set; }
        /// <summary>
        /// Ім'я
        /// </summary>
        [Required]
        [MinLength(3, ErrorMessage ="Ім'я неможе бути менше трьох букв")]
        public string Name { get; set; }
        /// <summary>
        /// Прізвище
        /// </summary>
        [Required]
        [MinLength(3, ErrorMessage = "Прізвище неможе бути менше трьох букв")]
        public string Surname { get; set; }
        /// <summary>
        /// Книги
        /// </summary>
        public virtual ICollection<Book> Books { get; set; }

        //голосування
        /// <summary>
        /// Голоси
        /// </summary>
        public int Vote { get; set; }
        /// <summary>
        /// Кількість голосів
        /// </summary>
        public int VoteCounter { get; set; }
        /// <summary>
        /// Рейтинг
        /// </summary>
        [NotMapped]
        public double RaitingInPersent => (double)Vote / (double)VoteCounter;

        /// <summary>
        /// Жанри
        /// </summary>
        public virtual ICollection<Genre> Genres { get; set; }


        //додаткові властивості
        /// <summary>
        /// Картинка
        /// </summary>
        public string PathToImage { get; set; }
        /// <summary>
        /// Опис Автора
        /// </summary>
        [Column(TypeName = "Text")]
        [MaxLength(2000, ErrorMessage ="Опис автора не може бути більшим 2000 знаків")]
        public string AboutOfAuthor { get; set; }
        /// <summary>
        /// Стандартний конструктор
        /// </summary>
        public Author() { }
    }

}
