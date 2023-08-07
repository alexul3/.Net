using FurnitureAssemblyDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyContracts.ViewModels
{
	public class MaterialViewModel : IMaterialModel
	{
		public int Id { get; set; }
		[DisplayName("Название материала")]
		public string Name { get; set; } = string.Empty;
		[DisplayName("Стоимость материала")]
		public double Cost { get; set; }
		public int ScopeId { get; set; }
		[DisplayName("Область применения")]
		public string ScopeName { get; set; } = string.Empty;
		public int UserId { get; set; }
		[DisplayName("Изготовитель")]
		public string UserName { get; set; } = string.Empty;
	}
}
