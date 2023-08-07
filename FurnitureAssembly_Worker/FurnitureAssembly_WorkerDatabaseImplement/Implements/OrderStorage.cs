using FurnitureAssembly_WorkerContracts.BindingModels;
using FurnitureAssembly_WorkerContracts.SearchModels;
using FurnitureAssembly_WorkerContracts.StorageContracts;
using FurnitureAssembly_WorkerContracts.ViewModels;
using Microsoft.EntityFrameworkCore;
using FurnitureAssembly_WorkerDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssembly_WorkerDatabaseImplement.Implements
{
	public class OrderStorage : IOrderStorage
	{
		public OrderViewModel? Delete(OrderBindingModel model)
		{
			using var context = new FurnitureAssemblyDatabase();
			var element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
			if (element != null)
			{
				var deletedElement = context.Orders
					.Include(x => x.Set)
					.Include(x => x.User)
					.FirstOrDefault(x => x.Id == model.Id)
					?.GetViewModel;
				context.Orders.Remove(element);
				context.SaveChanges();
				return deletedElement;
			}
			return null;
		}
		public OrderViewModel? GetElement(OrderSearchModel model)
		{
			if (!model.Id.HasValue)
			{
				return null;
			}
			using var context = new FurnitureAssemblyDatabase();
			return context.Orders
				.Include(x => x.Set)
				.Include(x => x.User)
				.FirstOrDefault(x => model.Id.HasValue && x.Id == model.Id)
				?.GetViewModel;
		}
		public List<OrderViewModel> GetFilteredList(OrderSearchModel model)
		{
			using var context = new FurnitureAssemblyDatabase();
			if (model.Id.HasValue)
			{
				return context.Orders
					.Include(x => x.Set)
					.Include(x => x.User)
					.Where(x => x.Id == model.Id)
					.Select(x => x.GetViewModel)
					.ToList();
			}
			else if (model.DateFrom != null && model.DateTo != null)
			{
				return context.Orders
					.Include(x => x.Set)
					.Include(x => x.User)
					.Where(x => x.DateCreate >= model.DateFrom && x.DateCreate <= model.DateTo)
					.Select(x => x.GetViewModel)
					.ToList();
			}
			else if (model.UserId.HasValue)
			{
				return context.Orders
					.Include(x => x.Set)
					.Include(x => x.User)
					.Where(x => x.UserId == model.UserId)
					.Select(x => x.GetViewModel)
					.ToList();
			}
			else
			{
				return new();
			}
		}
		public List<OrderViewModel> GetFullList()
		{
			using var context = new FurnitureAssemblyDatabase();
			return context.Orders
				.Include(x => x.Set)
				.Include(x => x.User)
				.Select(x => x.GetViewModel)
				.ToList();
		}
		public OrderViewModel? Insert(OrderBindingModel model)
		{
			var newOrder = Order.Create(model);
			if (newOrder == null)
			{
				return null;
			}
			using var context = new FurnitureAssemblyDatabase();
			context.Orders.Add(newOrder);
			context.SaveChanges();
			return context.Orders
				.Include(x => x.Set)
				.Include(x => x.User)
				.FirstOrDefault(x => x.Id == newOrder.Id)
				?.GetViewModel;
		}
		public OrderViewModel? Update(OrderBindingModel model)
		{
			using var context = new FurnitureAssemblyDatabase();
			var order = context.Orders.FirstOrDefault(x => x.Id == model.Id);
			if (order == null)
			{
				return null;
			}
			order.Update(model);
			context.SaveChanges();
			return context.Orders
				.Include(x => x.Set)
				.Include(x => x.User)
				.FirstOrDefault(x => x.Id == model.Id)
				?.GetViewModel;
		}
	}
}
