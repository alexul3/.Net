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
	public interface ISetStorage
	{
		List<SetViewModel> GetFullList();
		List<SetViewModel> GetFilteredList(SetSearchModel model);
		SetViewModel? GetElement(SetSearchModel model);
		SetViewModel? Insert(SetBindingModel model);
		SetViewModel? Update(SetBindingModel model);
		SetViewModel? Delete(SetBindingModel model);
	}
}
