using FurnitureAssemblyContracts.BindingModels;
using FurnitureAssemblyContracts.SearchModels;
using FurnitureAssemblyContracts.StorageContracts;
using FurnitureAssemblyContracts.ViewModels;
using Microsoft.EntityFrameworkCore;
using FurnitureAssemblyDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyDatabaseImplement.Implements
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
					.Include(x => x.OrderInfo)
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
            using var context = new FurnitureAssemblyDatabase();
            if (model.Id.HasValue)
			{
                return context.Orders
                .Include(x => x.Set)
                .Include(x => x.OrderInfo)
                .FirstOrDefault(x => model.Id.HasValue && x.Id == model.Id)
                ?.GetViewModel;
            }
			else if (model.OrderInfoId != null && model.OrderInfoId.Count == 1 && model.SetId.HasValue)
			{
                return context.Orders
                .Include(x => x.Set)
                .Include(x => x.OrderInfo)
                .FirstOrDefault(x => model.OrderInfoId.Contains(x.OrderInfoId) && x.SetId == model.SetId)
                ?.GetViewModel;
            }
			else
			{
				return null;
			}			
		}
		public List<OrderViewModel> GetFilteredList(OrderSearchModel model)
		{
			using var context = new FurnitureAssemblyDatabase();
			if (model.Id.HasValue)
			{
				return context.Orders
					.Include(x => x.Set)
                    .Include(x => x.OrderInfo)
                    .Where(x => x.Id == model.Id)
					.Select(x => x.GetViewModel)
					.ToList();
			}
			else if (model.SetId.HasValue && model.OrderInfoId != null && model.OrderInfoId.Count > 0)
			{
				return context.Orders
                    .Include(x => x.Set)
                    .Include(x => x.OrderInfo)
                    .Where(x => x.SetId == model.SetId && model.OrderInfoId.Contains(x.OrderInfoId))
					.Select(x => x.GetViewModel)
					.ToList();
			}
            else if (model.SetId.HasValue)
            {
                return context.Orders
                    .Include(x => x.Set)
                    .Include(x => x.OrderInfo)
                    .Where(x => x.SetId == model.SetId)
                    .Select(x => x.GetViewModel)
                    .ToList();
            }
            else if (model.OrderInfoId != null && model.OrderInfoId.Count > 0)
            {
                return context.Orders
                    .Include(x => x.Set)
                    .Include(x => x.OrderInfo)
                    .Where(x => model.OrderInfoId.Contains(x.OrderInfoId))
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
                .Include(x => x.OrderInfo)
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
                .Include(x => x.OrderInfo)
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
                .Include(x => x.OrderInfo)
                .FirstOrDefault(x => x.Id == model.Id)
				?.GetViewModel;
		}
	}
}
