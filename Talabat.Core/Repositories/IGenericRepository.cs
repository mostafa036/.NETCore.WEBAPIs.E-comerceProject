using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Specifications;

namespace Talabat.Core.IRepositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetallwithSpecAsync(ISpecifications<T> spec);       
        Task<T> GetIdwithSpecAsync(ISpecifications<T> spec);
        Task<int> GetCountAsync (ISpecifications<T> spec);       
        Task<int> CreateAsync(T entity);
        void Update(T entity);
        void Delete (T entity);
               



    }
}
