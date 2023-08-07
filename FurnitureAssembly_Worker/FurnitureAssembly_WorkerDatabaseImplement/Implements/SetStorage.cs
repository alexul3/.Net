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
	public class SetStorage : ISetStorage
	{
		public List<SetViewModel> GetFullList()
		{
			using var context = new FurnitureAssemblyDatabase();
			return context.Sets
				.Include(x => x.User)
				.Include(x => x.FurnitureModules)
				.ThenInclude(x => x.FurnitureModule).ToList()
				.Select(x => x.GetViewModel).ToList();
		}
		public List<SetViewModel> GetFilteredList(SetSearchModel model)
		{
			using var context = new FurnitureAssemblyDatabase();
			if (!string.IsNullOrEmpty(model.Name))
			{
				return context.Sets
					.Include(x => x.User)
					.Include(x => x.FurnitureModules)
					.ThenInclude(x => x.FurnitureModule)
					.Where(x => x.Name.Contains(model.Name)).ToList()
					.Select(x => x.GetViewModel).ToList();
			}
			else if (model.UserId.HasValue)
			{
				return context.Sets
					.Include(x => x.User)
					.Include(x => x.FurnitureModules)
					.ThenInclude(x => x.FurnitureModule)
					.Where(x => x.UserId == model.UserId)
					.Select(x => x.GetViewModel).ToList();
			}
			else
			{
				return new();
			}
		}
		public SetViewModel? GetElement(SetSearchModel model)
		{
			if (string.IsNullOrEmpty(model.Name) &&
			!model.Id.HasValue)
			{
				return null;
			}
			using var context = new FurnitureAssemblyDatabase();
			return context.Sets
				.Include(x => x.User)
				.Include(x => x.FurnitureModules)
				.ThenInclude(x => x.FurnitureModule)
				.FirstOrDefault(x => (!string.IsNullOrEmpty(model.Name) && x.Name == model.Name) || (model.Id.HasValue && x.Id == model.Id))
				?.GetViewModel;
		}
		public SetViewModel? Insert(SetBindingModel model)
		{
			using var context = new FurnitureAssemblyDatabase();
			var newSet = Set.Create(context, model);
			if (newSet == null)
			{
				return null;
			}
			context.Sets.Add(newSet);
			context.SaveChanges();
			return newSet.GetViewModel;
		}
		public SetViewModel? Update(SetBindingModel model)
		{
			using var context = new FurnitureAssemblyDatabase();
			using var transaction = context.Database.BeginTransaction();
			try
			{
				var set = context.Sets
					.Include(x => x.User)
					.FirstOrDefault(rec => rec.Id == model.Id);
				if (set == null)
				{
					return null;
				}
				set.Update(model);
				context.SaveChanges();
				set.UpdateFurnitureModules(context, model);
				transaction.Commit();
				return set.GetViewModel;
			}
			catch
			{
				transaction.Rollback();
				throw;
			}
		}
		public SetViewModel? Delete(SetBindingModel model)
		{
			using var context = new FurnitureAssemblyDatabase();
			var element = context.Sets
				.Include(x => x.User)
				.Include(x => x.FurnitureModules)
				.FirstOrDefault(rec => rec.Id == model.Id);
			if (element != null)
			{
				context.Sets.Remove(element);
				context.SaveChanges();
				return element.GetViewModel;
			}
			return null;
		}
	}
}
