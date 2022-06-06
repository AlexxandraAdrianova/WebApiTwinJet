using cs_app.Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using cs_app.Data.Models;
using cs_app.Data.Services;

namespace cs_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AviaController : ControllerBase
    {
        private readonly AviaService _context;

        public AviaController(AviaService context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Avia>>> GetAvias()
        {
            return await _context.GetAvias();
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<Avia>> GetAvia(int id)
        {
            var avia = await _context.GetAvia(id);

            if (avia == null)
            {
                return NotFound();
            }

            return avia;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Avia>> PutAvia(int id, [FromBody] AviaDTO avia)
        {
            var result = await _context.UpdateAvia(id, avia);
            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Avia>> PostAvia([FromBody] AviaDTO avia)
        {
            var result = await _context.AddAvia(avia);
            if (result == null)
            {
                BadRequest();
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAvia(int id)
        {
            var avia = await _context.DeleteAvia(id);
            if (avia)
            {
                return Ok();
            }

            return BadRequest();
            
        }
        [HttpGet("incomplete")]
        public async Task<ActionResult<IEnumerable<IncompleteAviaDTO>>> GetAviasIncomplete()
        {
            return await _context.GetAviasIncomplete();
        }
    }
}