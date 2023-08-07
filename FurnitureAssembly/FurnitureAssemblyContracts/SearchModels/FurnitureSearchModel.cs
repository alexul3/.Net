using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyContracts.SearchModels
{
	public class FurnitureSearchModel
	{
		public int? Id { get; set; }
		public string? Name { get; set; }
		public int? UserId { get; set; }

		public DateTime? DateFrom { get; set; }
		public DateTime? DateTo { get; set; }
	}
}
