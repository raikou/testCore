using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testCore.Entity;
using testCore.Entity.Models;

namespace testCore.Controllers.test
{
	[Produces("application/json")]
	[Route("api/test")]
	public class testController : Controller
	{
		// GET: api/test
		[HttpGet]
		public IEnumerable<string> Get()
		{
			//DbContext
			using (var context = new DomainModelPostgreSqlContext())
			{
				// do stuff
				var data = from x in context.Person
					select x;
				
				var addItem = new Person()
				{
					Id = 1,
					Name = "‚È‚Ü‚¦",
					Age = 23
				};

				var dataList = data.ToList();
				dataList.Add(addItem);

				context.SaveChanges();
			}


			return new string[] { "value1", "value2" };
		}

		// GET: api/test/5
		[HttpGet("{id}", Name = "Get")]
		public string Get(int id)
		{
			return "value";
		}

		// POST: api/test
		[HttpPost]
		public void Post([FromBody]string value)
		{
			return;
		}

		// PUT: api/test/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody]string value)
		{
			return;
		}

		// DELETE: api/ApiWithActions/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
			return;
		}
	}
}
