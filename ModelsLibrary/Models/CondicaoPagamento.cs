using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsLibrary.Models
{
    [Table("CondicoesPagamento")]
    public class CondicaoPagamento
    {
        [Key]
        public int Id { get; set; }

        public string? Nome { get; set; }

        public ICollection<CondicaoParcela>? Parcelas { get; set; }
    }
}
