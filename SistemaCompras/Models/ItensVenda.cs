using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SistemaCompras.Models
{
    [Table("ItensVenda")]
    public class ItensVenda
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Quantidade")]
        [Display(Name = "Quantidade")]
        public int Quantidade { get; set; }

        [Column("Total")]
        [Display(Name = "Total")]
        public double Total { get; set; }

        [Column("VendaId")]
        [Display(Name = "VendaId")]
        public int VendaId { get; set; }

        [Column("ProdutoId")]
        [Display(Name = "ProdutoId")]
        public int ProdutoId { get; set; }
    }
}
