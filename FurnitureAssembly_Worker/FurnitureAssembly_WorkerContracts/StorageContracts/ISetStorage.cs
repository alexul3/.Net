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
