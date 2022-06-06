using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cs_app.Data.Models
{
    public class Booking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long Id { get; set; }
        public string Hotel { get; set; }
        public int Room { get; set; }
        public string CarName { get; set; }
        public string GuideName { get; set; }
    
        public IEnumerable<Avia> Avias { get; set; }
        public IEnumerable<Passenger> Passengers { get; set; }

    }
}