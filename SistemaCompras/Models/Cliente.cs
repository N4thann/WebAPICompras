using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SistemaCompras.Models
{
    public class Cliente
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [Column("Nome")]
        [MaxLength(30), MinLength(5)]
        [Display(Name = "Nome")]
        public String Nome { get; set; }

        [Required]
        [Column("Email")]
        [MaxLength(25), MinLength(8)]
        [Display(Name = "Email")]
        public String Email { get; set; }

    }
}
