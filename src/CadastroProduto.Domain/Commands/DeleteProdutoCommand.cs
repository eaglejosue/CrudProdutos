using CadastroProduto.Domain.Validations;

namespace CadastroProduto.Domain.Commands
{
    public class DeleteProdutoCommand : ProdutoCommand
    {
        public DeleteProdutoCommand(int id) => Id = id;

        public override bool IsValid()
        {
            var validation = new ProdutoValidation();
            validation.ValidarId();

            ValidationResult = validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}