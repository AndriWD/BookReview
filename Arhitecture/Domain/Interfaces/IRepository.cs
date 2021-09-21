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
        /// <returns>Об'єкт типу T за ідентифікатором</returns>
        T GetItem(int id);
        /// <summary>
        /// Додати об'єкт типу T до БД
        /// </summary>
        /// <param name="item">Об'єкт типу T</param>
        void Create(T item);
        /// <summary>
        /// Оновити об'єкт типу T в БД
        /// </summary>
        /// <param name="item">Об'єкт типу T</param>
        void Update(T item);
        /// <summary>
        /// Видалити об'єкт типу T з БД за ідентифікатором
        /// </summary>
        /// <param name="id">Ідентифікатор</param>
        void Delate(int id);



    }
}
