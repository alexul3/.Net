using FurnitureAssemblyContracts.ViewModels;
using FurnitureAssemblyDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyContracts.BindingModels
{
	public class FurnitureModuleBindingModel : IFurnitureModuleModel
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public double Cost { get; set; }
		public DateTime DateCreate { get; set; }
		public int UserId { get; set; }
		public Dictionary<int, (IFurnitureModel, int)> FurnitureModuleFurnitures { get; set; } = new();
	}
}
