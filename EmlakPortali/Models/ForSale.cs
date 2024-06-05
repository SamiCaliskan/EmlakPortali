using System.ComponentModel.DataAnnotations;

namespace EmlakPortali.Models
{
    public class ForSale
    {
        [Key]
        public int ForSaleId { get; set; }
        public string ForSaleTitle { get; set; } = null!;

        public string Date { get; set; }

        public bool IsActive { get; set; }
    }
}
