using FurnitureAssemblyDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyContracts.ViewModels
{
	public class RoleViewModel : IRoleModel
	{
		public int Id { get; set; }
		[DisplayName("Название роли")]
		public string Name { get; set; } = string.Empty;
	}
}
