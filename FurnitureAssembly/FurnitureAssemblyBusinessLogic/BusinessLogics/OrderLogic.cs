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
	public class OrderLogic : IOrderLogic
	{
		private readonly ILogger _logger;
		private readonly IOrderStorage _orderStorage;
		private readonly IOrderInfoStorage _orderInfoStorage;
		private readonly ISetStorage _setStorage;
		public OrderLogic(ILogger<OrderLogic> logger, IOrderStorage orderStorage, IOrderInfoStorage orderInfoStorage, ISetStorage setStorage)
		{
			_logger = logger;
			_orderStorage = orderStorage;
			_orderInfoStorage = orderInfoStorage;
			_setStorage = setStorage;
		}
		public List<OrderViewModel>? ReadList(OrderSearchModel? model)
		{
			_logger.LogInformation("ReadList. OrderId:{Id}", model?.Id);
			var list = model == null ? _orderStorage.GetFullList() : _orderStorage.GetFilteredList(model);
			if (list == null)
			{
				_logger.LogWarning("ReadList return null list");
				return null;
			}
			_logger.LogInformation("ReadList. Count:{Count}", list.Count);
			return list;
		}
		public OrderViewModel? ReadElement(OrderSearchModel model)
		{
			if (model == null)
			{
				throw new ArgumentNullException(nameof(model));
			}
			_logger.LogInformation("ReadElement. OrderId:{ Id}", model.Id);
			var element = _orderStorage.GetElement(model);
			if (element == null)
			{
				_logger.LogWarning("ReadElement element not found");
				return null;
			}
			_logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
			return element;
		}
		public bool Create(OrderBindingModel model)
		{
			CheckModel(model);
			var orderInfo = _orderInfoStorage.GetElement(new OrderInfoSearchModel { Id = model.OrderInfoId });
			var set = _setStorage.GetElement(new SetSearchModel { Id = model.SetId });
			if (orderInfo == null || set == null)
			{
				return false;
			}
			_orderInfoStorage.Update(new OrderInfoBindingModel
			{
				Id = orderInfo.Id,
				CustomerName = orderInfo.CustomerName,
				Sum = orderInfo.Sum + (set.Cost * model.Count),
				DateCreate = orderInfo.DateCreate,
				PaymentType = orderInfo.PaymentType,
				UserId = orderInfo.UserId
			});
			var order = _orderStorage.GetElement(new OrderSearchModel
			{
				OrderInfoId = new List<int> { model.OrderInfoId },
				SetId = model.SetId
			});
			if (order != null)
			{
				if (_orderStorage.Update(new OrderBindingModel
				{
					Id = order.Id,
					OrderInfoId = order.OrderInfoId,
					SetId = order.SetId,
					Count = order.Count + model.Count
				}) == null)
				{
                    _logger.LogWarning("Insert operation failed");
                    return false;
                }
			}
			else if (_orderStorage.Insert(model) == null)
			{
				_logger.LogWarning("Insert operation failed");
				return false;
			}
			return true;
		}
		public bool Update(OrderBindingModel model)
		{
			CheckModel(model);
			if (_orderStorage.Update(model) == null)
			{
				_logger.LogWarning("Update operation failed");
				return false;
			}
			return true;
		}
		public bool Delete(OrderBindingModel model)
		{
			CheckModel(model, false);
			var order = _orderStorage.GetElement(new OrderSearchModel { Id = model.Id });
            var orderInfo = _orderInfoStorage.GetElement(new OrderInfoSearchModel { Id = order.OrderInfoId });
            var set = _setStorage.GetElement(new SetSearchModel { Id = order.SetId });
            if (orderInfo == null || set == null)
            {
                return false;
            }
            _orderInfoStorage.Update(new OrderInfoBindingModel
            {
                Id = orderInfo.Id,
                CustomerName = orderInfo.CustomerName,
                Sum = orderInfo.Sum - (set.Cost * order.Count),
                DateCreate = orderInfo.DateCreate,
                PaymentType = orderInfo.PaymentType,
                UserId = orderInfo.UserId
            });
            _logger.LogInformation("Delete. Id:{Id}", model.Id);
			if (_orderStorage.Delete(model) == null)
			{
				_logger.LogWarning("Delete operation failed");
				return false;
			}
			return true;
		}
		private void CheckModel(OrderBindingModel model, bool withParams = true)
		{
			if (model == null)
			{
				throw new ArgumentNullException(nameof(model));
			}
			if (!withParams)
			{
				return;
			}
			if (model.SetId < 0)
			{
				throw new ArgumentNullException("Некорректный идентификатор гарнитура", nameof(model.SetId));
			}
			if (model.OrderInfoId < 0)
			{
				throw new ArgumentNullException("Некорректный индентификатор order_info", nameof(model.OrderInfoId));
			}
			_logger.LogInformation("Order. Id: {Id}. SetId: {SetId}", model.Id, model.SetId);
		}
	}
}
