using FurnitureAssemblyContracts.BindingModels;
using FurnitureAssemblyContracts.SearchModels;
using FurnitureAssemblyContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyContracts.StorageContracts
{
    public interface IOrderInfoStorage
    {
        List<OrderInfoViewModel> GetFullList();
        List<OrderInfoViewModel> GetFilteredList(OrderInfoSearchModel model);
        OrderInfoViewModel? GetElement(OrderInfoSearchModel model);
        OrderInfoViewModel? Insert(OrderInfoBindingModel model);
        OrderInfoViewModel? Update(OrderInfoBindingModel model);
        OrderInfoViewModel? Delete(OrderInfoBindingModel model);
    }
}
