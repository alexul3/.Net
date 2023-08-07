using FurnitureAssemblyContracts.BindingModels;
using FurnitureAssemblyContracts.SearchModels;
using FurnitureAssemblyContracts.ViewModels;
using FurnitureAssemblyDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyContracts.BusinessLogicContracts
{
	public interface ISetLogic
	{
		List<SetViewModel>? ReadList(SetSearchModel? model);
		SetViewModel? ReadElement(SetSearchModel model);
        SetViewModel? Create(SetBindingModel model);
		bool Update(SetBindingModel model);
		bool AddFurnitureModuleInSet(SetSearchModel model, IFurnitureModuleModel furnitureModule, int count);
		bool Delete(SetBindingModel model);
	}
}
