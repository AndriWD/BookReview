using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookReview.Arhitecture.Domain.Models
{
    /// <summary>
    /// Інформація для побудови сторінки
    /// </summary>
    public class PageInfo
    {
        /// <summary>
        /// Номер сторінки
        /// </summary>
        public int PageNumber { get; set; }
        /// <summary>
        /// Розмір сторінки
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// Максимальна кількість елементів
        /// </summary>
        public int TotalItems { get; set; }
        /// <summary>
        /// Загальна кількість сторінок
        /// </summary>
        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / PageSize);
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="pageNumber">Номер сторінки</param>
        /// <param name="pageSize">Розмір сторінки</param>
        /// <param name="totalItems">Загальна кількість елементів</param>
        public PageInfo(int pageNumber, int pageSize, int totalItems)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalItems = totalItems;
        }

    }
}