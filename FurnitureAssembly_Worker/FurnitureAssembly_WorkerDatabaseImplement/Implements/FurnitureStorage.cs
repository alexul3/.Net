﻿using FurnitureAssembly_WorkerContracts.BindingModels;
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
	public class FurnitureStorage : IFurnitureStorage
	{
		public List<FurnitureViewModel> GetFullList()
		{
			using var context = new FurnitureAssemblyDatabase();
			return context.Furnitures
				.Include(x => x.User)
				.Include(x => x.Materials)
				.ThenInclude(x => x.Material).ToList()
				.Select(x => x.GetViewModel).ToList();
		}
		public List<FurnitureViewModel> GetFilteredList(FurnitureSearchModel model)
		{
			using var context = new FurnitureAssemblyDatabase();
			if (!string.IsNullOrEmpty(model.Name))
			{
				return context.Furnitures
					.Include(x => x.User)
					.Include(x => x.Materials)
					.ThenInclude(x => x.Material)
					.Where(x => x.Name.Contains(model.Name)).ToList()
					.Select(x => x.GetViewModel).ToList();
			}
			else if (model.UserId.HasValue)
			{
				return context.Furnitures
					.Include(x => x.User)
					.Include(x => x.Materials)
					.ThenInclude(x => x.Material)
					.Where(x => x.UserId == model.UserId)
					.Select(x => x.GetViewModel).ToList();
			}
			else
			{
				return new();
			}
		}
		public FurnitureViewModel? GetElement(FurnitureSearchModel model)
		{
			if (string.IsNullOrEmpty(model.Name) &&
			!model.Id.HasValue)
			{
				return null;
			}
			using var context = new FurnitureAssemblyDatabase();
			return context.Furnitures.Include(x => x.User)
				.Include(x => x.Materials).ThenInclude(x => x.Material)
				.FirstOrDefault(x => (!string.IsNullOrEmpty(model.Name) && x.Name == model.Name) || (model.Id.HasValue && x.Id == model.Id))?.GetViewModel;
		}
		public FurnitureViewModel? Insert(FurnitureBindingModel model)
		{
			using var context = new FurnitureAssemblyDatabase();
			var newFurniture = Furniture.Create(context, model);
			if (newFurniture == null)
			{
				return null;
			}
			context.Furnitures.Add(newFurniture);
			context.SaveChanges();
			return newFurniture.GetViewModel;
		}
		public FurnitureViewModel? Update(FurnitureBindingModel model)
		{
			using var context = new FurnitureAssemblyDatabase();
			using var transaction = context.Database.BeginTransaction();
			try
			{
				var furniture = context.Furnitures
					.Include(x => x.User)
					.FirstOrDefault(rec => rec.Id == model.Id);
				if (furniture == null)
				{
					return null;
				}
				furniture.Update(model);
				context.SaveChanges();
				furniture.UpdateMaterials(context, model);
				transaction.Commit();
				return furniture.GetViewModel;
			}
			catch
			{
				transaction.Rollback();
				throw;
			}
		}
		public FurnitureViewModel? Delete(FurnitureBindingModel model)
		{
			using var context = new FurnitureAssemblyDatabase();
			var element = context.Furnitures
				.Include(x => x.User)
				.Include(x => x.Materials)
				.FirstOrDefault(rec => rec.Id == model.Id);
			if (element != null)
			{
				context.Furnitures.Remove(element);
				context.SaveChanges();
				return element.GetViewModel;
			}
			return null;
		}
	}
}
