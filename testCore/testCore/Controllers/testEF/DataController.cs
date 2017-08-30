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
	[Route("api/data")]
	public class dataController : Controller
	{
		private readonly DomainModelPostgreSqlContext _context;

		public dataController(DomainModelPostgreSqlContext context)
		{
			_context = context;
		}

		// GET: api/Data
		[HttpGet]
		public IEnumerable<tododetaildata> Getdata()
		{
			return _context.tododetaildata;
		}

		// GET: api/Data/5
		[HttpGet("{userid}/{dataid}")]
		public async Task<IActionResult> Getdata([FromRoute] int userid, [FromRoute] int dataid)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				var data = await _context.tododetaildata.SingleOrDefaultAsync(m => m.userid == userid );

				if (data == null)
				{
					return NotFound();
				}

				return Ok(data);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}

		// PUT: api/Data/5
		[HttpPut("{id}")]
		public async Task<IActionResult> Putdata([FromRoute] int userid, [FromRoute] int dataid, [FromBody] tododetaildata data)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (false == (userid == data.userid && dataid == data.dataid))
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
				if (!dataExists(userid, dataid))
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
		public async Task<IActionResult> Postdata([FromBody] tododetaildata data)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			_context.tododetaildata.Add(data);
			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateException)
			{
				if (dataExists(data.userid, data.dataid))
				{
					return new StatusCodeResult(StatusCodes.Status409Conflict);
				}
				else
				{
					throw;
				}
			}

			return CreatedAtAction("Getdata", new { userid = data.userid, dataid = data.dataid }, data);
		}

		// DELETE: api/Data/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> Deletedata([FromRoute] int userid, [FromRoute] int dataid)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var data = await _context.tododetaildata.SingleOrDefaultAsync(m => m.userid == userid && m.dataid == dataid);
			if (data == null)
			{
				return NotFound();
			}

			_context.tododetaildata.Remove(data);
			await _context.SaveChangesAsync();

			return Ok(data);
		}

		private bool dataExists([FromRoute] int userid, [FromRoute] int dataid)
		{
			return _context.tododetaildata.Any(m => m.userid == userid && m.dataid == dataid);
		}
	}
}