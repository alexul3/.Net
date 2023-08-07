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
	public class UserStorage : IUserStorage
	{
		public UserViewModel? Delete(UserBindingModel model)
		{
			using var context = new FurnitureAssemblyDatabase();
			var element = context.Users
				.FirstOrDefault(rec => rec.Id == model.Id);
			if (element != null)
			{
				var deletedElement = context.Users
					.Include(x => x.Sets)
					.Include(x => x.Orders)
					.Include(x => x.FurnitureModules)
					.Include(x => x.Furnitures)
					.Include(x => x.Materials)
					.FirstOrDefault(x => x.Id == model.Id)
					?.GetViewModel;
				context.Users.Remove(element);
				context.SaveChanges();
				return deletedElement;
			}
			return null;
		}
		public UserViewModel? GetElement(UserSearchModel model)
		{
			if (!model.Id.HasValue)
			{
				return null;
			}
			using var context = new FurnitureAssemblyDatabase();
			return context.Users
				.Include(x => x.Sets)
				.Include(x => x.Orders)
				.Include(x => x.FurnitureModules)
				.Include(x => x.Furnitures)
				.Include(x => x.Materials)
				.FirstOrDefault(x => model.Id.HasValue && x.Id == model.Id)
				?.GetViewModel;
		}
		public List<UserViewModel> GetFilteredList(UserSearchModel model)
		{
			using var context = new FurnitureAssemblyDatabase();
			if (!string.IsNullOrEmpty(model.Login))
			{
				return context.Users
					.Include(x => x.Sets)
					.Include(x => x.Orders)
					.Include(x => x.FurnitureModules)
					.Include(x => x.Furnitures)
					.Include(x => x.Materials)
					.Where(x => x.Login.Contains(model.Login))
					.Select(x => x.GetViewModel)
					.ToList();
			}
			return new();
		}
		public List<UserViewModel> GetFullList()
		{
			using var context = new FurnitureAssemblyDatabase();
			return context.Users
				.Include(x => x.Sets)
				.Include(x => x.Orders)
				.Include(x => x.FurnitureModules)
				.Include(x => x.Furnitures)
				.Include(x => x.Materials)
				.Select(x => x.GetViewModel)
				.ToList();
		}
		public UserViewModel? Insert(UserBindingModel model)
		{
			var newUser = User.Create(model);
			if (newUser == null)
			{
				return null;
			}
			using var context = new FurnitureAssemblyDatabase();
			context.Users.Add(newUser);
			context.SaveChanges();
			return context.Users
				.Include(x => x.Sets)
				.Include(x => x.Orders)
				.Include(x => x.FurnitureModules)
				.Include(x => x.Furnitures)
				.Include(x => x.Materials)
				.FirstOrDefault(x => x.Id == newUser.Id)
				?.GetViewModel;
		}
		public UserViewModel? Update(UserBindingModel model)
		{
			using var context = new FurnitureAssemblyDatabase();
			var user = context.Users.FirstOrDefault(x => x.Id == model.Id);
			if (user == null)
			{
				return null;
			}
			user.Update(model);
			context.SaveChanges();
			return context.Users
				.Include(x => x.Sets)
				.Include(x => x.Orders)
				.Include(x => x.FurnitureModules)
				.Include(x => x.Furnitures)
				.Include(x => x.Materials)
				.FirstOrDefault(x => x.Id == model.Id)
				?.GetViewModel;
		}
	}
}
