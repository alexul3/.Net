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
	public interface IFurnitureLogic
	{
		List<FurnitureViewModel>? ReadList(FurnitureSearchModel? model);
		FurnitureViewModel? ReadElement(FurnitureSearchModel model);
		bool Create(FurnitureBindingModel model);
		bool Update(FurnitureBindingModel model);
		bool Delete(FurnitureBindingModel model);
	}
}
