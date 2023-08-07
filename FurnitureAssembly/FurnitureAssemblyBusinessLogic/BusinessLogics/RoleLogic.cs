using FurnitureAssemblyContracts.BindingModels;
using FurnitureAssemblyContracts.BusinessLogicContracts;
using FurnitureAssemblyContracts.SearchModels;
using FurnitureAssemblyContracts.StorageContracts;
using FurnitureAssemblyContracts.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyBusinessLogic.BusinessLogics
{
	public class RoleLogic : IRoleLogic
	{
		private readonly ILogger _logger;
		private readonly IRoleStorage _roleStorage;
		public RoleLogic(ILogger<RoleLogic> logger, IRoleStorage roleStorage)
		{
			_logger = logger;
			_roleStorage = roleStorage;
		}
		public List<RoleViewModel>? ReadList(RoleSearchModel? model)
		{
			_logger.LogInformation("ReadList. RoleName:{Name}. Id:{Id}", model?.Name, model?.Id);
			var list = model == null ? _roleStorage.GetFullList() : _roleStorage.GetFilteredList(model);
			if (list == null)
			{
				_logger.LogWarning("ReadList return null list");
				return null;
			}
			_logger.LogInformation("ReadList. Count:{Count}", list.Count);
			return list;
		}
		public RoleViewModel? ReadElement(RoleSearchModel model)
		{
			if (model == null)
			{
				throw new ArgumentNullException(nameof(model));
			}
			_logger.LogInformation("ReadElement. RoleName:{Name}. Id:{Id}", model.Name, model.Id);
			var element = _roleStorage.GetElement(model);
			if (element == null)
			{
				_logger.LogWarning("ReadElement element not found");
				return null;
			}
			_logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
			return element;
		}
		public bool Create(RoleBindingModel model)
		{
			CheckModel(model);
			if (_roleStorage.Insert(model) == null)
			{
				_logger.LogWarning("Insert operation failed");
				return false;
			}
			return true;
		}
		public bool Update(RoleBindingModel model)
		{
			CheckModel(model);
			if (_roleStorage.Update(model) == null)
			{
				_logger.LogWarning("Update operation failed");
				return false;
			}
			return true;
		}
		public bool Delete(RoleBindingModel model)
		{
			CheckModel(model, false);
			_logger.LogInformation("Delete. Id:{Id}", model.Id);
			if (_roleStorage.Delete(model) == null)
			{
				_logger.LogWarning("Delete operation failed");
				return false;
			}
			return true;
		}
		private void CheckModel(RoleBindingModel model, bool withParams = true)
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
				throw new ArgumentNullException("Нет имени роли", nameof(model.Name));
			}
			_logger.LogInformation("Role. Name:{Name}. Id:{Id}", model.Name, model.Id);
			var element = _roleStorage.GetElement(new RoleSearchModel
			{
				Name = model.Name
			});
			if (element != null && element.Id != model.Id)
			{
				throw new InvalidOperationException("Роль с таким названием уже есть");
			}
		}
	}
}
