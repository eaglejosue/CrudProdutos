using CadastroProduto.Domain.Entities;
using System;

namespace CadastroProduto.Domain.Commands
{
    public class CreateProdutoCommand : ProdutoCommand
    {
        public CreateProdutoCommand() { }

        public CreateProdutoCommand(int id, string nome, decimal valor, byte[] imagem, DateTime? dataCriacao = null)
        {
            Id = id;
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
    }
}