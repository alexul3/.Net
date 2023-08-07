using FurnitureAssembly_WorkerContracts.BindingModels;
using FurnitureAssembly_WorkerContracts.BusinessLogicContracts;
using FurnitureAssembly_WorkerContracts.SearchModels;
using FurnitureAssembly_WorkerContracts.StorageContracts;
using FurnitureAssembly_WorkerContracts.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssembly_WorkerBusinessLogic.BusinessLogics
{
	public class FurnitureModuleLogic : IFurnitureModuleLogic
	{
		private readonly ILogger _logger;
		private readonly IFurnitureModuleStorage _furnitureModuleStorage;
		public FurnitureModuleLogic(ILogger<FurnitureModuleLogic> logger, IFurnitureModuleStorage furnitureModuleStorage)
		{
			_logger = logger;
			_furnitureModuleStorage = furnitureModuleStorage;
		}
		public List<FurnitureModuleViewModel>? ReadList(FurnitureModuleSearchModel? model)
		{
			_logger.LogInformation("ReadList. FurnitureModuleName:{Name}. Id:{Id}", model?.Name, model?.Id);
			var list = model == null ? _furnitureModuleStorage.GetFullList() : _furnitureModuleStorage.GetFilteredList(model);
			if (list == null)
			{
				_logger.LogWarning("ReadList return null list");
				return null;
			}
			_logger.LogInformation("ReadList. Count:{Count}", list.Count);
			return list;
		}
		public FurnitureModuleViewModel? ReadElement(FurnitureModuleSearchModel model)
		{
			if (model == null)
			{
				throw new ArgumentNullException(nameof(model));
			}
			_logger.LogInformation("ReadElement. FurnitureModuleName:{Name}. Id:{Id}", model.Name, model.Id);
			var element = _furnitureModuleStorage.GetElement(model);
			if (element == null)
			{
				_logger.LogWarning("ReadElement element not found");
				return null;
			}
			_logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
			return element;
		}
		public bool Create(FurnitureModuleBindingModel model)
		{
			CheckModel(model);
			if (_furnitureModuleStorage.Insert(model) == null)
			{
				_logger.LogWarning("Insert operation failed");
				return false;
			}
			return true;
		}
		public bool Update(FurnitureModuleBindingModel model)
		{
			CheckModel(model);
			if (_furnitureModuleStorage.Update(model) == null)
			{
				_logger.LogWarning("Update operation failed");
				return false;
			}
			return true;
		}
		public bool Delete(FurnitureModuleBindingModel model)
		{
			CheckModel(model, false);
			_logger.LogInformation("Delete. Id:{Id}", model.Id);
			if (_furnitureModuleStorage.Delete(model) == null)
			{
				_logger.LogWarning("Delete operation failed");
				return false;
			}
			return true;
		}
		private void CheckModel(FurnitureModuleBindingModel model, bool withParams = true)
		{
			if (model == null)
			{
				throw new ArgumentNullException(nameof(model));
			}
			if (!withParams)
			{
				return;
			}
			if (string.IsNullOrEmpty(model.Name))
			{
				throw new ArgumentNullException("Нет названия мебельного модуля", nameof(model.Name));
			}
			if (model.Cost <= 0)
			{
				throw new ArgumentNullException("Цена мебельного модуля должна быть больше 0", nameof(model.Cost));
			}
			_logger.LogInformation("FurnitureModule. Name:{Name}. Cost:{Cost}. Id:{Id}", model.Name, model.Cost, model.Id);
			var element = _furnitureModuleStorage.GetElement(new FurnitureModuleSearchModel
			{
				Name = model.Name
			});
			if (element != null && element.Id != model.Id)
			{
				throw new InvalidOperationException("Мебельный модуль с таким названием уже есть");
			}
		}
	}
}
