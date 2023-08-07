using FurnitureAssemblyContracts.BindingModels;
using FurnitureAssemblyContracts.SearchModels;
using FurnitureAssemblyContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyContracts.BusinessLogicContracts
{
    public interface IOrderInfoLogic
    {
        List<OrderInfoViewModel>? ReadList(OrderInfoSearchModel? model);
        OrderInfoViewModel? ReadElement(OrderInfoSearchModel model);
        OrderInfoViewModel? Create(OrderInfoBindingModel model);
        bool Update(OrderInfoBindingModel model);
        bool Delete(OrderInfoBindingModel model);
    }
}
