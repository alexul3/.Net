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
    public interface IScopeLogic
    {
        List<ScopeViewModel>? ReadList(ScopeSearchModel? model);
        ScopeViewModel? ReadElement(ScopeSearchModel model);
        bool Create(ScopeBindingModel model);
        bool Update(ScopeBindingModel model);
        bool Delete(ScopeBindingModel model);
    }
}
