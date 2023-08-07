using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Spreadsheet;
using FurnitureAssemblyContracts.BindingModels;
using FurnitureAssemblyContracts.BusinessLogicContracts;
using FurnitureAssemblyContracts.SearchModels;
using FurnitureAssemblyContracts.ViewModels;
using FurnitureAssemblyDatabaseImplement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FurnitureAssemblyRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly ILogger _logger;
        private readonly IOrderLogic _order;
        private readonly IOrderInfoLogic _orderInfo;
        private readonly ISetLogic _set;
        public OrderController(ILogger<OrderController> logger,
            IOrderLogic order,
            IOrderInfoLogic orderInfo,
            ISetLogic set)
        {
            _logger = logger;
            _order = order;
            _orderInfo = orderInfo;
            _set = set;
        }

        [HttpGet]
        public List<OrderViewModel>? GetOrderList()
        {
            try
            {
                return _order.ReadList(null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка получения списка заказов");
                throw;
            }
        }
        [HttpGet]
        public OrderViewModel? GetOrder(int Id)
        {
            try
            {
                return _order.ReadElement(new OrderSearchModel { Id = Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка получения заказа по id={Id}", Id);
                throw;
            }
        }
		[HttpGet]
		public List<OrderViewModel>? GetOrderListByUser(int userId)
		{
			try
			{
                List<OrderInfoViewModel> orderInfos = _orderInfo.ReadList(new OrderInfoSearchModel { UserId = userId });
                if (orderInfos == null)
                {
                    return null;
                }
                List<int> orderInfoIds = new List<int>();
                foreach (OrderInfoViewModel orderInfo in orderInfos)
                {
					orderInfoIds.Add(orderInfo.Id);
                }
				return _order.ReadList(new OrderSearchModel { OrderInfoId = orderInfoIds });
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Ошибка получения заказа по userId={userId}", userId);
				throw;
			}
		}
        [HttpGet]
        public Tuple<OrderInfoViewModel, List<SetViewModel>, List<int>>? GetOrderWithSets(int orderId)
        {
            try
            {
                var orderInfo = _orderInfo.ReadElement(new() { Id = orderId });
                if (orderInfo == null)
                {
                    return null;
                }
                var orders = _order.ReadList(new OrderSearchModel { OrderInfoId = new List<int>() { orderInfo.Id } });
                var sets = new List<SetViewModel>();
                var counts = new List<int>();
                if (orders == null)
                {
                    sets = null;
                    counts = null;
                }
                else
                {
                    foreach (var order in orders)
                    {
                        var el = _set.ReadElement(new SetSearchModel { Id = order.SetId });
                        sets.Add(el);
                        counts.Add(order.Count);
                    }
                }
                var tuple = System.Tuple.Create(orderInfo,
                    sets,
                    counts);
                return tuple;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка гарнитура с мебельными модулями");
                throw;
            }
        }
        [HttpPost]
        public void AddOrder(OrderBindingModel model)
        {
            try
            {
                _order.Create(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка создания заказа");
                throw;
            }
        }
        [HttpPost]
        public void UpdateOrder(OrderBindingModel model)
        {
            try
            {
                _order.Update(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка обновления заказа");
                throw;
            }
        }
        [HttpPost]
        public void DeleteOrder(OrderBindingModel model)
        {
            try
            {
                _order.Delete(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка удаления заказа");
                throw;
            }
        }
		[HttpPost]
		public void AddSetInOrder(Tuple<OrderInfoSearchModel, SetSearchModel, int> orderSetWithCount)
		{
			try
			{
                var orderInfo = _orderInfo.ReadElement(orderSetWithCount.Item1);
                var set = _set.ReadElement(orderSetWithCount.Item2);
                _order.Create(new OrderBindingModel
                {
                    OrderInfoId = orderInfo.Id,
                    SetId = set.Id,
                    Count = orderSetWithCount.Item3
                });
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Ошибка добавления поездки в магазин");
				throw;
			}
		}
	}
}
