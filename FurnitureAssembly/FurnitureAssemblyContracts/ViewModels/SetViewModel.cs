﻿using FurnitureAssemblyDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyContracts.ViewModels
{
	public class SetViewModel : ISetModel
	{
		public int Id { get; set; }
		[DisplayName("Название гарнитура")]
		public string Name { get; set; } = string.Empty;
		[DisplayName("Стоимость гарнитура")]
		public double Cost { get; set; }
		[DisplayName("Дата создания")]
		public DateTime DateCreate { get; set; }
		public int UserId { get; set; }
		public Dictionary<int, (IFurnitureModuleModel, int)> SetFurnitureModules { get; set; } = new();
	}
}
