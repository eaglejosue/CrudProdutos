using CadastroProduto.Data.Context;
using CadastroProduto.Domain.Entities;
using CadastroProduto.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroProduto.Data.Repositories
{
    public class ProdutoRepository : RepositoryBase<Produto>, IProdutoRepository
    {
        public ProdutoRepository(CadastroProdutoContext context) : base(context) { }

        public async Task<Produto> GetByNome(string nome) =>
            await Db.Produtos
                .Where(b => EF.Functions.Like(b.Nome, $"%{nome}%"))
                .FirstOrDefaultAsync();
    }
}