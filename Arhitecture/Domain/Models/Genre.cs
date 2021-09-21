﻿using BookReview.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookReview.Arhitecture.Domain.Models
{
    /// <summary>
    /// Жанр
    /// </summary>
    public class Genre
    {
        /// <summary>
        /// Ідентифікатор
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Назва Жанру
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Повертає назву жанру
        /// </summary>
        /// <returns>Назва жанру</returns>
        public override string ToString()
        {
            return Name;
        }
        /// <summary>
        /// Автори
        /// </summary>
        public ICollection<Author> Authors { get; set; }
        /// <summary>
        /// Книги
        /// </summary>
        public ICollection<Book> Books { get; set; }
        /// <summary>
        /// Стандартний конструктор
        /// </summary>
        public Genre() { }
    }

    //ActionBook,
    //    WarBook,
    //    Detective,
    //    ChildrensLiteratures,
    //    Drama,
    //    HistoricalLiturature,
    //    Classic,
    //    ScientFiction,
    //    AppliedLiterature,
    //    Politics,
    //    Adventures,
    //    Psyhology,
    //    Novel,
    //    tales,
    //    ModernLiterature,
    //    Trillers,
    //    Horrors,
    //    Mistic,
    //    Fantast,
    //    Humor
}