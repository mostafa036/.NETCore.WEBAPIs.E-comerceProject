using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.IRepositories;
using Talabat.Core.Repositories;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class Unitofwork : Iunitofwork
    {
        private readonly TalabatContext _Context;
        private Hashtable _repositories;
        public Unitofwork(TalabatContext Context)
        {
            _Context = Context;
        }
        public async Task<int> Complete()
         => await _Context.SaveChangesAsync();
        

        public void Dispose()
        {
           _Context.Dispose();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories == null )
                _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositores = new GenericRepository<TEntity>(_Context);

                _repositories.Add(type, repositores);
            }

            return (IGenericRepository < TEntity >) _repositories[type];

            
        }
    }
}
