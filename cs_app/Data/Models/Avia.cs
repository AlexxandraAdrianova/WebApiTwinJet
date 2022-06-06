using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cs_app.Data.Models
{
    public class Avia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]

        public long Id { get; set; }
        public string Country { get; set; }
        public decimal Price { get; set; }
        public string Dest { get; set; }
        public string Place { get; set; }
        public int NumPass { get; set; }
        public int Flight { get; set; }
    
        public IEnumerable<Booking> Bookings { get; set; }
        public IEnumerable<Passenger> Passengers { get; set; }


    }
}