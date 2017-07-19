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
		public List<Person> Get()
		{
			var result = new List<Person>();
			//DbContext
			using (var context = new DomainModelPostgreSqlContext())
			{
				var conPerson = context.Person;
				var data = from x in conPerson select new {x};
				var dataList = data.ToList();

				//追加
				{
					var addItem = new Person()
					{
						Id = 1 + dataList.Count(),
						Name = "なまえ" + dataList.Count().ToString(),
						Age = 3 +dataList.Count()
					};
					conPerson.Add(addItem);

					//更新
					int tAge = 1 + dataList.Max(x => x.x.Age);
					foreach (var v in dataList)
					{
						v.x.Age = tAge;
					}

					//データ取得
					result = data.Select(x => (Person)x.x).ToList();
				}

				context.SaveChanges();
			}


			return result;
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
