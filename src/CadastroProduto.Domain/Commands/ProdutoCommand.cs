using CadastroProduto.Domain.Interfaces.Commands;
using CadastroProduto.Domain.Validations;
using FluentValidation.Results;
using MediatR;
using System;

namespace CadastroProduto.Domain.Commands
{
    public abstract class ProdutoCommand : IRequest<Result>, ICommand
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public string ImagemURL { get; set; }
        public DateTime DataCriacao { get; set; }

        public ValidationResult ValidationResult { get; set; }

        public virtual bool IsValid()
        {
            var validation = new ProdutoValidation();
            validation.ValidarId();
            validation.ValidarNome();
            validation.ValidarValor();
            validation.ValidarDataCriacao();

            ValidationResult = validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}