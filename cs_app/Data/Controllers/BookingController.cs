using Microsoft.AspNetCore.Mvc;
using cs_app.Data.Models;
using cs_app.Data.Services;
using cs_app.Data.DTOs;

namespace cs_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly BookingService _context;

        public BookingsController(BookingService context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
        {
            return await _context.GetBookings();
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<Booking>> GetBookingsById(int id)
        {
            var bookings = await _context.GetBooking(id);

            if (bookings == null)
            {
                return NotFound();
            }

            return bookings;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Booking>> PutBookings(int id, [FromBody] BookingDTO bookings)
        {
            var result = await _context.UpdateBooking(id, bookings);
            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Booking>> PostBookings([FromBody] BookingDTO bookings)
        {
            var result = await _context.AddBooking(bookings);
            if (result == null)
            {
                BadRequest();
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookings(int id)
        {
            var bookings = await _context.DeleteBooking(id);
            if (bookings)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("incomplete")]
        public async Task<ActionResult<IEnumerable<IncompleteBookingDTO>>> GetAuthorIncomplete()
        {
            return await _context.GetBookingsIncomplete();
        }
    }
}