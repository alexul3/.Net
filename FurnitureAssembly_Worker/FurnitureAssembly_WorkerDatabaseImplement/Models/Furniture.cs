using FurnitureAssembly_WorkerContracts.BindingModels;
using FurnitureAssembly_WorkerContracts.ViewModels;
using FurnitureAssembly_WorkerDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssembly_WorkerDatabaseImplement.Models
{
	public class Furniture : IFurnitureModel
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; } = string.Empty;
		[Required]
		public double Cost { get; set; }
		[Required]
		public DateTime DateCreate { get; set; } = DateTime.Now;
		[Required]
		public int UserId { get; set; }
		public virtual User User { get; set; }
		public Dictionary<int, (IMaterialModel, int)>? _furnitureMaterials = null;
		[NotMapped]
		public Dictionary<int, (IMaterialModel, int)> FurnitureMaterials
		{
			get
			{
				if (_furnitureMaterials == null)
				{
					_furnitureMaterials = Materials
						.ToDictionary(recPC => recPC.MaterialId, recPC =>
						(recPC.Material as IMaterialModel, recPC.Count));
				}
				return _furnitureMaterials;
			}
		}
		[ForeignKey("FurnitureId")]
		public virtual List<FurnitureMaterial> Materials { get; set; } = new();
		public static Furniture Create(FurnitureAssemblyDatabase context, FurnitureBindingModel model)
		{
			return new Furniture()
			{
				Id = model.Id,
				Name = model.Name,
				Cost = model.Cost,
				UserId = model.UserId,
				DateCreate = model.DateCreate,
				Materials = model.FurnitureMaterials.Select(x => new FurnitureMaterial
				{
					Material = context.Materials.First(y => y.Id == x.Key),
					Count = x.Value.Item2
				}).ToList()
			};
		}
		public void Update(FurnitureBindingModel model)
		{
			if (model == null)
			{
				return;
			}
			Name = model.Name;
			Cost = model.Cost;
		}
		public FurnitureViewModel GetViewModel => new()
		{
			Id = Id,
			Name = Name,
			Cost = Cost,
			UserId = UserId,
			UserName = User.Name,
			DateCreate = DateCreate,
			FurnitureMaterials = FurnitureMaterials
		};
		public void UpdateMaterials(FurnitureAssemblyDatabase context, FurnitureBindingModel model)
		{
			var furnitureMaterials = context.FurnitureMaterials.Where(rec =>
				rec.FurnitureId == model.Id).ToList();
			if (furnitureMaterials != null && furnitureMaterials.Count > 0)
			{ // удалили те, которых нет в модели
				context.FurnitureMaterials.RemoveRange(furnitureMaterials.Where(rec
					=> !model.FurnitureMaterials.ContainsKey(rec.MaterialId)));
				context.SaveChanges();
				// обновили количество у существующих записей
				foreach (var updateMaterial in furnitureMaterials)
				{
					updateMaterial.Count = model.FurnitureMaterials[updateMaterial.MaterialId].Item2;
					model.FurnitureMaterials.Remove(updateMaterial.MaterialId);
				}
				context.SaveChanges();
			}
			var furniture = context.Furnitures.First(x => x.Id == Id);
			foreach (var pc in model.FurnitureMaterials)
			{
				context.FurnitureMaterials.Add(new FurnitureMaterial
				{
					Furniture = furniture,
					Material = context.Materials.First(x => x.Id == pc.Key),
					Count = pc.Value.Item2
				});
				context.SaveChanges();
			}
			_furnitureMaterials = null;
		}
	}
}
