using Blazor.Data.Models;

namespace Blazor.Data.Models
{
    public class Booking
    {
        public long Id { get; set; }
        public string Hotel { get; set; }
        public int Room { get; set; }
        public string CarName { get; set; }
        public string GuideName { get; set; }
    
        public IEnumerable<Avia> Avias { get; set; }
        public IEnumerable<Passenger> Passengers { get; set; }

    }
}