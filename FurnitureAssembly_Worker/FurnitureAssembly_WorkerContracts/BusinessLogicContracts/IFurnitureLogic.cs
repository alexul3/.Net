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
	public interface IFurnitureLogic
	{
		List<FurnitureViewModel>? ReadList(FurnitureSearchModel? model);
		FurnitureViewModel? ReadElement(FurnitureSearchModel model);
		bool Create(FurnitureBindingModel model);
		bool Update(FurnitureBindingModel model);
		bool Delete(FurnitureBindingModel model);
	}
}
