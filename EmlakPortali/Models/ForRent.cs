using System.ComponentModel.DataAnnotations;

namespace EmlakPortali.Models
{
    public class ForRent
    {
        [Key]
        public int ForRentId { get; set; }
        public string ForRentTitle { get; set; } = null!;

        public string Date { get; set; }

        public bool IsActive { get; set; }
    }
}
