using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CadastroProduto.Domain.Interfaces.Repositories
{
    public interface IRepositoryBase<TEntity> : IDisposable where TEntity : class
    {
        Task<TEntity> Add(TEntity obj);
        Task<TEntity> GetById(int? id);
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> Update(TEntity obj);
        Task Remove(TEntity obj);
    }
}