using FurnitureAssembly_WorkerContracts.BindingModels;
using FurnitureAssembly_WorkerContracts.ViewModels;
using FurnitureAssembly_WorkerDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssembly_WorkerDatabaseImplement.Models
{
	public class FurnitureModule : IFurnitureModuleModel
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; } = string.Empty;
		[Required]
		public double Cost { get; set; }
		[Required]
		public DateTime DateCreate { get; set; }
		[Required]
		public int UserId { get; set; }
		public virtual User User { get; set; }
		public Dictionary<int, (IFurnitureModel, int)>? _furnitureModuleFurnitures = null;
		[NotMapped]
		public Dictionary<int, (IFurnitureModel, int)> FurnitureModuleFurnitures
		{
			get
			{
				if (_furnitureModuleFurnitures == null)
				{
					_furnitureModuleFurnitures = Furnitures
						.ToDictionary(recPC => recPC.FurnitureId, recPC =>
						(recPC.Furniture as IFurnitureModel, recPC.Count));
				}
				return _furnitureModuleFurnitures;
			}
		}
		[ForeignKey("FurnitureModuleId")]
		public virtual List<FurnitureModuleFurniture> Furnitures { get; set; } = new();
		[ForeignKey("FurnitureModuleId")]
		public virtual List<SetFurnitureModule> Sets { get; set; } = new();
		public static FurnitureModule Create(FurnitureAssemblyDatabase context, FurnitureModuleBindingModel model)
		{
			if (model == null)
			{
				return null;
			}
			return new FurnitureModule()
			{
				Id = model.Id,
				Name = model.Name,
				Cost = model.Cost,
				DateCreate = model.DateCreate,
				UserId = model.UserId,
				Furnitures = model.FurnitureModuleFurnitures.Select(x => new FurnitureModuleFurniture
				{
					Furniture = context.Furnitures.First(y => y.Id == x.Key),
					Count = x.Value.Item2
				}).ToList()
			};
		}
		public void Update(FurnitureModuleBindingModel model)
		{
			if (model == null)
			{
				return;
			}
			Name = model.Name;
			Cost = model.Cost;
		}
		public FurnitureModuleViewModel GetViewModel => new()
		{
			Id = Id,
			Name = Name,
			Cost = Cost,
			DateCreate = DateCreate,
			UserId = UserId,
			UserName = User.Name,
			FurnitureModuleFurnitures = FurnitureModuleFurnitures
		};
		public void UpdateFurnitures(FurnitureAssemblyDatabase context, FurnitureModuleBindingModel model)
		{
			var furnitureModuleFurnitures = context.FurnitureModuleFurnitures.Where(rec =>
				rec.FurnitureModuleId == model.Id).ToList();
			if (furnitureModuleFurnitures != null && furnitureModuleFurnitures.Count > 0)
			{ // удалили те, которых нет в модели
				context.FurnitureModuleFurnitures.RemoveRange(furnitureModuleFurnitures.Where(rec
					=> !model.FurnitureModuleFurnitures.ContainsKey(rec.FurnitureId)));
				context.SaveChanges();
				// обновили количество у существующих записей
				foreach (var updateFurniture in furnitureModuleFurnitures)
				{
					updateFurniture.Count = model.FurnitureModuleFurnitures[updateFurniture.FurnitureId].Item2;
					model.FurnitureModuleFurnitures.Remove(updateFurniture.FurnitureId);
				}
				context.SaveChanges();
			}
			var furnitureModule = context.FurnitureModules.First(x => x.Id == Id);
			foreach (var pc in model.FurnitureModuleFurnitures)
			{
				context.FurnitureModuleFurnitures.Add(new FurnitureModuleFurniture
				{
					FurnitureModule = furnitureModule,
					Furniture = context.Furnitures.First(x => x.Id == pc.Key),
					Count = pc.Value.Item2
				});
				context.SaveChanges();
			}
			_furnitureModuleFurnitures = null;
		}

	}
}
