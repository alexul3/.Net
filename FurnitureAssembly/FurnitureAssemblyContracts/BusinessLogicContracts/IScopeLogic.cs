using FurnitureAssemblyContracts.BindingModels;
using FurnitureAssemblyContracts.SearchModels;
using FurnitureAssemblyContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyContracts.BusinessLogicContracts
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
