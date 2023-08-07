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
	public interface IOrderLogic
	{
		List<OrderViewModel>? ReadList(OrderSearchModel? model);
		OrderViewModel? ReadElement(OrderSearchModel model);
		bool Create(OrderBindingModel model);
		bool Update(OrderBindingModel model);
		bool Delete(OrderBindingModel model);
	}
}
