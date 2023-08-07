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
	public class SetLogic : ISetLogic
	{
		private readonly ILogger _logger;
		private readonly ISetStorage _SetStorage;
		public SetLogic(ILogger<SetLogic> logger, ISetStorage SetStorage)
		{
			_logger = logger;
			_SetStorage = SetStorage;
		}
		public List<SetViewModel>? ReadList(SetSearchModel? model)
		{
			_logger.LogInformation("ReadList. SetName:{Name}.Id:{ Id}", model?.Name, model?.Id);
			var list = model == null ? _SetStorage.GetFullList() : _SetStorage.GetFilteredList(model);
			if (list == null)
			{
				_logger.LogWarning("ReadList return null list");
				return null;
			}
			_logger.LogInformation("ReadList. Count:{Count}", list.Count);
			return list;
		}
		public SetViewModel? ReadElement(SetSearchModel model)
		{
			if (model == null)
			{
				throw new ArgumentNullException(nameof(model));
			}
			_logger.LogInformation("ReadElement. SetName:{Name}.Id:{ Id}", model.Name, model.Id);
			var element = _SetStorage.GetElement(model);
			if (element == null)
			{
				_logger.LogWarning("ReadElement element not found");
				return null;
			}
			_logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
			return element;
		}
		public bool Create(SetBindingModel model)
		{
			CheckModel(model);
			if (_SetStorage.Insert(model) == null)
			{
				_logger.LogWarning("Insert operation failed");
				return false;
			}
			return true;
		}
		public bool Update(SetBindingModel model)
		{
			CheckModel(model);
			if (_SetStorage.Update(model) == null)
			{
				_logger.LogWarning("Update operation failed");
				return false;
			}
			return true;
		}
		public bool Delete(SetBindingModel model)
		{
			CheckModel(model, false);
			_logger.LogInformation("Delete. Id:{Id}", model.Id);
			if (_SetStorage.Delete(model) == null)
			{
				_logger.LogWarning("Delete operation failed");
				return false;
			}
			return true;
		}
		private void CheckModel(SetBindingModel model, bool withParams = true)
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
				throw new ArgumentNullException("Нет названия гарнитура", nameof(model.Name));
			}
			if (model.Cost <= 0)
			{
				throw new ArgumentNullException("Цена гарнитура должна быть больше 0", nameof(model.Cost));
			}
			_logger.LogInformation("Set. Name:{Name}. Cost:{Cost}. Id:{Id}", model.Name, model.Cost, model.Id);
			var element = _SetStorage.GetElement(new SetSearchModel
			{
				Name = model.Name
			});
			if (element != null && element.Id != model.Id)
			{
				throw new InvalidOperationException("Гарнитур с таким названием уже есть");
			}
		}
	}
}
