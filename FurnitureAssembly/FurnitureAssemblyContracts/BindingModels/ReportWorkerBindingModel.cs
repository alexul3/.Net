using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyContracts.BindingModels
{
	public class ReportWorkerBindingModel
	{
		public string FileName { get; set; } = string.Empty;
		public DateTime? DateFrom { get; set; }
		public DateTime? DateTo { get; set; }
		public List<int>? SetIds { get; set; }
        public int UserId { get; set; }
    }
}
