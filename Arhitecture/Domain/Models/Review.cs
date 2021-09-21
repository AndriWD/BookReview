using BookReview.Arhitecture.Domain.Models;
using BookReview.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReview.Domain.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        [Column(TypeName ="Text")]
        [MaxLength(2000, ErrorMessage ="Рецензія не може бути більшою ніж 2000 знаків")]
        [Required]
        public string TextOfReview { get; set; }
        public ApplicationUser AppUser { get; set; }
        //Залежності
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
        //голосування, добавлення голосів буде реалізовано в контроллері
        public int Vote { get; set; }
        public int VoteCounter { get; set; }
        [NotMapped]
        public double RaitingInPersent => (double)Vote / (double)VoteCounter;
        public bool IsPositive { get; set; }
    }
    public class IndexViewReview
    {
        public IEnumerable<Review> Reviews { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
