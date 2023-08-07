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
    public class OrderInfoLogic : IOrderInfoLogic
    {
        private readonly ILogger _logger;
        private readonly IOrderInfoStorage _orderInfoStorage;
        public OrderInfoLogic(ILogger<OrderInfoLogic> logger, IOrderInfoStorage orderInfoStorage)
        {
            _logger = logger;
            _orderInfoStorage = orderInfoStorage;
        }
        public List<OrderInfoViewModel>? ReadList(OrderInfoSearchModel? model)
        {
            _logger.LogInformation("ReadList. OrderInfoId:{Id}", model?.Id);
            var list = model == null ? _orderInfoStorage.GetFullList() : _orderInfoStorage.GetFilteredList(model);
            if (list == null)
            {
                _logger.LogWarning("ReadList return null list");
                return null;
            }
            _logger.LogInformation("ReadList. Count:{Count}", list.Count);
            return list;
        }
        public OrderInfoViewModel? ReadElement(OrderInfoSearchModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            _logger.LogInformation("ReadElement. OrderInfoId:{ Id}", model.Id);
            var element = _orderInfoStorage.GetElement(model);
            if (element == null)
            {
                _logger.LogWarning("ReadElement element not found");
                return null;
            }
            _logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
            return element;
        }
        public OrderInfoViewModel? Create(OrderInfoBindingModel model)
        {
            CheckModel(model);
            var order = _orderInfoStorage.Insert(model);
            if (order == null)
            {
                _logger.LogWarning("Insert operation failed");
                return null;
            }
            return order;
        }
        public bool Update(OrderInfoBindingModel model)
        {
            CheckModel(model);
            if (_orderInfoStorage.Update(model) == null)
            {
                _logger.LogWarning("Update operation failed");
                return false;
            }
            return true;
        }
        public bool Delete(OrderInfoBindingModel model)
        {
            CheckModel(model, false);
            _logger.LogInformation("Delete. Id:{Id}", model.Id);
            if (_orderInfoStorage.Delete(model) == null)
            {
                _logger.LogWarning("Delete operation failed");
                return false;
            }
            return true;
        }
        private void CheckModel(OrderInfoBindingModel model, bool withParams = true)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (!withParams)
            {
                return;
            }
            if (model.PaymentType == FurnitureAssemblyDataModels.Enums.PaymentType.Неизвестен)
            {
                throw new ArgumentNullException("Неизвестный тип оплаты", nameof(model.PaymentType));
            }
            if (model.Sum < 0)
            {
                throw new ArgumentNullException("Сумма заказа должна быть больше 0", nameof(model.Sum));
            }
            if (model.UserId < 0)
            {
                throw new ArgumentNullException("Неправильный Id пользователя", nameof(model.UserId));
            }
            _logger.LogInformation("OrderInfo. Id: {Id}.", model.Id);
        }
    }
}
