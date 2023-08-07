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
