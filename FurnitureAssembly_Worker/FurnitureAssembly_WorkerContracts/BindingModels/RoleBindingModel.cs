using FurnitureAssembly_WorkerDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssembly_WorkerContracts.BindingModels
{
	public class RoleBindingModel : IRoleModel
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
	}
}
