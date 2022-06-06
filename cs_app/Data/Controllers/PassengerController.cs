using cs_app.Data.DTOs;
using cs_app.Data.Models;
using cs_app.Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cs_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        private readonly PassengerService _context;

        public PassengerController(PassengerService context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Passenger>>> GetPassengers()
        {
            return await _context.GetPassengers();
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<Passenger>> GetPassngersById(int id)
        {
            var passengers = await _context.GetPassengers(id);

            if (passengers == null)
            {
                return NotFound();
            }

            return passengers;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Passenger>> PutPassengers(int id, [FromBody] PassengerDTO passengers)
        {
            var result = await _context.UpdatePassenger(id, passengers);
            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Passenger>> PostPassengers([FromBody] PassengerDTO passengers)
        {
            var result = await _context.AddPassenger(passengers);
            if (result == null)
            {
                BadRequest();
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePassengers(int id)
        {
            var passengers = await _context.DeletePassenger(id);
            if (passengers)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("incomplete")]
        public async Task<ActionResult<IEnumerable<IncompletePassengerDTO>>> GetPassengerIncomplete()
        {
            return await _context.GetPassengersIncomplete();
        }
    }
}