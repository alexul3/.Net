using FurnitureAssembly_WorkerContracts.BindingModels;
using FurnitureAssembly_WorkerContracts.SearchModels;
using FurnitureAssembly_WorkerContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssembly_WorkerContracts.StorageContracts
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
