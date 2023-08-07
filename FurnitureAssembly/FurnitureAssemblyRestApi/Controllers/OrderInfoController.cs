using DocumentFormat.OpenXml.Spreadsheet;
using FurnitureAssemblyContracts.BindingModels;
using FurnitureAssemblyContracts.BusinessLogicContracts;
using FurnitureAssemblyContracts.SearchModels;
using FurnitureAssemblyContracts.ViewModels;
using FurnitureAssemblyDatabaseImplement.Models;
using FurnitureAssemblyDataModels.Enums;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.X9;
using System.Collections.Generic;

namespace FurnitureAssemblyRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderInfoController : Controller
    {
        private readonly ILogger _logger;
        private readonly IOrderInfoLogic _orderInfo;
        private readonly IUserLogic _user;
        public OrderInfoController(ILogger<OrderInfoController> logger, IOrderInfoLogic orderInfo, IUserLogic user)
        {
            _logger = logger;
            _orderInfo = orderInfo;
            _user = user;
        }

        [HttpGet]
        public List<OrderInfoViewModel>? GetOrderInfoList()
        {
            try
            {
                return _orderInfo.ReadList(null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка получения списка мебельных модулей");
                throw;
            }
        }
        [HttpGet]
        public OrderInfoViewModel? GetOrderInfo(int Id)
        {
            try
            {
                return _orderInfo.ReadElement(new OrderInfoSearchModel { Id = Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка получения мебельного модуля по id={Id}", Id);
                throw;
            }
        }
        [HttpGet]
        public List<OrderInfoViewModel>? GetOrderInfoListByUser(int userId)
        {
            try
            {
                return _orderInfo.ReadList(new OrderInfoSearchModel { UserId = userId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка получения списка гарнитуров у пользователя по id={userId}", userId);
                throw;
            }
        }
        [HttpGet]
        public List<List<OrderInfoViewModel>>? GetOrderInfoListByDate()
        {
            try
            {
                List<List<OrderInfoViewModel>> list = new List<List<OrderInfoViewModel>>();
                for (int i = 1; i <= 12; i++)
                {
                    var resp = _orderInfo.ReadList(new OrderInfoSearchModel
                    {
                        DateFrom = new DateTime(DateTime.Today.Year - 1, i, 1, 0, 0, 0),
                        DateTo = new DateTime(DateTime.Today.Year - 1, i, DateTime.DaysInMonth(DateTime.Today.Year - 1, i) - 1, 23, 59, 59)
                    });
                    list.Add(resp == null ? null : resp);
                }
                return list;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка получения списка заказов по времени}");
                throw;
            }
        }
        [HttpGet]
        public List<Tuple<string, double>>? GetGraphicUsersByPreviousMonth()
        {
            try
            {
                List<Tuple<string, double>> list = new List<Tuple<string, double>>();
                List<UserViewModel> users = _user.ReadList(new UserSearchModel { RoleId = 4 });
                foreach (var user in users)
                {
                    var resp = _orderInfo.ReadList(new OrderInfoSearchModel
                    {
                        DateFrom = new DateTime(DateTime.Today.Year, DateTime.Today.Month-1, 1, 0, 0, 0),
                        DateTo = new DateTime(DateTime.Today.Year, DateTime.Today.Month - 1, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month - 1), 23, 59, 59),
                        UserId = user.Id
                    });
                    list.Add(new Tuple<string, double>(user.Name, resp.Sum(x => x.Sum)));
                }
                return list;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка получения списка заказов по времени}");
                throw;
            }
        }
        [HttpGet]
        public List<Tuple<string, double>>? GetGraphicOrdersByPreviousDay()
        {
            try
            {
                List<Tuple<string, double>> list = new List<Tuple<string, double>>();
                var orderInfos = _orderInfo.ReadList(new OrderInfoSearchModel
                {
                    DateFrom = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day - 1, 0, 0, 0),
                    DateTo = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day - 1, 23, 59, 59)
                });
                foreach (var orderInfo in orderInfos)
                {
                    list.Add(new Tuple<string, double>(orderInfo.Id.ToString(), orderInfo.Sum));
                }
                return list;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка получения списка заказов по времени}");
                throw;
            }
        }
        [HttpGet]
        public List<Tuple<string, double>>? GetGraphicOrdersByPaymentType()
        {
            try
            {
                List<(PaymentType, double)> list = new List<(PaymentType, double)>();
                list.Add((PaymentType.Наличными, 0));
                list.Add((PaymentType.Картой, 0));
                list.Add((PaymentType.Смешанный, 0));
                List<OrderInfoViewModel> orderInfos = _orderInfo.ReadList(null);
                foreach (var orderInfo in orderInfos)
                {
                    switch (orderInfo.PaymentType)
                    {
                        case PaymentType.Наличными:
                            list[0] = (PaymentType.Наличными, list[0].Item2 + orderInfo.Sum);
                            break;
                        case PaymentType.Картой:
                            list[1] = (PaymentType.Картой, list[1].Item2 + orderInfo.Sum);
                            break;
                        case PaymentType.Смешанный:
                            list[2] = (PaymentType.Смешанный, list[2].Item2 + orderInfo.Sum);
                            break;
                    }
                }
                List<Tuple<string, double>> listRes = new List<Tuple<string, double>>();
                foreach (var el in list)
                {
                    switch (el.Item1)
                    {
                        case PaymentType.Наличными:
                            listRes.Add(new Tuple<string, double>("Наличными", el.Item2));
                            break;
                        case PaymentType.Картой:
                            listRes.Add(new Tuple<string, double>("Картой", el.Item2));
                            break;
                        case PaymentType.Смешанный:
                            listRes.Add(new Tuple<string, double>("Смешанный", el.Item2));
                            break;
                    }
                }
                return listRes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка получения списка заказов по времени}");
                throw;
            }
        }
        [HttpPost]
        public OrderInfoViewModel? AddOrderInfo(OrderInfoBindingModel model)
        {
            try
            {
                return _orderInfo.Create(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка создания мебельного модуля");
                throw;
            }
        }
        [HttpPost]
        public void UpdateOrderInfo(OrderInfoBindingModel model)
        {
            try
            {
                _orderInfo.Update(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка обновления мебельного модуля");
                throw;
            }
        }
        [HttpPost]
        public void DeleteOrderInfo(OrderInfoBindingModel model)
        {
            try
            {
                _orderInfo.Delete(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка удаления мебельного модуля");
                throw;
            }
        }
    }
}
