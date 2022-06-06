using Blazor.Data.Models;

namespace Blazor.Data.Models
{
    public class Passenger
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string SecName { get; set; }
        public int Age { get; set; }
        public int Doc { get; set; }
    
        public IEnumerable<Booking> Bookings { get; set; }
        public IEnumerable<Avia> Avias { get; set; }
    }

}
