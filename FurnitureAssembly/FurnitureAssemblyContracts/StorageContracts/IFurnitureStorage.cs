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
