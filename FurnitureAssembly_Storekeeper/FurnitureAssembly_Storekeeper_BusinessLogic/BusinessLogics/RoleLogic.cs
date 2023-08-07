using FurnitureAssembly_Storekeeper_Contracts.BindingModels;
using FurnitureAssembly_Storekeeper_Contracts.BusinessLogicsContracts;
using FurnitureAssembly_Storekeeper_Contracts.SearchModels;
using FurnitureAssembly_Storekeeper_Contracts.StoragesContracts;
using FurnitureAssembly_Storekeeper_Contracts.ViewModels;
using Microsoft.Extensions.Logging;

namespace FurnitureAssembly_Storekeeper_BusinessLogic.BusinessLogics
{
    public class RoleLogic : IRoleLogic
    {
        private readonly ILogger _logger;
        private readonly IRoleStorage _RoleStorage;
        public RoleLogic(ILogger<RoleLogic> logger, IRoleStorage
       RoleStorage)
        {
            _logger = logger;
            _RoleStorage = RoleStorage;
        }
        public List<RoleViewModel>? ReadList(RoleSearchModel? model)
        {
            _logger.LogInformation("ReadList. RoleName:{RoleName}.Id:{Id}", model?.RoleName, model?.Id);
            var list = model == null ? _RoleStorage.GetFullList() :
            _RoleStorage.GetFilteredList(model);
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
            _logger.LogInformation("ReadElement. RoleName:{RoleName}.Id:{Id}", model.RoleName, model.Id);

            var element = _RoleStorage.GetElement(model);

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
            if (_RoleStorage.Insert(model) == null)
            {
                _logger.LogWarning("Insert operation failed");
                return false;
            }
            return true;
        }
        public bool Update(RoleBindingModel model)
        {
            CheckModel(model);
            if (_RoleStorage.Update(model) == null)
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
            if (_RoleStorage.Delete(model) == null)
            {
                _logger.LogWarning("Delete operation failed");
                return false;
            }
            return true;
        }
        private void CheckModel(RoleBindingModel model, bool withParams =
       true)
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
                throw new ArgumentNullException("Нет названия роли",
               nameof(model.Name));
            }
            
            _logger.LogInformation("Role. RoleName:{RoleName}. Id: {Id}", model.Name, model.Id);
            var element = _RoleStorage.GetElement(new RoleSearchModel
            {
               RoleName = model.Name
            });
            if (element != null && element.Id != model.Id)
            {
                throw new InvalidOperationException("Роль с таким названием уже есть");
            }
        }
    }
}