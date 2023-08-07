using FurnitureAssembly_WorkerDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace FurnitureAssembly_WorkerContracts.ViewModels
{
	public class FurnitureModuleViewModel : IFurnitureModuleModel
	{
		public int Id { get; set; }
		[DisplayName("Название модуля")]
		public string Name { get; set; } = string.Empty;
		[DisplayName("Цена модуля")]
		public double Cost { get; set; }
		[DisplayName("Дата создания")]
		public DateTime DateCreate { get; set; }
		public int UserId { get; set; }
		[DisplayName("Изготовитель")]
		public string UserName { get; set; } = string.Empty;
		public Dictionary<int, (IFurnitureModel, int)> FurnitureModuleFurnitures { get; set; } = new();
	}
}
