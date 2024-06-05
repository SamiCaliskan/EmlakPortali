namespace EmlakPortali.DTO
{
    public class AdvertDTO
    {
        public int AdvertId { get; set; }
        public string AdvertTitle { get; set; } = null!;

        public string Date { get; set; }
        public bool IsActive { get; internal set; }
    }
}
