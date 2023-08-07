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
    public interface IFurnitureLogic
    {
        List<FurnitureViewModel>? ReadList(FurnitureSearchModel? model);
        FurnitureViewModel? ReadElement(FurnitureSearchModel model);
        bool Create(FurnitureBindingModel model);
        bool Update(FurnitureBindingModel model);
        bool Delete(FurnitureBindingModel model);
    }
}
