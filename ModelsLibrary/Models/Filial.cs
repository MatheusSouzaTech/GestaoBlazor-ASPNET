using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsLibrary.Models
{
    [Table("Filiais")]
    public class Filial
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EmpresaId { get; set; }

        public string? Nome { get; set; }

        public string? CNPJ { get; set; }

        public bool Ativo { get; set; } = true;

        public DateTime DataCadastro { get; set; } = DateTime.Now;

        [ForeignKey("EmpresaId")]
        public Empresa? Empresa { get; set; }

        public ICollection<Endereco>? Enderecos { get; set; }
    }
}
