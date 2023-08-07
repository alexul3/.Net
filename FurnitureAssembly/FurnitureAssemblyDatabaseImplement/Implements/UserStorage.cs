using FurnitureAssemblyContracts.BindingModels;
using FurnitureAssemblyContracts.SearchModels;
using FurnitureAssemblyContracts.StorageContracts;
using FurnitureAssemblyContracts.ViewModels;
using FurnitureAssemblyDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyDatabaseImplement.Implements
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
					.Include(x => x.Role)
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
			using var context = new FurnitureAssemblyDatabase();
			if (model.Id.HasValue)
			{
				return context.Users
				.Include(x => x.Sets)
				.Include(x => x.Orders)
				.Include(x => x.FurnitureModules)
				.Include(x => x.Furnitures)
				.Include(x => x.Materials)
				.Include(x => x.Role)
				.FirstOrDefault(x => x.Id == model.Id)
				?.GetViewModel;
			} 
			else if (!model.Login.IsNullOrEmpty() && !model.Password.IsNullOrEmpty())
			{
				return context.Users
				.Include(x => x.Sets)
				.Include(x => x.Orders)
				.Include(x => x.FurnitureModules)
				.Include(x => x.Furnitures)
				.Include(x => x.Materials)
				.Include(x => x.Role)
				.FirstOrDefault(x => model.Login == x.Login && x.Password == model.Password)
				?.GetViewModel;
			}
			else
			{
				return null;
			}
			
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
                    .Include(x => x.Role)
                    .Where(x => x.Login.Contains(model.Login))
					.Select(x => x.GetViewModel)
					.ToList();
			}
			else if (model.RoleId != 0)
			{
                return context.Users
                    .Include(x => x.Sets)
                    .Include(x => x.Orders)
                    .Include(x => x.FurnitureModules)
                    .Include(x => x.Furnitures)
                    .Include(x => x.Materials)
                    .Include(x => x.Role)
                    .Where(x => x.RoleId == model.RoleId)
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
                .Include(x => x.Role)
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
                .Include(x => x.Role)
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
                .Include(x => x.Role)
                .FirstOrDefault(x => x.Id == model.Id)
				?.GetViewModel;
		}
	}
}
