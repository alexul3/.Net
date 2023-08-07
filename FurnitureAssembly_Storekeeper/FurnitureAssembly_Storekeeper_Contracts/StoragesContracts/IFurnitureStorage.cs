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
    public interface IFurnitureStorage
    {
        List<FurnitureViewModel> GetFullList();
        List<FurnitureViewModel> GetFilteredList(FurnitureSearchModel model);
        FurnitureViewModel? GetElement(FurnitureSearchModel model);
        FurnitureViewModel? Insert(FurnitureBindingModel model);
        FurnitureViewModel? Update(FurnitureBindingModel model);
        FurnitureViewModel? Delete(FurnitureBindingModel model);
    }
}
