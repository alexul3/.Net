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
    public interface IMaterialLogic
    {
        List<MaterialViewModel>? ReadList(MaterialSearchModel? model);
        MaterialViewModel? ReadElement(MaterialSearchModel model);
        bool Create(MaterialBindingModel model);
        bool Update(MaterialBindingModel model);
        bool Delete(MaterialBindingModel model);
    }
}
