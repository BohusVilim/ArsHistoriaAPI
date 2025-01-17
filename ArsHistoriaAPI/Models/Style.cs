namespace ArsHistoriaAPI.Models
{
    public class Style
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public Period Period { get; set; }
    }
}
