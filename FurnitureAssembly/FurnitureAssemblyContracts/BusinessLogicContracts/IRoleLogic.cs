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
	public interface IRoleLogic
	{
		List<RoleViewModel>? ReadList(RoleSearchModel? model);
		RoleViewModel? ReadElement(RoleSearchModel model);
		bool Create(RoleBindingModel model);
		bool Update(RoleBindingModel model);
		bool Delete(RoleBindingModel model);
	}
}
