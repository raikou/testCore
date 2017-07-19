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
		public List<person> Get()
		{
			var result = new List<person>();
			//DbContext
			using (var context = new DomainModelPostgreSqlContext())
			{
				var conPerson = context.person;
				var data = from x in conPerson select new {x};
				var dataList = data.ToList();

				//追加
				{
					var addItem = new person()
					{
						id = 1 + dataList.Count(),
						name = "なまえ" + dataList.Count().ToString(),
						age = 3 +dataList.Count()
					};
					conPerson.Add(addItem);

					//更新
					if (dataList.Count > 0)
					{
						int tAge = 1 + dataList.Max(x => x.x.age);
						foreach (var v in dataList)
						{
							v.x.age = tAge;
						}
					}

					//データ取得
					result = data.Select(x => (person)x.x).ToList();
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
