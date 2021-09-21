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
    public static class ExtensionMethods
    {
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
