using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsLibrary.Models
{
    [Table("UnidadesMedida")]
    public class UnidadeMedida
    {
        [Key]
        public int Id { get; set; }

        public string? Nome { get; set; }

        public string? Sigla { get; set; }

        public ICollection<Produto>? Produtos { get; set; }
    }
}
