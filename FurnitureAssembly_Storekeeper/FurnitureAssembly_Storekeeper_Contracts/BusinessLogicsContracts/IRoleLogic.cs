using FurnitureAssembly_Storekeeper_Contracts.BindingModels;
using FurnitureAssembly_Storekeeper_Contracts.SearchModels;
using FurnitureAssembly_Storekeeper_Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssembly_Storekeeper_Contracts.BusinessLogicsContracts
{
    public interface IRoleLogic
    {
        List<RoleViewModel>? ReadList(RoleSearchModel? model);
        RoleViewModel? ReadElement(RoleSearchModel model);
        bool Create(RoleBindingModel model);
        bool Update(RoleBindingModel model);
        bool Delete(RoleBindingModel model);
    }
}
