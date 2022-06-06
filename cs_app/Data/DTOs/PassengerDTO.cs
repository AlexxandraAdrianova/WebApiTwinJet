namespace cs_app.Data.DTOs
{
    public class PassengerDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string SecName { get; set; }
        public int Age { get; set; }
        public int Doc { get; set; }
    
        public IEnumerable<long> Bookings { get; set; }
        public IEnumerable<long> Avias { get; set; }
    }
}