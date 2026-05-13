using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsLibrary.Models
{
    [Table("Categorias")]
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        public string? Nome { get; set; }

        public int? CategoriaPaiId { get; set; }

        [ForeignKey("CategoriaPaiId")]
        public Categoria? CategoriaPai { get; set; }

        public ICollection<Categoria>? SubCategorias { get; set; }
        public ICollection<Produto>? Produtos { get; set; }
    }
}
