using System;

namespace CadastroProduto.Domain.Entities
{
    public class Produto
    {
        public Produto() { }

        public Produto(string nome, decimal valor, string imagemURL = null, DateTime? dataCriacao = null)
        {
            Nome = nome;
            Valor = valor;
            ImagemURL = imagemURL;
            DataCriacao = dataCriacao ?? DateTime.Now;
        }

        public Produto(int id, string nome, decimal valor, string imagemURL = null, DateTime? dataCriacao = null)
        {
            Id = id;
            Nome = nome;
            Valor = valor;
            ImagemURL = imagemURL;
            DataCriacao = dataCriacao ?? DateTime.Now;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public string ImagemURL { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}