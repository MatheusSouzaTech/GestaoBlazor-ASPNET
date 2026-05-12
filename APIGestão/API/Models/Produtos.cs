using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APIGestão.API.Models
{
    [Table("Produtos")]
    public class Produtos
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Nome { get; set; } = string.Empty;
        [ForeignKey(nameof(Marca))]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione uma marca válida.")]
        public int IDMarca { get; set; }
        public Marcas? Marca { get; set; }
        public int Quantidade { get; set; }
        [Required]
        public string TipoMercadoria { get; set; } = string.Empty;
        public bool Status { get; set; }
        public DateOnly? DataCadastro { get; set; }

        [JsonIgnore]
        public ICollection<Fornecedores>? Fornecedores { get; set; }
    }
}
