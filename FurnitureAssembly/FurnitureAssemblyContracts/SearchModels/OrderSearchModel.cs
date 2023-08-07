using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyContracts.SearchModels
{
	public class OrderSearchModel
	{
		public int? Id { get; set; }
        public int? SetId { get; set; }
        public List<int>? OrderInfoId { get; set; }
	}
}
