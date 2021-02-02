using CadastroProduto.Domain.Entities;
using CadastroProduto.Domain.Validations;
using System;

namespace CadastroProduto.Domain.Commands
{
    public class CreateProdutoCommand : ProdutoCommand
    {
        public CreateProdutoCommand() { }

        public CreateProdutoCommand(string nome, decimal valor, byte[] imagem = null, DateTime? dataCriacao = null)
        {
            Nome = nome;
            Valor = valor;
            Imagem = imagem;
            DataCriacao = dataCriacao ?? DateTime.Now;
        }

        public CreateProdutoCommand(Produto produto)
        {
            Nome = produto.Nome;
            Valor = produto.Valor;
            Imagem = produto.Imagem;
            DataCriacao = DateTime.Now;
        }

        public override bool IsValid()
        {
            var validation = new ProdutoValidation();
            validation.ValidarNome();
            validation.ValidarValor();
            validation.ValidarDataCriacao();

            ValidationResult = validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}