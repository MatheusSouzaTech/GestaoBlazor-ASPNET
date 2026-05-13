using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsLibrary.Models
{
    [Table("Marcas")]
    public class Marcas
    {
        [Key]
        public int Id { get; set; }

        public string? Nome { get; set; }

        public ICollection<Produto>? Produtos { get; set; }
    }
}
