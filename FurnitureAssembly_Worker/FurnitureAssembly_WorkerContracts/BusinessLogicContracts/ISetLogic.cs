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
	public interface ISetLogic
	{
		List<SetViewModel>? ReadList(SetSearchModel? model);
		SetViewModel? ReadElement(SetSearchModel model);
		bool Create(SetBindingModel model);
		bool Update(SetBindingModel model);
		bool Delete(SetBindingModel model);
	}
}
