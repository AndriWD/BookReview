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
    /// Книга
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Ідентифікатор Книги
        /// </summary>
        public int BookId { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Назва книги неможе бути менше трьох букв")]
        public string NameOfBook { get; set; }

        //залежності
        /// <summary>
        /// Ідентифікатор Автора
        /// </summary>
        public int AuthorId { get; set; }
        /// <summary>
        /// Автор
        /// </summary>
        public virtual Author Author { get; set; }
        /// <summary>
        /// Рецензії про книгу
        /// </summary>
        public virtual ICollection<Review> Reviews { get; set; }

        /// <summary>
        /// Жанр книги
        /// </summary>
        public virtual ICollection<Genre> Genres { get; set; }


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

        //додаткові властивості
        /// <summary>
        /// Картинка
        /// </summary>
        public string PathToImage { get; set; }
        /// <summary>
        /// Опис Книги
        /// </summary>
        [Column(TypeName = "Text")]
        [MaxLength(2000, ErrorMessage = "Опис книги не може бути більшим ніж 1000 знаків")]
        public string AboutOfBook { get; set; }
        /// <summary>
        /// Кількість сторінок
        /// </summary>
        public int CountOfPage { get; set; }
        /// <summary>
        /// Рік видання
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// Стандартний конструктор
        /// </summary>
        public Book() { }
    }
}
