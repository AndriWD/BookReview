using BookReview.Arhitecture.Domain.Models;
using BookReview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookReview.Arhitecture.Services
{
    public class InitialDatabase
    {
        public static void Initial(ApplicationDbContext db)
        {
            if (!db.Genres.Any())
            {
                db.Genres.AddRange(new List<Genre>()
            {
                new Genre() {Id = 1, Authors = null, Books = null, Name = "Бойовики"},
                new Genre() {Id = 2, Authors = null, Books = null, Name = "Книги про війну"},
                new Genre() {Id = 3, Authors = null, Books = null, Name = "Детективи"},
                new Genre() {Id = 4, Authors = null, Books = null, Name = "Книги для дітей"},
                new Genre() {Id = 5, Authors = null, Books = null, Name = "Драма"},
                new Genre() {Id = 6, Authors = null, Books = null, Name = "Історична література"},
                new Genre() {Id = 7, Authors = null, Books = null, Name = "Наукова література"},
                new Genre() {Id = 8, Authors = null, Books = null, Name = "Прикладна література"},
                new Genre() {Id = 9, Authors = null, Books = null, Name = "Пригодницькі книги"},
                new Genre() {Id = 10, Authors = null, Books = null, Name = "Психологія"},
                new Genre() {Id = 11, Authors = null, Books = null, Name = "Романи"},
                new Genre() {Id = 12, Authors = null, Books = null, Name = "Казки"},
                new Genre() {Id = 13, Authors = null, Books = null, Name = "Сучасна література"},
                new Genre() {Id = 14, Authors = null, Books = null, Name = "Триллери"},
                new Genre() {Id = 15, Authors = null, Books = null, Name = "Жахи"},
                new Genre() {Id = 16, Authors = null, Books = null, Name = "Містика"},
                new Genre() {Id = 17, Authors = null, Books = null, Name = "Фантастика"},
                new Genre() {Id = 17, Authors = null, Books = null, Name = "Гумор"},
                new Genre() {Id = 17, Authors = null, Books = null, Name = "Фентезі"}
               });
                db.SaveChanges();
            }
        }
    }
}












 //    Horrors,
 //    Mistic,
 //    Fantast,
 //    Humor