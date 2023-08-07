using FurnitureAssembly_WorkerDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssembly_WorkerContracts.ViewModels
{
	public class ReportOrdersViewModel
	{
		public int Id { get; set; }
		public DateTime DateCreate { get; set; }
		public string SetName { get; set; } = string.Empty;
		public List<(string Name, double Cost)> FurnitureModules { get; set; } = new();
		public int TotalCount { get; set; }
		public double Sum { get; set; }
	}
}
