using BookReview.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReview.Domain.Models
{
    /// <summary>
    /// Рецензія
    /// </summary>
    public class Review
    {
        /// <summary>
        /// Ідентифікатор Рецензії
        /// </summary>
        public int ReviewId { get; set; }
        /// <summary>
        /// Текст Рецензії
        /// </summary>
        [Column(TypeName ="Text")]
        [MaxLength(2000, ErrorMessage ="Рецензія не може бути більшою ніж 2000 знаків")]
        [Required]
        public string TextOfReview { get; set; }
        /// <summary>
        /// Автор Рецензії
        /// </summary>
        public ApplicationUser AppUser { get; set; }
        //Залежності
        /// <summary>
        /// Ідентифікатор Книги
        /// </summary>
        public int BookId { get; set; }
        /// <summary>
        /// Книга
        /// </summary>
        public virtual Book Book { get; set; }
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
        /// Позначка чи позитивна рецензія
        /// </summary>
        public bool IsPositive { get; set; }
        /// <summary>
        /// Стандартний конструктор
        /// </summary>
        public Review() { }
    }
}
