using FurnitureAssembly_WorkerDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssembly_WorkerContracts.BindingModels
{
	public class FurnitureBindingModel : IFurnitureModel
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public double Cost { get; set; }
		public int UserId { get; set; }
		public DateTime DateCreate { get; set; } = DateTime.Now;
		public Dictionary<int, (IMaterialModel, int)> FurnitureMaterials { get; set; } = new();
	}
}
