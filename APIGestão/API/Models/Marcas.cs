using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIGestão.API.Models
{
    [Table("Marcas")]
    public class Marcas
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Nome { get; set; } = string.Empty;
        public ICollection<Fornecedores>? Fornecedores { get; set; }
    }
}