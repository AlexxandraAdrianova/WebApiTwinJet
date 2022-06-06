namespace cs_app.Data.DTOs
{
    public class BookingDTO
    {
        public long Id { get; set; }
        public string Hotel { get; set; }
        public int Room { get; set; }
        public string CarName { get; set; }
        public string GuideName { get; set; }
    
        public IEnumerable<long> Avias { get; set; }
        public IEnumerable<long> Passengers { get; set; }

    }
}