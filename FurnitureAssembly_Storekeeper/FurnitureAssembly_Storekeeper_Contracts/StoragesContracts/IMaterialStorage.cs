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
    public interface IMaterialStorage
    {
        List<MaterialViewModel> GetFullList();
        List<MaterialViewModel> GetFilteredList(MaterialSearchModel model);
        MaterialViewModel? GetElement(MaterialSearchModel model);
        MaterialViewModel? Insert(MaterialBindingModel model);
        MaterialViewModel? Update(MaterialBindingModel model);
        MaterialViewModel? Delete(MaterialBindingModel model);
    }
}
