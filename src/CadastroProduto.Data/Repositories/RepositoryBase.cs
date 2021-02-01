using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CadastroProduto.Data.Context;
using CadastroProduto.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CadastroProduto.Data.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        protected readonly CadastroProdutoContext Db;

        public RepositoryBase(CadastroProdutoContext context) => Db = context;

        public virtual async Task<TEntity> Add(TEntity obj)
        {
            var entity = Db.Add(obj).Entity;
            await Db.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll() =>
            await Db.Set<TEntity>().ToListAsync();

        public virtual async Task<TEntity> GetById(int? id) =>
            await Db.Set<TEntity>().FindAsync(id);

        public virtual async Task<TEntity> Update(TEntity obj)
        {
            var entity = Db.Update(obj).Entity;
            await Db.SaveChangesAsync();
            return entity;
        }

        public virtual async Task Remove(TEntity obj)
        {
            Db.Set<TEntity>().Remove(obj);
            await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}