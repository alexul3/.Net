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
	public interface IFurnitureModuleLogic
	{
		List<FurnitureModuleViewModel>? ReadList(FurnitureModuleSearchModel? model);
		FurnitureModuleViewModel? ReadElement(FurnitureModuleSearchModel model);
		bool Create(FurnitureModuleBindingModel model);
		bool Update(FurnitureModuleBindingModel model);
		bool Delete(FurnitureModuleBindingModel model);
	}
}
