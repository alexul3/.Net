using FurnitureAssemblyContracts.BindingModels;
using FurnitureAssemblyContracts.BusinessLogicContracts;
using FurnitureAssemblyContracts.SearchModels;
using FurnitureAssemblyContracts.StorageContracts;
using FurnitureAssemblyContracts.ViewModels;
using Microsoft.Extensions.Logging;

namespace FurnitureAssemblyBusinessLogic.BusinessLogics
{
    public class MaterialLogic : IMaterialLogic
    {
        private readonly ILogger _logger;
        private readonly IMaterialStorage _MaterialStorage;
        public MaterialLogic(ILogger<MaterialLogic> logger, IMaterialStorage
       MaterialStorage)
        {
            _logger = logger;
            _MaterialStorage = MaterialStorage;
        }
        public List<MaterialViewModel>? ReadList(MaterialSearchModel? model)
        {
            _logger.LogInformation("ReadList. MaterialName:{Name}.Id:{Id}", model?.Name, model?.Id);
            var list = model == null ? _MaterialStorage.GetFullList() :
            _MaterialStorage.GetFilteredList(model);
            if (list == null)
            {
                _logger.LogWarning("ReadList return null list");
                return null;
            }
            _logger.LogInformation("ReadList. Count:{Count}", list.Count);
            return list;
        }
        public MaterialViewModel? ReadElement(MaterialSearchModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            _logger.LogInformation("ReadElement. MaterialName:{Name}.Id:{Id}", model.Name, model.Id);

            var element = _MaterialStorage.GetElement(model);

            if (element == null)
            {
                _logger.LogWarning("ReadElement element not found");
                return null;
            }
            _logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
            return element;
        }
        public bool Create(MaterialBindingModel model)
        {
            CheckModel(model);
            if (_MaterialStorage.Insert(model) == null)
            {
                _logger.LogWarning("Insert operation failed");
                return false;
            }
            return true;
        }
        public bool Update(MaterialBindingModel model)
        {
            CheckModel(model);
            if (_MaterialStorage.Update(model) == null)
            {
                _logger.LogWarning("Update operation failed");
                return false;
            }
            return true;
        }
        public bool Delete(MaterialBindingModel model)
        {
            CheckModel(model, false);
            _logger.LogInformation("Delete. Id:{Id}", model.Id);
            if (_MaterialStorage.Delete(model) == null)
            {
                _logger.LogWarning("Delete operation failed");
                return false;
            }
            return true;
        }
        private void CheckModel(MaterialBindingModel model, bool withParams =
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
                throw new ArgumentNullException("Нет названия компонента",
               nameof(model.Name));
            }
            if (model.Cost <= 0)
            {
                throw new ArgumentNullException("Цена компонента должна быть больше 0", nameof(model.Cost));
            }
            _logger.LogInformation("Material. Name:{Name}. Cost:{ Cost}. Id: {Id}", model.Name, model.Cost, model.Id);
            var element = _MaterialStorage.GetElement(new MaterialSearchModel
            {
               Name = model.Name
            });
            if (element != null && element.Id != model.Id)
            {
                throw new InvalidOperationException("Компонент с таким названием уже есть");
            }
        }
    }
}