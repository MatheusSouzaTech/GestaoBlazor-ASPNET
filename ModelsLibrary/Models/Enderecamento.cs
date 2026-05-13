using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsLibrary.Models
{
    [Table("Enderecamentos")]
    public class Enderecamento
    {
        [Key]
        public int Id { get; set; }

        public int? ArmazemId { get; set; }
        public string? Rua { get; set; }
        public string? Prateleira { get; set; }
        public string? Posicao { get; set; }

        [ForeignKey("ArmazemId")]
        public Armazem? Armazem { get; set; }
    }
}
