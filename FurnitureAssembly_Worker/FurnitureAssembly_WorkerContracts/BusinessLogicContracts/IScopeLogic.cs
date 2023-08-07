using FurnitureAssembly_WorkerContracts.BindingModels;
using FurnitureAssembly_WorkerContracts.SearchModels;
using FurnitureAssembly_WorkerContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssembly_WorkerContracts.BusinessLogicContracts
{
	public interface IScopeLogic
	{
		List<ScopeViewModel>? ReadList(ScopeSearchModel? model);
		ScopeViewModel? ReadElement(ScopeSearchModel model);
		bool Create(ScopeBindingModel model);
		bool Update(ScopeBindingModel model);
		bool Delete(ScopeBindingModel model);
	}
}
