using System.Threading.Tasks;
using CadastroProduto.Domain.Entities;

namespace CadastroProduto.Domain.Interfaces.Repositories
{
    public interface IProdutoRepository : IRepositoryBase<Produto>
    {
        Task<Produto> GetByNome(string nome);
    }
}