using FurnitureAssemblyDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyContracts.ViewModels
{
	public class FurnitureViewModel : IFurnitureModel
	{
		public int Id { get; set; }
		[DisplayName("Название изделия")]
		public string Name { get; set; } = string.Empty;
		[DisplayName("Цена изделия")]
		public double Cost { get; set; }
		[DisplayName("Дата создания")]
		public DateTime DateCreate { get; set; }
		public Dictionary<int, (IMaterialModel, int)> FurnitureMaterials { get; set; } = new();
		public int UserId { get; set; }
		[DisplayName("Изготовитель")]
		public string UserName { get; set; } = string.Empty;
	}
}
