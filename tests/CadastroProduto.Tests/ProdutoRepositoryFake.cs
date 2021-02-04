using CadastroProduto.Domain.Entities;
using CadastroProduto.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CadastroProduto.Tests
{
    public class ProdutoRepositoryFake : IProdutoRepository
    {
        private int ProdutoId = 0;

        public Task<Produto> Add(Produto produto)
        {
            produto.Id = ProdutoId++;
            return Task.FromResult(produto);
        }

        public Task<IEnumerable<Produto>> GetAll()
        {
            var produtos = new List<Produto>();

            for (int i = 0; i < 10; i++)
                produtos.Add(new Produto(ProdutoId, $"Produto {ProdutoId}", 1.5m * ProdutoId));

            return Task.FromResult<IEnumerable<Produto>>(produtos);
        }

        public Task<Produto> GetById(int? id)
        {
            return Task.FromResult(new Produto(ProdutoId, $"Produto {ProdutoId}", 1.5m * ProdutoId));
        }

        public Task<Produto> GetByNome(string nome)
        {
            return Task.FromResult(new Produto(ProdutoId, "Produto A", 10m));
        }

        public Task Remove(Produto produto)
        {
            return Task.CompletedTask;
        }

        public Task<Produto> Update(Produto produto)
        {
            return Task.FromResult(produto);
        }

        public void Dispose() { }
    }
}
