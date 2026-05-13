using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsLibrary.Models
{
    [Table("Empresas")]
    public class Empresa
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string RazaoSocial { get; set; } = string.Empty;

        public string? NomeFantasia { get; set; }

        [Required]
        public string CNPJ { get; set; } = string.Empty;

        public int RegimeTributario { get; set; }

        public bool Ativo { get; set; } = true;

        public DateTime DataCadastro { get; set; } = DateTime.Now;

        public ICollection<Filial>? Filiais { get; set; }
    }
}
