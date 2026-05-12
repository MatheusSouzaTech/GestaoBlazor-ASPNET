using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APIGestão.API.Models
{
    [Table("Transportadora")]
    public class Transportadora
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string RazaoSocial { get; set; } = string.Empty;
        [Required]
        public string NomeFantasia { get; set; } = string.Empty;
        [Required]
        public string Telefone { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string TipoTransporte { get; set; } = string.Empty;
        public int PrazoEntrega { get; set; }
        public bool Status { get; set; }
        [JsonIgnore]
        public ICollection<Fornecedores>? Fornecedores { get; set; }
    }
}
