using BookReview.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookReview.Arhitecture.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Dispose();
        void Save();
        
    }
}