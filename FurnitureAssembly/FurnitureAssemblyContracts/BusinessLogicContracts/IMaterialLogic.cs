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
	public interface IMaterialLogic
	{
		List<MaterialViewModel>? ReadList(MaterialSearchModel? model);
		MaterialViewModel? ReadElement(MaterialSearchModel model);
		bool Create(MaterialBindingModel model);
		bool Update(MaterialBindingModel model);
		bool Delete(MaterialBindingModel model);
	}
}
