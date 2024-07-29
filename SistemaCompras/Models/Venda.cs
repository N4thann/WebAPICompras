using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SistemaCompras.Models
{
    [Table("Venda")]
    public class Venda
    {
        public Venda()
        {
            ListaDeItens = new Collection<ItensVenda>();
        }

        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Total")]
        [Display(Name = "Total")]
        public double Total { get; set; }

        [Column("Status")]
        [Display(Name = "Status")]
        public String? Status { get; set; }

        [Column("ClienteId")]
        [Display(Name = "ClienteId")]
        public int ClienteId { get; set; }

        public virtual ICollection<ItensVenda> ListaDeItens { get; set; }

    }
}
