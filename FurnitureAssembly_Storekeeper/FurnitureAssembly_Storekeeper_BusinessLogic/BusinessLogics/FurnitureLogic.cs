using FurnitureAssembly_Storekeeper_Contracts.BindingModels;
using FurnitureAssembly_Storekeeper_Contracts.BusinessLogicsContracts;
using FurnitureAssembly_Storekeeper_Contracts.SearchModels;
using FurnitureAssembly_Storekeeper_Contracts.StoragesContracts;
using FurnitureAssembly_Storekeeper_Contracts.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssembly_Storekeeper_BusinessLogic.BusinessLogics
{
    public class FurnitureLogic: IFurnitureLogic
    {
        private readonly ILogger _logger;
        private readonly IFurnitureStorage _FurnitureStorage;
        public FurnitureLogic(ILogger<FurnitureLogic> logger, IFurnitureStorage furnitureStorage)
        {
            _logger = logger;
            _FurnitureStorage = furnitureStorage;
        }
        public List<FurnitureViewModel>? ReadList(FurnitureSearchModel? model)
        {
            _logger.LogInformation("ReadList. FurnitureName:{FurnitureName}.Id:{Id}", model?.FurnitureName, model?.Id);
            var list = model == null ? _FurnitureStorage.GetFullList() : _FurnitureStorage.GetFilteredList(model);
            if (list == null)
            {
                _logger.LogWarning("ReadList return null list");
                return null;
            }
            _logger.LogInformation("ReadList. Count:{Count}", list.Count);
            return list;
        }
        public FurnitureViewModel? ReadElement(FurnitureSearchModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            _logger.LogInformation("ReadElement. FurnitureName:{FurnitureName}.Id:{Id}", model.FurnitureName, model.Id);

            var element = _FurnitureStorage.GetElement(model);

            if (element == null)
            {
                _logger.LogWarning("ReadElement element not found");
                return null;
            }
            _logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
            return element;
        }
        public bool Create(FurnitureBindingModel model)
        {
            CheckModel(model);
            if (_FurnitureStorage.Insert(model) == null)
            {
                _logger.LogWarning("Insert operation failed");
                return false;
            }
            return true;
        }
        public bool Update(FurnitureBindingModel model)
        {
            CheckModel(model);
            if (_FurnitureStorage.Update(model) == null)
            {
                _logger.LogWarning("Update operation failed");
                return false;
            }
            return true;
        }
        public bool Delete(FurnitureBindingModel model)
        {
            CheckModel(model, false);
            _logger.LogInformation("Delete. Id:{Id}", model.Id);
            if (_FurnitureStorage.Delete(model) == null)
            {
                _logger.LogWarning("Delete operation failed");
                return false;
            }
            return true;
        }
        private void CheckModel(FurnitureBindingModel model, bool withParams = true)
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
                throw new ArgumentNullException("Нет названия продукта",nameof(model.Name));
            }
            if (model.Cost <= 0)
            {
                throw new ArgumentNullException("Цена компонента должна быть больше 0", nameof(model.Cost));
            }
            _logger.LogInformation("Furniture. FurnitureName:{FurnitureName}. Cost:{Cost}. Id: {Id}", model.Name, model.Cost, model.Id);
            var element = _FurnitureStorage.GetElement(new FurnitureSearchModel
            {
                FurnitureName = model.Name
            });
            if (element != null && element.Id != model.Id)
            {
                throw new InvalidOperationException("Изделие с таким названием уже есть");
            }
        }
    }
}
