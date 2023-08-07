using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssembly_WorkerContracts.ViewModels
{
	public class ReportSetFurnitureModuleViewModel
	{
		public string SetName { get; set; } = string.Empty;
		public int TotalCount { get; set; }
		public List<(string FurnitureModule, int Count)> FurnitureModules { get; set; } = new();
	}
}
