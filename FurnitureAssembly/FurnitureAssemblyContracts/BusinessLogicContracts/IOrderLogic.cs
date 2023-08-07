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
	public interface IOrderLogic
	{
		List<OrderViewModel>? ReadList(OrderSearchModel? model);
		OrderViewModel? ReadElement(OrderSearchModel model);
		bool Create(OrderBindingModel model);
		bool Update(OrderBindingModel model);
		bool Delete(OrderBindingModel model);
	}
}
