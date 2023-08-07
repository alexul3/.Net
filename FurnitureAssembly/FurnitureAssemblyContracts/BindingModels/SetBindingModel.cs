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
	public class SetBindingModel : ISetModel
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public double Cost { get; set; }
		public DateTime DateCreate { get; set; }
		public int UserId { get; set; }
		public Dictionary<int, (IFurnitureModuleModel, int)> SetFurnitureModules { get; set; } = new();
	}
}
