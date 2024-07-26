using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SistemaCompras.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [Column("Login")]
        [MaxLength(20), MinLength(5)]
        [Display(Name = "Login")]
        public String Login { get; set; }

        [Required]
        [Column("Senha")]
        [MaxLength(15), MinLength(8)]
        [Display(Name = "Senha")]
        public String Senha { get; set; }
    }
}
