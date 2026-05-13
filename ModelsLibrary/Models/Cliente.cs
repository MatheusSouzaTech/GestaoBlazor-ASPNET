using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsLibrary.Models
{
    [Table("Clientes")]
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; } = string.Empty;

        [Required]
        public int TipoPessoa { get; set; }

        public string? CPF_CNPJ { get; set; }

        public string? Email { get; set; }

        public string? Telefone { get; set; }

        public bool Ativo { get; set; } = true;

        public DateTime DataCadastro { get; set; } = DateTime.Now;

        public ICollection<Endereco>? Enderecos { get; set; }
    }
}
