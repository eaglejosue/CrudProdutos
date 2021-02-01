using CadastroProduto.Domain.Entities;

namespace CadastroProduto.Domain.Commands
{
    public class UpdateProdutoCommand : ProdutoCommand
    {
        public UpdateProdutoCommand() { }

        public UpdateProdutoCommand(int id, string nome, decimal valor, byte[] imagem)
        {
            Id = id;
            Nome = nome;
            Valor = valor;
            Imagem = imagem;
        }

        public UpdateProdutoCommand(Produto produto)
        {
            Id = produto.Id;
            Nome = produto.Nome;
            Valor = produto.Valor;
            Imagem = produto.Imagem;
        }
    }
}