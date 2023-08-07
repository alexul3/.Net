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
