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
    [Route("api/people")]
    public class peopleController : Controller
    {
        private readonly DomainModelPostgreSqlContext _context;

        public peopleController(DomainModelPostgreSqlContext context)
        {
            _context = context;
        }

        // GET: api/people
        [HttpGet]
        public IEnumerable<person> Getperson()
        {
            return _context.person;
        }

        // GET: api/people/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Getperson([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var person = await _context.person.SingleOrDefaultAsync(m => m.id == id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        // PUT: api/people/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Putperson([FromRoute] int id, [FromBody] person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != person.id)
            {
                return BadRequest();
            }

            _context.Entry(person).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!personExists(id))
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

        // POST: api/people
        [HttpPost]
        public async Task<IActionResult> Postperson([FromBody] person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.person.Add(person);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getperson", new { id = person.id }, person);
        }

        // DELETE: api/people/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteperson([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var person = await _context.person.SingleOrDefaultAsync(m => m.id == id);
            if (person == null)
            {
                return NotFound();
            }

            _context.person.Remove(person);
            await _context.SaveChangesAsync();

            return Ok(person);
        }

        private bool personExists(int id)
        {
            return _context.person.Any(e => e.id == id);
        }
    }
}