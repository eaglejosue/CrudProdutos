using CadastroProduto.Domain.Entities;
using CadastroProduto.Domain.Validations;

namespace CadastroProduto.Domain.Commands
{
    public class UpdateProdutoCommand : ProdutoCommand
    {
        public UpdateProdutoCommand() { }

        public UpdateProdutoCommand(int id, string nome, decimal valor, string imagemURL = null)
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

        public override bool IsValid()
        {
            var validation = new ProdutoValidation();
            validation.ValidarId();
            validation.ValidarNome();
            validation.ValidarValor();

            ValidationResult = validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}