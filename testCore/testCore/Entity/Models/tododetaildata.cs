using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace testCore.Entity.Models
{
	public class tododetaildata
	{
		public int userid { get; set; }
		public int dataid { get; set; }
		public string title { get; set; }
		public string detail { get; set; }
		public DateTime startdate { get; set; }
		public DateTime enddate { get; set; }
		public int state { get; set; }
		public int colorid { get; set; }
	}
}