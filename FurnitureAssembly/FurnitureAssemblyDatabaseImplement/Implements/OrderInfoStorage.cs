using FurnitureAssemblyContracts.BindingModels;
using FurnitureAssemblyContracts.SearchModels;
using FurnitureAssemblyContracts.StorageContracts;
using FurnitureAssemblyContracts.ViewModels;
using FurnitureAssemblyDatabaseImplement.Models;
using FurnitureAssemblyDataModels.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyDatabaseImplement.Implements
{
    public class OrderInfoStorage : IOrderInfoStorage
    {
        public OrderInfoViewModel? Delete(OrderInfoBindingModel model)
        {
            using var context = new FurnitureAssemblyDatabase();
            var element = context.OrderInfos.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                var deletedElement = context.OrderInfos
                    .Include(x => x.User)
                    .FirstOrDefault(x => x.Id == model.Id)
                    ?.GetViewModel;
                context.OrderInfos.Remove(element);
                context.SaveChanges();
                return deletedElement;
            }
            return null;
        }
        public OrderInfoViewModel? GetElement(OrderInfoSearchModel model)
        {
            if (!model.Id.HasValue)
            {
                return null;
            }
            using var context = new FurnitureAssemblyDatabase();
            return context.OrderInfos
                .Include(x => x.User)
                .FirstOrDefault(x => model.Id.HasValue && x.Id == model.Id)
                ?.GetViewModel;
        }
        public List<OrderInfoViewModel> GetFilteredList(OrderInfoSearchModel model)
        {
            using var context = new FurnitureAssemblyDatabase();
            if (model.Id.HasValue)
            {
                return context.OrderInfos
                    .Include(x => x.User)
                    .Where(x => x.Id == model.Id)
                    .Select(x => x.GetViewModel)
                    .ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.UserId.HasValue)
            {
                return context.OrderInfos
                    .Include(x => x.User)
                    .Where(x => x.DateCreate >= model.DateFrom.Value.ToUniversalTime() && x.DateCreate <= model.DateTo.Value.ToUniversalTime() && model.UserId == x.UserId)
                    .Select(x => x.GetViewModel)
                    .ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null)
            {
                return context.OrderInfos
                    .Include(x => x.User)
                    .Where(x => x.DateCreate >= model.DateFrom.Value.ToUniversalTime() && x.DateCreate <= model.DateTo.Value.ToUniversalTime())
                    .Select(x => x.GetViewModel)
                    .ToList();
            }
            else if (model.UserId.HasValue)
            {
                return context.OrderInfos
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
        public List<OrderInfoViewModel> GetFullList()
        {
            using var context = new FurnitureAssemblyDatabase();
            return context.OrderInfos
                .Include(x => x.User)
                .Select(x => x.GetViewModel)
                .ToList();
        }
        public OrderInfoViewModel? Insert(OrderInfoBindingModel model)
        {
            var newOrderInfo = OrderInfo.Create(model);
            if (newOrderInfo == null)
            {
                return null;
            }
            using var context = new FurnitureAssemblyDatabase();
            context.OrderInfos.Add(newOrderInfo);
            context.SaveChanges();
            return context.OrderInfos
                .Include(x => x.User)
                .FirstOrDefault(x => x.Id == newOrderInfo.Id)
                ?.GetViewModel;
        }
        public OrderInfoViewModel? Update(OrderInfoBindingModel model)
        {
            using var context = new FurnitureAssemblyDatabase();
            var orderInfo = context.OrderInfos.FirstOrDefault(x => x.Id == model.Id);
            if (orderInfo == null)
            {
                return null;
            }
            orderInfo.Update(model);
            context.SaveChanges();
            return context.OrderInfos
                .Include(x => x.User)
                .FirstOrDefault(x => x.Id == model.Id)
                ?.GetViewModel;
        }
    }
}
