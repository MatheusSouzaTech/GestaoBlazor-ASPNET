using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsLibrary.Models
{
    [Table("Fornecedores")]
    public class Fornecedor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string RazaoSocial { get; set; } = string.Empty;

        public string? CNPJ { get; set; }

        public string? Email { get; set; }

        public string? Telefone { get; set; }

        public bool Ativo { get; set; } = true;

        public DateTime DataCadastro { get; set; } = DateTime.Now;

        public ICollection<Endereco>? Enderecos { get; set; }
    }
}
