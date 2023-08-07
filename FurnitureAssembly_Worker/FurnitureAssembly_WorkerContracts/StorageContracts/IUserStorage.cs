using FurnitureAssembly_WorkerContracts.BindingModels;
using FurnitureAssembly_WorkerContracts.SearchModels;
using FurnitureAssembly_WorkerContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssembly_WorkerContracts.StorageContracts
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
