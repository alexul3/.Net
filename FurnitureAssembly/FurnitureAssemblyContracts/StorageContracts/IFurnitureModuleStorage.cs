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
	public interface IFurnitureModuleStorage
	{
		List<FurnitureModuleViewModel> GetFullList();
		List<FurnitureModuleViewModel> GetFilteredList(FurnitureModuleSearchModel model);
		FurnitureModuleViewModel? GetElement(FurnitureModuleSearchModel model);
		FurnitureModuleViewModel? Insert(FurnitureModuleBindingModel model);
		FurnitureModuleViewModel? Update(FurnitureModuleBindingModel model);
		FurnitureModuleViewModel? Delete(FurnitureModuleBindingModel model);
	}
}
