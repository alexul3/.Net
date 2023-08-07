using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyContracts.ViewModels
{
	public class ReportSetFurnitureModuleWorkerViewModel
	{
		public string SetName { get; set; } = string.Empty;
		public int TotalCount { get; set; }
		public List<(string FurnitureModule, int Count)> FurnitureModules { get; set; } = new();
	}
}
