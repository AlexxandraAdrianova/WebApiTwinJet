using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cs_app.Data.Models
{
    public class Passenger
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
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
