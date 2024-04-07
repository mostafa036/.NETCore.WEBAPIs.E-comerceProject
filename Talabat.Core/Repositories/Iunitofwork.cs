using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Entites.Order_Aggregate;
using Talabat.Core.Entites.OrderAggregate;
using Talabat.Core.IRepositories;

namespace Talabat.Core.Repositories
{
    public interface Iunitofwork : IDisposable
    {

        IGenericRepository<TEntity> Repository<TEntity>()where TEntity : BaseEntity;
        Task<int> Complete();

        //IGenericRepository<Product> productRepo();
        //IGenericRepository<Order> OrderRepo();
        //IGenericRepository<Deliverymethod> DeliverymethodRepos();
        //IGenericRepository<OrderItem> orderItemRepo();

        //IGenericRepository<ProductBrand> brandsRepo();
        //IGenericRepository<ProductType> typesReo();
    }
}
