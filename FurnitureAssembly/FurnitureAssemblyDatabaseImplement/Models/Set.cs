using FurnitureAssemblyContracts.BindingModels;
using FurnitureAssemblyContracts.ViewModels;
using FurnitureAssemblyDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyDatabaseImplement.Models
{
	public class Set : ISetModel
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
		public Dictionary<int, (IFurnitureModuleModel, int)>? _setFurnitureModules = null;
		[NotMapped]
		public Dictionary<int, (IFurnitureModuleModel, int)> SetFurnitureModules
		{ 
			get
			{
				if (_setFurnitureModules == null)
				{
					_setFurnitureModules = FurnitureModules
						.ToDictionary(recPC => recPC.FurnitureModuleId, recPC =>
						(recPC.FurnitureModule as IFurnitureModuleModel, recPC.Count));
				}
				return _setFurnitureModules;
			}
		}
		[ForeignKey("SetId")]
		public virtual List<SetFurnitureModule> FurnitureModules { get; set; } = new();
		[ForeignKey("SetId")]
		public virtual List<Order> Orders { get; set; } = new();
		public virtual User User { get; set; }
		public static Set Create(FurnitureAssemblyDatabase context, SetBindingModel model)
		{
			return new Set()
			{
				Id = model.Id,
				Name = model.Name,
				Cost = model.Cost,
				DateCreate = DateTime.Now.ToUniversalTime(),
				UserId = model.UserId,
				FurnitureModules = model.SetFurnitureModules.Select(x => new SetFurnitureModule
				{
					FurnitureModule = context.FurnitureModules.First(y => y.Id == x.Key),
					Count = x.Value.Item2
				}).ToList()
			};
		}
		public void Update(SetBindingModel model)
		{
			if (model == null)
			{
				return;
			}
			Name = model.Name;
			Cost = model.Cost;
			DateCreate = model.DateCreate;
		}
		public SetViewModel GetViewModel => new()
		{
			Id = Id,
			Name = Name,
			Cost = Cost,
			DateCreate = DateCreate,
			UserId = UserId,
			SetFurnitureModules = SetFurnitureModules
		};
		public void UpdateFurnitureModules(FurnitureAssemblyDatabase context, SetBindingModel model)
		{
			var setFurnitureModules = context.SetFurnitureModules.Where(rec =>
				rec.SetId == model.Id).ToList();
			if (setFurnitureModules != null && setFurnitureModules.Count > 0)
			{ // удалили те, которых нет в модели
				context.SetFurnitureModules.RemoveRange(setFurnitureModules.Where(rec
					=> !model.SetFurnitureModules.ContainsKey(rec.FurnitureModuleId)));
				context.SaveChanges();
				// обновили количество у существующих записей
				foreach (var updateFurnitureModule in setFurnitureModules)
				{
					updateFurnitureModule.Count = model.SetFurnitureModules[updateFurnitureModule.FurnitureModuleId].Item2;
					model.SetFurnitureModules.Remove(updateFurnitureModule.FurnitureModuleId);
				}
				context.SaveChanges();
			}
			var set = context.Sets.First(x => x.Id == Id);
			foreach (var pc in model.SetFurnitureModules)
			{
				context.SetFurnitureModules.Add(new SetFurnitureModule
				{
					Set = set,
					FurnitureModule = context.FurnitureModules.First(x => x.Id == pc.Key),
					Count = pc.Value.Item2
				});
				context.SaveChanges();
			}
			_setFurnitureModules = null;
		}
	}
}
