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
	public interface IMaterialLogic
	{
		List<MaterialViewModel>? ReadList(MaterialSearchModel? model);
		MaterialViewModel? ReadElement(MaterialSearchModel model);
		bool Create(MaterialBindingModel model);
		bool Update(MaterialBindingModel model);
		bool Delete(MaterialBindingModel model);
	}
}
