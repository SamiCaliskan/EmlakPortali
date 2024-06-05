using System.ComponentModel.DataAnnotations;

namespace EmlakPortali.Models
{
    public class Advert
    {
        [Key]
        public int AdvertId { get; set; }
        public string AdvertTitle { get; set; } = null!;

        public string Date { get; set; }

        public bool IsActive { get; set; }
    }
}
