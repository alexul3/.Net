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
	public interface IUserStorage
	{
		List<UserViewModel> GetFullList();
		List<UserViewModel> GetFilteredList(UserSearchModel model);
		UserViewModel? GetElement(UserSearchModel model);
		UserViewModel? Insert(UserBindingModel model);
		UserViewModel? Update(UserBindingModel model);
		UserViewModel? Delete(UserBindingModel model);
	}
}
