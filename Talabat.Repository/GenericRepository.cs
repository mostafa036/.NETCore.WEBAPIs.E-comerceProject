using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.IRepositories;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly TalabatContext _context;

        public GenericRepository(TalabatContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
           // if (typeof(T) == typeof(Product))
             //  return (IEnumerable<T>) await _context.Set<Product>().Include( P => P.ProductBrand).Include( P => P.ProductType).ToListAsync();
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)

        // => await _context.Set<T>().Where(item => item.Id == id).FirstOrDefault();

        => await _context.Set<T>().FindAsync(id);

        public async Task<IReadOnlyList<T>> GetallwithSpecAsync(ISpecifications<T> spec)
        {
           return await AppluSpecitication(spec).ToListAsync();
        }

        public async Task<T> GetIdwithSpecAsync(ISpecifications<T> spec)
        {
            return await AppluSpecitication(spec).FirstOrDefaultAsync();
        }
        public async Task<int> GetCountAsync(ISpecifications<T> spec)
        {
            return await AppluSpecitication(spec).CountAsync();
        }

        private IQueryable<T> AppluSpecitication(ISpecifications<T> spec)
        {
            return SpecificationsEvaluator<T>.GetQuery(_context.Set<T>(), spec);
        }
        public async Task CreateAsync(T entity)
            => await _context.Set<T>().AddAsync(entity);     

        public void Update(T entity)
            =>  _context.Set<T>().Update(entity);

        public void Delete(T entity)
            => _context.Set<T>().Remove(entity);

        Task<int> IGenericRepository<T>.CreateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
