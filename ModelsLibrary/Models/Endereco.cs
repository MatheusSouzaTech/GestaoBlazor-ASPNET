using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsLibrary.Models
{
    [Table("Enderecos")]
    public class Endereco
    {
        [Key]
        public int Id { get; set; }

        public int? ClienteId { get; set; }
        public int? FornecedorId { get; set; }
        public int? FilialId { get; set; }

        public string? CEP { get; set; }
        public string? Logradouro { get; set; }
        public string? Numero { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }
        public int TipoEndereco { get; set; }

        [ForeignKey("ClienteId")]
        public Cliente? Cliente { get; set; }

        [ForeignKey("FornecedorId")]
        public Fornecedor? Fornecedor { get; set; }

        [ForeignKey("FilialId")]
        public Filial? Filial { get; set; }
    }
}
