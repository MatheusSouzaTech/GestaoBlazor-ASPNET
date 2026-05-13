using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsLibrary.Models
{
    [Table("Transportadoras")]
    public class Transportadora
    {
        [Key]
        public int Id { get; set; }

        public string? Nome { get; set; }

        public string? CNPJ { get; set; }

        public string? Telefone { get; set; }

        public int Tipo { get; set; }

        public bool Ativo { get; set; } = true;
    }
}
