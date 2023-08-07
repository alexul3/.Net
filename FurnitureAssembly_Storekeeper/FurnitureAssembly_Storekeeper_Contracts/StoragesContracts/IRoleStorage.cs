using FurnitureAssembly_Storekeeper_Contracts.BindingModels;
using FurnitureAssembly_Storekeeper_Contracts.SearchModels;
using FurnitureAssembly_Storekeeper_Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssembly_Storekeeper_Contracts.StoragesContracts
{
    public interface IRoleStorage
    {
        List<RoleViewModel> GetFullList();
        List<RoleViewModel> GetFilteredList(RoleSearchModel model);
        RoleViewModel? GetElement(RoleSearchModel model);
        RoleViewModel? Insert(RoleBindingModel model);
        RoleViewModel? Update(RoleBindingModel model);
        RoleViewModel? Delete(RoleBindingModel model);
    }
}
