using CadastroProduto.Domain.Commands;
using FluentValidation;

namespace CadastroProduto.Domain.Validations
{
    public class ProdutoValidation : AbstractValidator<ProdutoCommand>
    {
        public void ValidarId()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0).WithMessage("O Id deve ser maior que zero.");
        }

        public void ValidarNome()
        {
            RuleFor(p => p.Nome)
                .NotEmpty().WithMessage("O Nome n�o pode ser vazio.")
                .MaximumLength(100).WithMessage("O Nome deve possui no m�ximo 100 caracteres.");
        }
        
        public void ValidarValor()
        {
            RuleFor(p => p.Valor)
                .GreaterThan(0).WithMessage("O Valor deve ser maior que zero.");
        }
        
        public void ValidarDataCriacao()
        {
            RuleFor(p => p.DataCriacao)
                .NotNull().WithMessage("A Data de Cria��o n�o pode ser nula.");
        }
    }
}