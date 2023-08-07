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
	public interface IScopeStorage
	{
		List<ScopeViewModel> GetFullList();
		List<ScopeViewModel> GetFilteredList(ScopeSearchModel model);
		ScopeViewModel? GetElement(ScopeSearchModel model);
		ScopeViewModel? Insert(ScopeBindingModel model);
		ScopeViewModel? Update(ScopeBindingModel model);
		ScopeViewModel? Delete(ScopeBindingModel model);
	}
}
