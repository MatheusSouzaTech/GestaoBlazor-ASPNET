using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsLibrary.Models
{
    [Table("Produtos")]
    public class Produto
    {
        [Key]
        public int Id { get; set; }

        public string? Nome { get; set; }
        public string? SKU { get; set; }
        public string? CodigoBarras { get; set; }

        public int? CategoriaId { get; set; }
        public int? MarcaId { get; set; }
        public int? UnidadeMedidaId { get; set; }

        public decimal PrecoVenda { get; set; }
        public decimal Custo { get; set; }

        public bool ControlaEstoque { get; set; } = true;
        public bool Ativo { get; set; } = true;
        public DateTime? DataCadastro { get; set; } = DateTime.Now;

        [ForeignKey("CategoriaId")]
        public Categoria? Categoria { get; set; }

        [ForeignKey("MarcaId")]
        public Marcas? Marca { get; set; }

        [ForeignKey("UnidadeMedidaId")]
        public UnidadeMedida? UnidadeMedida { get; set; }

        public ICollection<TabelaPrecoItem>? TabelaPrecoItens { get; set; }
    }
}
