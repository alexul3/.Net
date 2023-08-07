using FurnitureAssembly_WorkerContracts.BindingModels;
using FurnitureAssembly_WorkerContracts.SearchModels;
using FurnitureAssembly_WorkerContracts.StorageContracts;
using FurnitureAssembly_WorkerContracts.ViewModels;
using FurnitureAssembly_WorkerDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssembly_WorkerDatabaseImplement.Implements
{
	public class FurnitureModuleStorage : IFurnitureModuleStorage
	{
		public List<FurnitureModuleViewModel> GetFullList()
		{
			using var context = new FurnitureAssemblyDatabase();
			return context.FurnitureModules
				.Include(x => x.User)
				.Include(x => x.Furnitures)
				.ThenInclude(x => x.Furniture).ToList()
				.Select(x => x.GetViewModel).ToList();
		}
		public List<FurnitureModuleViewModel> GetFilteredList(FurnitureModuleSearchModel model)
		{
			using var context = new FurnitureAssemblyDatabase();
			if (!string.IsNullOrEmpty(model.Name))
			{
				return context.FurnitureModules
					.Include(x => x.User)
					.Include(x => x.Furnitures)
					.ThenInclude(x => x.Furniture)
					.Where(x => x.Name.Contains(model.Name)).ToList()
					.Select(x => x.GetViewModel).ToList();
			}
			else if (model.UserId.HasValue)
			{
				return context.FurnitureModules
					.Include(x => x.User)
					.Include(x => x.Furnitures)
					.ThenInclude(x => x.Furniture)
					.Where(x => x.UserId == model.UserId)
					.Select(x => x.GetViewModel).ToList();
			}
			else
			{
				return new();
			}
		}
		public FurnitureModuleViewModel? GetElement(FurnitureModuleSearchModel model)
		{
			if (string.IsNullOrEmpty(model.Name) &&
			!model.Id.HasValue)
			{
				return null;
			}
			using var context = new FurnitureAssemblyDatabase();
			return context.FurnitureModules
				.Include(x => x.User)
				.Include(x => x.Furnitures)
				.ThenInclude(x => x.Furniture)
				.FirstOrDefault(x => (!string.IsNullOrEmpty(model.Name) && x.Name == model.Name) || (model.Id.HasValue && x.Id == model.Id))
				?.GetViewModel;
		}
		public FurnitureModuleViewModel? Insert(FurnitureModuleBindingModel model)
		{
			using var context = new FurnitureAssemblyDatabase();
			var newFurnitureModule = FurnitureModule.Create(context, model);
			if (newFurnitureModule == null)
			{
				return null;
			}
			context.FurnitureModules.Add(newFurnitureModule);
			context.SaveChanges();
			return newFurnitureModule.GetViewModel;
		}
		public FurnitureModuleViewModel? Update(FurnitureModuleBindingModel model)
		{
			using var context = new FurnitureAssemblyDatabase();
			using var transaction = context.Database.BeginTransaction();
			try
			{
				var furnitureModule = context.FurnitureModules.FirstOrDefault(rec =>
					rec.Id == model.Id);
				if (furnitureModule == null)
				{
					return null;
				}
				furnitureModule.Update(model);
				context.SaveChanges();
				furnitureModule.UpdateFurnitures(context, model);
				transaction.Commit();
				return furnitureModule.GetViewModel;
			}
			catch
			{
				transaction.Rollback();
				throw;
			}
		}
		public FurnitureModuleViewModel? Delete(FurnitureModuleBindingModel model)
		{
			using var context = new FurnitureAssemblyDatabase();
			var element = context.FurnitureModules
				.Include(x => x.User)
				.Include(x => x.Furnitures)
				.FirstOrDefault(rec => rec.Id == model.Id);
			if (element != null)
			{
				context.FurnitureModules.Remove(element);
				context.SaveChanges();
				return element.GetViewModel;
			}
			return null;
		}
	}
}
