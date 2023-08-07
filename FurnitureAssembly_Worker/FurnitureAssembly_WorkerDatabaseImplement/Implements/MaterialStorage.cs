﻿using FurnitureAssembly_WorkerContracts.BindingModels;
using FurnitureAssembly_WorkerContracts.SearchModels;
using FurnitureAssembly_WorkerContracts.StorageContracts;
using FurnitureAssembly_WorkerContracts.ViewModels;
using FurnitureAssembly_WorkerDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssembly_WorkerDatabaseImplement.Implements
{
	public class MaterialStorage : IMaterialStorage
	{
		public List<MaterialViewModel> GetFullList()
		{
			using var context = new FurnitureAssemblyDatabase();
			return context.Materials
				.Include(x => x.User)
				.Include(x => x.Scope)
				.Select(x => x.GetViewModel)
				.ToList();
		}
		public List<MaterialViewModel> GetFilteredList(MaterialSearchModel model)
		{
			using var context = new FurnitureAssemblyDatabase();
			if (!string.IsNullOrEmpty(model.Name))
			{
				return context.Materials
					.Include(x => x.User)
					.Include(x => x.Scope)
					.Where(x => x.Name.Contains(model.Name))
					.Select(x => x.GetViewModel).ToList();
			}
			else if (model.UserId.HasValue)
			{
				return context.Materials
					.Include(x => x.User)
					.Include(x => x.Scope)
					.Where(x => x.UserId == model.UserId)
					.Select(x => x.GetViewModel).ToList();
			}
			else
			{
				return new();
			}
		}
		public MaterialViewModel? GetElement(MaterialSearchModel model)
		{
			if (string.IsNullOrEmpty(model.Name) && !model.Id.HasValue)
			{
				return null;
			}
			using var context = new FurnitureAssemblyDatabase();
			return context.Materials
				.Include(x => x.User)
				.Include(x => x.Scope)
				.FirstOrDefault(x => (!string.IsNullOrEmpty(model.Name) && x.Name == model.Name) ||
				(model.Id.HasValue && x.Id == model.Id))
				?.GetViewModel;
		}
		public MaterialViewModel? Insert(MaterialBindingModel model)
		{
			var newMaterial = Material.Create(model);
			if (newMaterial == null)
			{
				return null;
			}
			using var context = new FurnitureAssemblyDatabase();
			context.Materials.Add(newMaterial);
			context.SaveChanges();
			return newMaterial.GetViewModel;
		}
		public MaterialViewModel? Update(MaterialBindingModel model)
		{
			using var context = new FurnitureAssemblyDatabase();
			var material = context.Materials
				.Include(x => x.User)
				.FirstOrDefault(x => x.Id == model.Id);
			if (material == null)
			{
				return null;
			}
			material.Update(model);
			context.SaveChanges();
			return material.GetViewModel;
		}
		public MaterialViewModel? Delete(MaterialBindingModel model)
		{
			using var context = new FurnitureAssemblyDatabase();
			var element = context.Materials
				.Include(x => x.User)
				.FirstOrDefault(rec => rec.Id == model.Id);
			if (element != null)
			{
				context.Materials.Remove(element);
				context.SaveChanges();
				return element.GetViewModel;
			}
			return null;
		}
	}
}
