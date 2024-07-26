using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SistemaCompras.Models
{
    [Table("Produto")]
    public class Produto
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [Column("Descricao")]
        [Display(Name = "Descricao")]
        [MaxLength(20), MinLength(2)]
        public String? Descricao { get; set; }

        [Column("Preco")]
        [Display(Name = "Preco")]
        public double Preco { get; set; }

    }
}
