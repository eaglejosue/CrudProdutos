using System;

namespace CadastroProduto.Domain.Entities
{
    public class Produto
    {
        public Produto() { }

        public Produto(string nome, decimal valor, byte[] imagem = null, DateTime? dataCriacao = null)
        {
            Nome = nome;
            Valor = valor;
            Imagem = imagem;
            DataCriacao = dataCriacao ?? DateTime.Now;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public byte[] Imagem { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}