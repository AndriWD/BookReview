using BookReview.Arhitecture.Domain.Models;
using BookReview.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BookReview.Infrastructura.Bussines.Expensive_Method
{
    /// <summary>
    /// Методи розширення
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Проголосувати за автора
        /// </summary>
        /// <param name="author">Автор</param>
        /// <param name="isPositive">Чи голос позитивний</param>
        public static void Vote(this Author author, bool isPositive)
        {
            if (author == null)
            {
                throw new NullReferenceException("Автора не вибрано");
            }

            if (isPositive == true)
            {
                author.Vote += 1;
                author.VoteCounter += 1;

            }
            else
            {
                author.Vote -= 1;
                author.VoteCounter += 1;

            }
        }
        /// <summary>
        /// Проголосувати за книгу
        /// </summary>
        /// <param name="book">Книга</param>
        /// <param name="isPositive">Чи голос позитивний</param>
        public static void Vote(this Book book, bool isPositive)
        {
            if (book == null)
            {
                throw new NullReferenceException("Книгу не вибрано");
            }

            if (isPositive == true)
            {
                book.Vote += 1;
                book.VoteCounter += 1;
                
                book.Author.Vote += 1;
                book.Author.VoteCounter += 1;

            }
            else
            {
                book.Vote -= 1;
                book.VoteCounter += 1;

                book.Author.Vote -= 1;
                book.Author.VoteCounter += 1;

            }
        }
        /// <summary>
        /// Проголосувати за рецензію
        /// </summary>
        /// <param name="review">Рецензія</param>
        /// <param name="isPositive">Чи голос позитивний</param>
        public static void Vote(this Review review, bool isPositive)
        {
            if (review == null)
            {
                throw new NullReferenceException("Рецензію не вибрано");
            }

            if (isPositive == true)
            {
                review.Vote += 1;
                review.VoteCounter += 1;

                review.Book.Vote += 1;
                review.Book.VoteCounter += 1;

                review.Book.Author.Vote += 1;
                review.Book.Author.VoteCounter += 1;
            }
            else
            {
                review.Vote -= 1;
                review.VoteCounter += 1;

                review.Book.Vote -= 1;
                review.Book.VoteCounter += 1;

                review.Book.Author.Vote -= 1;
                review.Book.Author.VoteCounter += 1;
            }
        }
        /// <summary>
        /// HTML хелпер додавання посилань на наступні сторінки при використанні класу PageInfo
        /// </summary>
        /// <param name="html">HTMLHelper</param>
        /// <param name="pageInfo">Дані про сторінки</param>
        /// <param name="pageUrl">Посилання</param>
        /// <returns>Посилання на інші сторінки з PageInfo</returns>
        public static MvcHtmlString PageLinks(this HtmlHelper html, PageInfo pageInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i < pageInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                //якщо текуща сторінка, то виділяємо її класом
                if (i == pageInfo.PageNumber)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }
                tag.AddCssClass("btn btn-default");
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }

    }
    
}
