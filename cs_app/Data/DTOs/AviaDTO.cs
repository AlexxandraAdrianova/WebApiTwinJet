namespace cs_app.Data.DTOs
{
    public class AviaDTO
    {
        public long Id { get; set; }
        public string Dest { get; set; }
        public string Place { get; set; }
        public int NumPass { get; set; }
        public int Flight { get; set; }
        public int Price { get; set; }
        public string Country { get; set; }
        public IEnumerable<long> Bookings { get; set; }
        public IEnumerable<long> Passengers { get; set; }
    }
}