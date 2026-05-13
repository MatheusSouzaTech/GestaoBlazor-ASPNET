using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsLibrary.Models
{
    [Table("TabelasPreco")]
    public class TabelaPreco
    {
        [Key]
        public int Id { get; set; }

        public string? Nome { get; set; }

        public ICollection<TabelaPrecoItem>? Itens { get; set; }
    }
}
