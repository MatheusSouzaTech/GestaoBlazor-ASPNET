using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsLibrary.Models
{
    [Table("TabelaPrecoItens")]
    public class TabelaPrecoItem
    {
        [Key]
        public int Id { get; set; }

        public int? TabelaPrecoId { get; set; }
        public int? ProdutoId { get; set; }
        public decimal Preco { get; set; }

        [ForeignKey("TabelaPrecoId")]
        public TabelaPreco? TabelaPreco { get; set; }

        [ForeignKey("ProdutoId")]
        public Produto? Produto { get; set; }
    }
}
