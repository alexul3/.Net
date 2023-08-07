using FurnitureAssembly_Storekeeper_Contracts.BindingModels;
using FurnitureAssembly_Storekeeper_Contracts.ViewModels;
using FurnitureAssembly_Storekeeper_DataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyStorekeeperDatabaseImplement.Models
{
	public class Role : IRole
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
