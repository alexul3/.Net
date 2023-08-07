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
	public class Scope : IScope
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; } = string.Empty;
		[ForeignKey("ScopeId")]
		public virtual List<Material> Materials { get; set; } = new();
		public static Scope? Create(ScopeBindingModel? model)
		{
			if (model == null)
			{
				return null;
			}
			return new Scope()
			{
				Id = model.Id,
				Name = model.Name
			};
		}
		public void Update(ScopeBindingModel? model)
		{
			if (model == null)
			{
				return;
			}
			Name = model.Name;
		}
		public ScopeViewModel GetViewModel => new()
		{
			Id = Id,
			Name = Name
		};
	}
}
