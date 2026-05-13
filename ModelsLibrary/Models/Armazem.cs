using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsLibrary.Models
{
    [Table("Armazens")]
    public class Armazem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int FilialId { get; set; }

        public string? Nome { get; set; }

        [ForeignKey("FilialId")]
        public Filial? Filial { get; set; }

        public ICollection<Enderecamento>? Enderecamentos { get; set; }
    }
}
