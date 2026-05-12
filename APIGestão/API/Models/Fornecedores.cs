using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIGestão.API.Models
{
    [Table("Fornecedores")]
    public class Fornecedores
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Nome { get; set; } = string.Empty;
        [Required]
        public string CNPJ { get; set; } = string.Empty;
        [Required]
        public string Endereco { get; set; } = string.Empty;
        [Required]
        public string Telefone { get; set; } = string.Empty;
        public bool Status { get; set; }
        public DateOnly DataCadastro { get; set; }

        public ICollection<Produtos>? Produtos { get; set; }
        public ICollection<Marcas>? Marcas { get; set; }
        public ICollection<Transportadora>? Transportadoras { get; set; }
    }
}
