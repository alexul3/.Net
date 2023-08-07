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
	internal class UserLogic : IUserLogic
	{
		private readonly ILogger _logger;
		private readonly IUserStorage _userStorage;
		public UserLogic(ILogger<UserLogic> logger, IUserStorage userStorage)
		{
			_logger = logger;
			_userStorage = userStorage;
		}
		public List<UserViewModel>? ReadList(UserSearchModel? model)
		{
			_logger.LogInformation("ReadList. UserName:{Name}. Id:{Id}", model?.Name, model?.Id);
			var list = model == null ? _userStorage.GetFullList() : _userStorage.GetFilteredList(model);
			if (list == null)
			{
				_logger.LogWarning("ReadList return null list");
				return null;
			}
			_logger.LogInformation("ReadList. Count:{Count}", list.Count);
			return list;
		}
		public UserViewModel? ReadElement(UserSearchModel model)
		{
			if (model == null)
			{
				throw new ArgumentNullException(nameof(model));
			}
			_logger.LogInformation("ReadElement. UserName:{Name}. Id:{Id}", model.Name, model.Id);
			var element = _userStorage.GetElement(model);
			if (element == null)
			{
				_logger.LogWarning("ReadElement element not found");
				return null;
			}
			_logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
			return element;
		}
		public bool Create(UserBindingModel model)
		{
			CheckModel(model);
			if (_userStorage.Insert(model) == null)
			{
				_logger.LogWarning("Insert operation failed");
				return false;
			}
			return true;
		}
		public bool Update(UserBindingModel model)
		{
			CheckModel(model);
			if (_userStorage.Update(model) == null)
			{
				_logger.LogWarning("Update operation failed");
				return false;
			}
			return true;
		}
		public bool Delete(UserBindingModel model)
		{
			CheckModel(model, false);
			_logger.LogInformation("Delete. Id:{Id}", model.Id);
			if (_userStorage.Delete(model) == null)
			{
				_logger.LogWarning("Delete operation failed");
				return false;
			}
			return true;
		}
		private void CheckModel(UserBindingModel model, bool withParams = true)
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
				throw new ArgumentNullException("Нет имени клиента", nameof(model.Name));
			}
			if (string.IsNullOrEmpty(model.Login))
			{
				throw new ArgumentNullException("Нет логина клиента", nameof(model.Name));
			}
			if (string.IsNullOrEmpty(model.Password))
			{
				throw new ArgumentNullException("Нет пароля учетной записи клиента", nameof(model.Name));
			}
			_logger.LogInformation("User. Name:{Name}. Login:{Login}. Password:{Password}. Id:{Id}", model.Name, model.Login, model.Password, model.Id);
			var element = _userStorage.GetElement(new UserSearchModel
			{
				Login = model.Login
			});
			if (element != null && element.Id != model.Id)
			{
				throw new InvalidOperationException("Клиент с таким логином уже есть");
			}
		}
	}
}
