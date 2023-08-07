using FurnitureAssembly_WorkerContracts.BindingModels;
using FurnitureAssembly_WorkerContracts.ViewModels;
using FurnitureAssembly_WorkerDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssembly_WorkerDatabaseImplement.Models
{
	public class Role : IRoleModel
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; } = string.Empty;
		[ForeignKey("RoleId")]
		public virtual List<User> Users { get; set; } = new();
		public static Role? Create(RoleBindingModel? model)
		{
			if (model == null)
			{
				return null;
			}
			return new Role()
			{
				Id = model.Id,
				Name = model.Name
			};
		}
		public void Update(RoleBindingModel? model)
		{
			if (model == null)
			{
				return;
			}
			Name = model.Name;
		}
		public RoleViewModel GetViewModel => new()
		{
			Id = Id,
			Name = Name
		};
	}
}
