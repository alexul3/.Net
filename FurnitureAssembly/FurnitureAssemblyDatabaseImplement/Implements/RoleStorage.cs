using FurnitureAssemblyContracts.BindingModels;
using FurnitureAssemblyContracts.SearchModels;
using FurnitureAssemblyContracts.StorageContracts;
using FurnitureAssemblyContracts.ViewModels;
using FurnitureAssemblyDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyDatabaseImplement.Implements
{
    public class RoleStorage : IRoleStorage
    {
        public RoleViewModel? Delete(RoleBindingModel model)
        {
            using var context = new FurnitureAssemblyDatabase();
            var element = context.Roles.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                var deletedElement = context.Roles
                    .Include(x => x.Users)
                    .FirstOrDefault(x => x.Id == model.Id)
                    ?.GetViewModel;
                context.Roles.Remove(element);
                context.SaveChanges();
                return deletedElement;
            }
            return null;
        }
        public RoleViewModel? GetElement(RoleSearchModel model)
        {
            if (!model.Id.HasValue)
            {
                return null;
            }
            using var context = new FurnitureAssemblyDatabase();
            return context.Roles
                .Include(x => x.Users)
                .FirstOrDefault(x => model.Id.HasValue && x.Id == model.Id)
                ?.GetViewModel;
        }
        public List<RoleViewModel> GetFilteredList(RoleSearchModel model)
        {
            using var context = new FurnitureAssemblyDatabase();
            if (model.Id.HasValue)
            {
                return context.Roles
                    .Include(x => x.Users)
                    .Where(x => x.Id == model.Id)
                    .Select(x => x.GetViewModel)
                    .ToList();
            }
            else
            {
                return new();
            }
        }
        public List<RoleViewModel> GetFullList()
        {
            using var context = new FurnitureAssemblyDatabase();
            return context.Roles
                .Include(x => x.Users)
                .Select(x => x.GetViewModel)
                .ToList();
        }
        public RoleViewModel? Insert(RoleBindingModel model)
        {
            var newRole = Role.Create(model);
            if (newRole == null)
            {
                return null;
            }
            using var context = new FurnitureAssemblyDatabase();
            context.Roles.Add(newRole);
            context.SaveChanges();
            return context.Roles
                .Include(x => x.Users)
                .FirstOrDefault(x => x.Id == newRole.Id)
                ?.GetViewModel;
        }
        public RoleViewModel? Update(RoleBindingModel model)
        {
            using var context = new FurnitureAssemblyDatabase();
            var role = context.Roles.FirstOrDefault(x => x.Id == model.Id);
            if (role == null)
            {
                return null;
            }
            role.Update(model);
            context.SaveChanges();
            return context.Roles
                .Include(x => x.Users)
                .FirstOrDefault(x => x.Id == model.Id)
                ?.GetViewModel;
        }
    }
}
