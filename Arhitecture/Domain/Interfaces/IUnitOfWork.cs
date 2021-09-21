using BookReview.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookReview.Arhitecture.Domain.Interfaces
{
    /// <summary>
    /// Контракт для реалізації паттерну UnitOfWork
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Метод для очищення виділеної пам'яті
        /// </summary>
        void Dispose();
        /// <summary>
        /// Збереження в БД
        /// </summary>
        void Save();
        
    }
}