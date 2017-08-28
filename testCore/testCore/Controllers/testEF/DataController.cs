using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testCore.Entity;
using testCore.Entity.Models;

namespace testCore.Controllers.testEF
{
    [Produces("application/json")]
    [Route("api/Data")]
    public class DataController : Controller
    {
        private readonly DomainModelPostgreSqlContext _context;

        public DataController(DomainModelPostgreSqlContext context)
        {
            _context = context;
        }

        // GET: api/Data
        [HttpGet]
        public IEnumerable<data> Getdata()
        {
            return _context.data;
        }

        // GET: api/Data/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Getdata([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var data = await _context.data.SingleOrDefaultAsync(m => m.dataid == id);

            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }

        // PUT: api/Data/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Putdata([FromRoute] int id, [FromBody] data data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != data.dataid)
            {
                return BadRequest();
            }

            _context.Entry(data).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!dataExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Data
        [HttpPost]
        public async Task<IActionResult> Postdata([FromBody] data data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.data.Add(data);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getdata", new { id = data.dataid }, data);
        }

        // DELETE: api/Data/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletedata([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var data = await _context.data.SingleOrDefaultAsync(m => m.dataid == id);
            if (data == null)
            {
                return NotFound();
            }

            _context.data.Remove(data);
            await _context.SaveChangesAsync();

            return Ok(data);
        }

        private bool dataExists(int id)
        {
            return _context.data.Any(e => e.dataid == id);
        }
    }
}