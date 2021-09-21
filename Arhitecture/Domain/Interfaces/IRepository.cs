using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReview.Domain.Interfaces
{
    /// <summary>
    /// Контракт Репозиторія 
    /// </summary>
    /// <typeparam name="T">Відповідний тип класу, що реалізує його</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Отримати всі обєкти даного типу
        /// </summary>
        /// <returns>Всі елементи заданого типу</returns>
        IEnumerable<T> GetAllItems();
        /// <summary>
        /// Отримати конкретний об'єкт за ідентифікатором
        /// </summary>
        /// <param name="id">Ідентифікатор</param>
        /// <returns></returns>
        T GetItem(int id);
        void Create(T item);
        void Update(T item);
        void Delate(int id);



    }
}
