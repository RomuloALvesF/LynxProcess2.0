using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TesteASPNET.Domain.Entities
{
    [Table("Produtos")]
    public class Produto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 100 caracteres.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O preço é obrigatório.")]
        [Range(0.01, 9999999.99, ErrorMessage = "O preço deve ser maior que zero.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Preço")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "O estoque é obrigatório.")]
        [Range(0, int.MaxValue, ErrorMessage = "O estoque não pode ser negativo.")]
        [Display(Name = "Estoque")]
        public int Estoque { get; set; }

        [Display(Name = "Data de Atualização")]
        [DataType(DataType.DateTime)]
        public DateTime DataAtualizacao { get; set; }

        [Display(Name = "Data de Deleção")]
        [DataType(DataType.DateTime)]
        public DateTime? DataDelecao { get; set; }

        public Produto() { }

        public Produto(string nome, decimal preco, int estoque)
        {
            Nome = nome;
            Preco = preco;
            Estoque = estoque;
            DataAtualizacao = DateTime.Now;
        }

        public void Atualizar(string nome, decimal preco, int estoque)
        {
            Nome = nome;
            Preco = preco;
            Estoque = estoque;
            DataAtualizacao = DateTime.Now;
        }
    }
}