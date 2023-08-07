using FurnitureAssemblyContracts.BindingModels;
using FurnitureAssemblyContracts.SearchModels;
using FurnitureAssemblyContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyContracts.StorageContracts
{
	public interface IRoleStorage
	{
		List<RoleViewModel> GetFullList();
		List<RoleViewModel> GetFilteredList(RoleSearchModel model);
		RoleViewModel? GetElement(RoleSearchModel model);
		RoleViewModel? Insert(RoleBindingModel model);
		RoleViewModel? Update(RoleBindingModel model);
		RoleViewModel? Delete(RoleBindingModel model);
	}
}
