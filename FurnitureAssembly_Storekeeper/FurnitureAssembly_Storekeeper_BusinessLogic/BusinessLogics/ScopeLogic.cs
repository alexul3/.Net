using FurnitureAssembly_Storekeeper_Contracts.BindingModels;
using FurnitureAssembly_Storekeeper_Contracts.BusinessLogicsContracts;
using FurnitureAssembly_Storekeeper_Contracts.SearchModels;
using FurnitureAssembly_Storekeeper_Contracts.StoragesContracts;
using FurnitureAssembly_Storekeeper_Contracts.ViewModels;
using Microsoft.Extensions.Logging;

namespace FurnitureAssembly_Storekeeper_BusinessLogic.BusinessLogics
{
    public class ScopeLogic : IScopeLogic
    {
        private readonly ILogger _logger;
        private readonly IScopeStorage _ScopeStorage;
        public ScopeLogic(ILogger<ScopeLogic> logger, IScopeStorage
       ScopeStorage)
        {
            _logger = logger;
            _ScopeStorage = ScopeStorage;
        }
        public List<ScopeViewModel>? ReadList(ScopeSearchModel? model)
        {
            _logger.LogInformation("ReadList. ScopeName:{ScopeName}.Id:{Id}", model?.ScopeName, model?.Id);
            var list = model == null ? _ScopeStorage.GetFullList() :
            _ScopeStorage.GetFilteredList(model);
            if (list == null)
            {
                _logger.LogWarning("ReadList return null list");
                return null;
            }
            _logger.LogInformation("ReadList. Count:{Count}", list.Count);
            return list;
        }
        public ScopeViewModel? ReadElement(ScopeSearchModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            _logger.LogInformation("ReadElement. ScopeName:{ScopeName}.Id:{Id}", model.ScopeName, model.Id);

            var element = _ScopeStorage.GetElement(model);

            if (element == null)
            {
                _logger.LogWarning("ReadElement element not found");
                return null;
            }
            _logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
            return element;
        }
        public bool Create(ScopeBindingModel model)
        {
            CheckModel(model);
            if (_ScopeStorage.Insert(model) == null)
            {
                _logger.LogWarning("Insert operation failed");
                return false;
            }
            return true;
        }
        public bool Update(ScopeBindingModel model)
        {
            CheckModel(model);
            if (_ScopeStorage.Update(model) == null)
            {
                _logger.LogWarning("Update operation failed");
                return false;
            }
            return true;
        }
        public bool Delete(ScopeBindingModel model)
        {
            CheckModel(model, false);
            _logger.LogInformation("Delete. Id:{Id}", model.Id);
            if (_ScopeStorage.Delete(model) == null)
            {
                _logger.LogWarning("Delete operation failed");
                return false;
            }
            return true;
        }
        private void CheckModel(ScopeBindingModel model, bool withParams =
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
                throw new ArgumentNullException("Нет названия области использования",
               nameof(model.Name));
            }
            _logger.LogInformation("Scope. ScopeName:{ScopeName}.  Id: {Id}", model.Name,model.Id);
            var element = _ScopeStorage.GetElement(new ScopeSearchModel
            {
               ScopeName = model.Name
            });
            if (element != null && element.Id != model.Id)
            {
                throw new InvalidOperationException("Область использования с таким названием уже есть");
            }
        }
    }
}