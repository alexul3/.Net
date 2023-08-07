using FurnitureAssembly_Storekeeper_Contracts.BindingModels;
using FurnitureAssembly_Storekeeper_Contracts.ViewModels;
using FurnitureAssembly_Storekeeper_DataModels.Models;
using FurnitureAssemblyStorekeeperDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyStorekeeperDatabaseImplement.Models
{
	public class Material : IMaterial
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; } = string.Empty;
		[Required]
		public double Cost { get; set; }
		[Required]
		public int ScopeId { get; set; }
		public virtual IScope Scope { get; set; }
		[Required]
		public int UserId { get; set; }
		public virtual User User { get; set; }
		[ForeignKey("MaterialId")]
		public virtual List<FurnitureMaterial> FurnitureMaterials { get; set; } = new();
		public static Material? Create(MaterialBindingModel model)
		{
			if (model == null)
			{
				return null;
			}
			return new Material()
			{
				Id = model.Id,
				Name = model.Name,
				Cost = model.Cost,
				ScopeId = model.Scope.Id
			};
		}
		public void Update(MaterialBindingModel model)
		{
			if (model == null)
			{
				return;
			}
			Name = model.Name;
			Cost = model.Cost;
			ScopeId = model.Scope.Id;
		}
		public MaterialViewModel GetViewModel => new()
		{
			Id = Id,
			Name = Name,
			Cost = Cost,
			Scope = Scope
		};

    }
}
