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
            ImagemURL = imagemURL;
        }

        public UpdateProdutoCommand(Produto produto)
        {
            Id = produto.Id;
            Nome = produto.Nome;
            Valor = produto.Valor;
            ImagemURL = produto.ImagemURL;
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