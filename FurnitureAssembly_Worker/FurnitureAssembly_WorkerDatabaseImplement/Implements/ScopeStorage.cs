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
	public class ScopeStorage : IScopeStorage
	{
		public List<ScopeViewModel> GetFullList()
		{
			using var context = new FurnitureAssemblyDatabase();
			return context.Scopes
					.Include(x => x.Materials)
					.Select(x => x.GetViewModel)
					.ToList();
		}
		public List<ScopeViewModel> GetFilteredList(ScopeSearchModel model)
		{
			if (model == null)
			{
				return new();
			}
			if (!string.IsNullOrEmpty(model.Name))
			{
				using var context = new FurnitureAssemblyDatabase();
				return context.Scopes
					.Include(x => x.Materials)
					.Where(x => x.Name.Contains(model.Name))
					.Select(x => x.GetViewModel)
					.ToList();
			}
			return new();
		}
		public ScopeViewModel? GetElement(ScopeSearchModel model)
		{
			using var context = new FurnitureAssemblyDatabase();
			if (model.Id.HasValue)
			{
				return context.Scopes
					.FirstOrDefault(x => (x.Id == model.Id))
					?.GetViewModel;
			}
			else if (!string.IsNullOrEmpty(model.Name))
			{
				return context.Scopes
					.FirstOrDefault(x => (x.Name == model.Name))
					?.GetViewModel;
			}
			return new();
		}
		public ScopeViewModel? Insert(ScopeBindingModel model)
		{
			var newScope = Scope.Create(model);
			if (newScope == null)
			{
				return null;
			}
			using var context = new FurnitureAssemblyDatabase();
			context.Scopes.Add(newScope);
			context.SaveChanges();
			return newScope.GetViewModel;
		}
		public ScopeViewModel? Update(ScopeBindingModel model)
		{
			using var context = new FurnitureAssemblyDatabase();
			var scope = context.Scopes
				.FirstOrDefault(x => x.Id == model.Id);
			if (scope == null)
			{
				return null;
			}
			scope.Update(model);
			context.SaveChanges();
			return scope.GetViewModel;
		}
		public ScopeViewModel? Delete(ScopeBindingModel model)
		{
			using var context = new FurnitureAssemblyDatabase();
			var element = context.Scopes
				.Include(x => x.Materials)
				.FirstOrDefault(rec => rec.Id == model.Id);
			if (element != null)
			{
				context.Scopes.Remove(element);
				context.SaveChanges();
				return element.GetViewModel;
			}
			return null;
		}
	}
}
