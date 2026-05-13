using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsLibrary.Models
{
    [Table("CondicaoParcelas")]
    public class CondicaoParcela
    {
        [Key]
        public int Id { get; set; }

        public int? CondicaoPagamentoId { get; set; }
        public int NumeroParcela { get; set; }
        public int Dias { get; set; }

        [ForeignKey("CondicaoPagamentoId")]
        public CondicaoPagamento? CondicaoPagamento { get; set; }
    }
}
