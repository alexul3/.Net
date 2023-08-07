using FurnitureAssemblyDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyContracts.BindingModels
{
	public class RoleBindingModel : IRoleModel
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
	}
}
