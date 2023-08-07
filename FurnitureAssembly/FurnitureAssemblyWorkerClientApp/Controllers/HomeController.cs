using FurnitureAssemblyWorkerClientApp.Models;
using FurnitureAssemblyContracts.BindingModels;
using FurnitureAssemblyContracts.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using FurnitureAssemblyDataModels.Models;
using System;
using FurnitureAssemblyContracts.SearchModels;
using FurnitureAssemblyDataModels.Enums;
using System.Web.Helpers;
using System.Reflection;
using FurnitureAssemblyBusinessLogic.BusinessLogics;
using FurnitureAssemblyContracts.BusinessLogicContracts;
using FurnitureAssemblyDatabaseImplement.Models;

namespace FurnitureAssemblyWorkerClientApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IReportWorkerLogic _report;
        public HomeController(ILogger<HomeController> logger, IReportWorkerLogic report)
        {
            _logger = logger;
            _report = report;
        }

		[HttpGet]
		public IActionResult Index()
		{
			if (APIClient.User == null)
			{
				return Redirect("~/Home/Enter");
			}
			return View(APIClient.GetRequest<List<OrderViewModel>>($"api/order/getorderlistbyuser?userId={APIClient.User.Id}"));
		}
		[HttpGet]
		public IActionResult Privacy()
		{
			if (APIClient.User == null)
			{
				return Redirect("~/Home/Enter");
			}
			return View(APIClient.User);
		}
		[HttpPost]
		public void Privacy(string login, string password, string name, int roleId)
		{
			if (APIClient.User == null)
			{
				throw new Exception("Вы как суда попали? Суда вход только авторизованным");
			}
			if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(name))
			{
				throw new Exception("Введите логин, пароль и ФИО");
			}
			APIClient.PostRequest("api/user/updateuser", new UserBindingModel
			{
				Id = APIClient.User.Id,
				Name = name,
				Login = login,
				Password = password,
				RoleId = roleId
			});

			APIClient.User.Name = name;
			APIClient.User.Login = login;
			APIClient.User.Password = password;
			APIClient.User.RoleId = roleId;
			Response.Redirect("Index");
		}
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
		[HttpGet]
		public IActionResult Enter()
		{
			return View();
		}
		[HttpPost]
		public void Enter(string login, string password)
		{
			if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
			{
				throw new Exception("Введите логин и пароль");
			}
			APIClient.User = APIClient.GetRequest<UserViewModel>($"api/user/login?login={login}&password={password}");
            
			if (APIClient.User == null)
			{
				throw new Exception("Неверный логин/пароль");
			}
			if (APIClient.User.RoleName != "Работник")
			{
				APIClient.User = null;
				throw new Exception("Данному сотруднику вход запрещен");
			}
			Response.Redirect("Orders");
		}
		[HttpGet]
		public IActionResult Register()
		{
			return View(APIClient.GetRequest<List<RoleViewModel>>($"api/role/getrolelist"));
		}
		[HttpPost]
		public void Register(string login, string password, string name, int roleId)
		{
			if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(name))
			{
				throw new Exception("Введите логин, пароль и ФИО");
			}
			APIClient.PostRequest("api/user/adduser", new UserBindingModel
			{
				Name = name,
				Login = login,
				Password = password,
				RoleId = roleId
			});
			Response.Redirect("Enter");
			return;
		}
        [HttpGet]
        public IActionResult Sets()
        {
            if (APIClient.User == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIClient.GetRequest<List<SetViewModel>>($"api/set/getsetlistbyuser?userId={APIClient.User.Id}"));
        }
		[HttpGet]
		public IActionResult CreateSet()
		{
			return View((APIClient.GetRequest<List<FurnitureModuleViewModel>>($"api/furnituremodule/getfurnituremodulelistbyuser?userId={APIClient.User.Id}")));
		}
		[HttpPost]
        public void CreateSet(string name, double cost, int[] furnitureModuleIds, int[] counts)
        {
            if (APIClient.User == null)
            {
                throw new Exception("Вы как суда попали? Суда вход только авторизованным");
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("Название гарнитура не указано");
            }
            if (cost <= 0)
            {
                throw new Exception("Стоимость гарнитура не корректна");
            }
            var set = APIClient.PostRequest("api/set/addset", new SetBindingModel
            {
                Name = name,
                Cost = cost,
				UserId = APIClient.User.Id
            });
            if (furnitureModuleIds != null && furnitureModuleIds.Length > 0 && counts != null && furnitureModuleIds.Length == counts.Length)
            {
                for (int i = 0; i < counts.Length; i++)
                {
                    APIClient.PostRequest("api/set/addfurnituremoduleinset", Tuple.Create(
                        new SetSearchModel() { Id = set.Id },
                        new FurnitureModuleViewModel() { Id = furnitureModuleIds[i] },
                        counts[i]
                    ));
                }
            }
            Response.Redirect("Sets");
        }
        [HttpGet]
        public Tuple<SetViewModel, string>? GetSetWithFurnitureModules(int setId)
        {
            var result = APIClient.GetRequest<Tuple<SetViewModel, List<FurnitureModuleViewModel>, List<int>>>
                ($"api/set/getsetwithfurnituremodules?setId={setId}");
            if (result == null)
            {
                return default;
            }
            string furnitureModuleTable = "";
            for (int i = 0; i < result.Item2.Count; i++)
            {
                var furnitureModule = result.Item2[i];
                var count = result.Item3[i];
                furnitureModuleTable += "<tr>";
                furnitureModuleTable += $"<td>{furnitureModule.Name}</td>";
                furnitureModuleTable += $"<td>{furnitureModule.Cost}</td>";
                furnitureModuleTable += $"<td>{furnitureModule.DateCreate.Date}</td>";
                furnitureModuleTable += $"<td>{count}</td>";
                furnitureModuleTable += "</tr>";
            }
            return Tuple.Create(result.Item1, furnitureModuleTable);
        }
		[HttpGet]
		public IActionResult UpdateSet()
		{
			if (APIClient.User == null)
			{
				return Redirect("~/Home/Enter");
			}
            return View(APIClient.GetRequest<List<SetViewModel>>($"api/set/getsetlistbyuser?userId={APIClient.User.Id}"));
		}
		[HttpPost]
		public void UpdateSet(int set, string name, double cost, DateTime dateCreate)
		{
			if (APIClient.User == null)
			{
				throw new Exception("Вы как суда попали? Суда вход только авторизованным");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new Exception("Название гарнитура не указано");
			}
			if (cost <= 0)
			{
				throw new Exception("Стоимость гарнитура не корректна");
			}
			var listFurnitureModules = APIClient.GetRequest<Dictionary<int, (IFurnitureModuleModel, int)>>($"api/set/getsetfurnituremodules?setId={set}");
			APIClient.PostRequest("api/set/updateset", new SetBindingModel
			{
				Id = set,
				Name = name,
				Cost = cost,
				DateCreate = dateCreate.Date,
				SetFurnitureModules = listFurnitureModules!
			});
			Response.Redirect("Sets");
		}
		[HttpGet]
		public IActionResult DeleteSet()
		{
			if (APIClient.User == null)
			{
				return Redirect("~/Home/Enter");
			}
			return View(APIClient.GetRequest<List<SetViewModel>>($"api/set/getsetlistbyuser?userId={APIClient.User.Id}"));
		}
		[HttpPost]
		public void DeleteSet(int set)
		{
			if (APIClient.User == null)
			{
				throw new Exception("Вы как суда попали? Суда вход только авторизованным");
			}
			APIClient.PostRequest("api/set/deleteset", new SetBindingModel
			{
				Id = set,
			});
			Response.Redirect("Sets");
		}
		[HttpGet]
		public IActionResult AddFurnitureModuleInSet()
		{
            if (APIClient.User == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(Tuple.Create(APIClient.GetRequest<List<SetViewModel>>($"api/set/getsetlistbyuser?userId={APIClient.User.Id}"), APIClient.GetRequest<List<FurnitureModuleViewModel>>($"api/furnituremodule/getfurnituremodulelistbyuser?userId={APIClient.User.Id}")));
		}
		[HttpPost]
		public void AddFurnitureModuleInSet(int set, int furnitureModule, int count)
		{
			if (APIClient.User == null)
			{
				throw new Exception("Вы как суда попали? Суда вход только авторизованным");
			}
			if (count <= 0)
			{
				throw new Exception("Количество должно быть больше 0");
			}
			APIClient.PostRequest("api/set/addfurnituremoduleinset", Tuple.Create(
				new SetSearchModel() { Id = set },
				new FurnitureModuleViewModel() { Id = furnitureModule },
				count
			));
			Response.Redirect("Sets");
		}
		[HttpGet]
		public IActionResult ListSetsFurnitureModulesToFile()
		{
            if (APIClient.User == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIClient.GetRequest<List<SetViewModel>>($"api/set/getsetlistbyuser?userId={APIClient.User.Id}"));
        }
        [HttpPost]
        public void ListSetsFurnitureModulesToFile(int[] setIds, string type)
        {
            if (APIClient.User == null)
            {
                throw new Exception("Вы как суда попали? Суда вход только авторизованным");
            }
            if (setIds.Length <= 0)
            {
                throw new Exception("Количество должно быть больше 0");
            }
			if (string.IsNullOrEmpty(type))
			{
                throw new Exception("Неверный тип отчета");
            }

            List<int> res = new List<int>();
            foreach (var item in setIds)
            {
                res.Add(item);
            }
			if (type == "docx")
			{
                APIClient.PostRequest("api/report/createreporttodocx", new ReportWorkerBindingModel
                {
                    SetIds = res
                });
                Response.Redirect("GetDocxFile");
            } 
			else
			{
                APIClient.PostRequest("api/report/createreporttoxlsx", new ReportWorkerBindingModel
                {
                    SetIds = res
                });
                Response.Redirect("GetXlsxFile");
            }
        }
		[HttpGet]
        public IActionResult GetDocxFile()
        {
            return new PhysicalFileResult("C:\\temp\\word_worker.docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
        }
        public IActionResult GetXlsxFile()
        {
            return new PhysicalFileResult("C:\\temp\\excel_worker.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
        public IActionResult GetPdfFile()
        {
            return new PhysicalFileResult("C:\\temp\\pdf_worker.pdf", "application/pdf");
        }
        [HttpGet]
        public IActionResult FurnitureModules()
        {
            if (APIClient.User == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIClient.GetRequest<List<FurnitureModuleViewModel>>($"api/furnituremodule/getfurnituremodulelistbyuser?userId={APIClient.User.Id}"));
        }
        [HttpGet]
        public IActionResult CreateFurnitureModule()
        {
            return View();
        }
        [HttpPost]
        public void CreateFurnitureModule(string name, double cost)
        {
            if (APIClient.User == null)
            {
                throw new Exception("Вы как суда попали? Суда вход только авторизованным");
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("Название мебельного модуля не указано");
            }
            if (cost <= 0)
            {
                throw new Exception("Стоимость мебельного модуля не корректна");
            }
            APIClient.PostRequest("api/furnituremodule/addfurnituremodule", new FurnitureModuleBindingModel
            {
                Name = name,
                Cost = cost,
                UserId = APIClient.User.Id
            });
            Response.Redirect("FurnitureModules");
        }
        [HttpGet]
        public IActionResult UpdateFurnitureModule()
        {
            if (APIClient.User == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIClient.GetRequest<List<FurnitureModuleViewModel>>($"api/furnituremodule/getfurnituremodulelistbyuser?userId={APIClient.User.Id}"));
        }
        [HttpPost]
        public void UpdateFurnitureModule(int furnitureModule, string name, double cost, DateTime dateCreate)
        {
            if (APIClient.User == null)
            {
                throw new Exception("Вы как суда попали? Суда вход только авторизованным");
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("Название мебельного модуля не указано");
            }
            if (cost <= 0)
            {
                throw new Exception("Стоимость мебельного модуля не корректна");
            }
            APIClient.PostRequest("api/furnituremodule/updatefurnituremodule", new FurnitureModuleBindingModel
            {
                Id = furnitureModule,
                Name = name,
                Cost = cost,
                DateCreate = dateCreate.Date
            });
            Response.Redirect("FurnitureModules");
        }
		[HttpGet]
		public IActionResult DeleteFurnitureModule()
		{
			if (APIClient.User == null)
			{
				return Redirect("~/Home/Enter");
			}
			return View(APIClient.GetRequest<List<FurnitureModuleViewModel>>($"api/furnituremodule/getfurnituremodulelistbyuser?userId={APIClient.User.Id}"));
		}
		[HttpPost]
		public void DeleteFurnitureModule(int furnitureModule)
		{
			if (APIClient.User == null)
			{
				throw new Exception("Вы как суда попали? Суда вход только авторизованным");
			}
			APIClient.PostRequest("api/furnituremodule/deletefurnituremodule", new FurnitureModuleBindingModel
			{
				Id = furnitureModule,
			});
			Response.Redirect("FurnitureModules");
		}
        [HttpGet]
        public FurnitureModuleViewModel? GetFurnitureModule(int furnitureModuleId)
        {
            var result = APIClient.GetRequest<FurnitureModuleViewModel>
                ($"api/furnitureModule/getfurnituremodule?Id={furnitureModuleId}");
            if (result == null)
            {
                return default;
            }
            return result;
        }
		[HttpGet]
		public IActionResult AddFurnitureModulesInSet()
		{
			if (APIClient.User == null)
			{
				return Redirect("~/Home/Enter");
			}
			return View(Tuple.Create(APIClient.GetRequest<List<SetViewModel>>($"api/set/getsetlistbyuser?userId={APIClient.User.Id}"), APIClient.GetRequest<List<FurnitureModuleViewModel>>($"api/furnituremodule/getfurnituremodulelistbyuser?userId={APIClient.User.Id}")));
		}
		[HttpPost]
		public void AddFurnitureModulesInSet(int set, int[] furnitureModuleIds, int[] counts)
		{
			if (APIClient.User == null)
			{
				throw new Exception("Вы как суда попали? Суда вход только авторизованным");
			}
			if (furnitureModuleIds.Length != counts.Length)
			{
				throw new Exception("Массивы не совпадают");
			}
			for (int i = 0; i < furnitureModuleIds.Length; i++)
			{
				APIClient.PostRequest("api/set/addfurnituremoduleinset", Tuple.Create(
					new SetSearchModel() { Id = set },
					new FurnitureModuleViewModel() { Id = furnitureModuleIds[i] },
					counts[i]
				));
			}
            Response.Redirect("AddFurnitureModulesInSet");
		}
		[HttpGet]
		public IActionResult Orders()
		{
            if (APIClient.User == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIClient.GetRequest<List<OrderInfoViewModel>>($"api/orderinfo/getorderinfolistbyuser?userId={APIClient.User.Id}"));
		}
		[HttpGet]
		public IActionResult CreateOrder()
		{
            if (APIClient.User == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIClient.GetRequest<List<SetViewModel>>($"api/set/getsetlistbyuser?userId={APIClient.User.Id}"));
        }
		[HttpPost]
		public void CreateOrder(string name, PaymentType paymentType, int[] setIds, int[] counts)
		{
            if (APIClient.User == null)
            {
                throw new Exception("Вы как суда попали? Суда вход только авторизованным");
            }
			if (string.IsNullOrEmpty(name))
			{
                throw new Exception("Имя не должно быть пустым");
            }
            var orderInfo = APIClient.PostRequest("api/orderinfo/addorderinfo", new OrderInfoBindingModel
			{
				CustomerName = name,
				PaymentType = paymentType,
				UserId = APIClient.User.Id
            });
			if (orderInfo == null)
			{
				throw new Exception("Ошибка создания заказа");
			}
			if (setIds != null && setIds.Length > 0 && counts != null && setIds.Length == counts.Length)
			{
				for (int i = 0; i < counts.Length; i++)
				{
                    APIClient.PostRequest("api/order/addorder", new OrderBindingModel
                    {
                        OrderInfoId = orderInfo.Id,
                        SetId = setIds[i],
                        Count = counts[i]
                    });
                }
            }
            Response.Redirect("Orders");
        }
        [HttpGet]
        public Tuple<OrderInfoViewModel, string>? GetOrderWithSets(int orderId)
        {
            var result = APIClient.GetRequest<Tuple<OrderInfoViewModel, List<SetViewModel>, List<int>>>
                ($"api/order/getorderwithsets?orderId={orderId}");
            if (result == null)
            {
                return default;
            }
            string setTable = "";
            for (int i = 0; i < result.Item2.Count; i++)
            {
                var set = result.Item2[i];
                var count = result.Item3[i];
                setTable += "<tr>";
                setTable += $"<td>{set.Name}</td>";
                setTable += $"<td>{set.Cost}</td>";
                setTable += $"<td>{set.DateCreate.Date}</td>";
                setTable += $"<td>{count}</td>";
                setTable += "</tr>";
            }
            return Tuple.Create(result.Item1, setTable);
        }
        [HttpGet]
        public IActionResult UpdateOrder()
        {
            if (APIClient.User == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIClient.GetRequest<List<OrderInfoViewModel>>($"api/orderinfo/getorderinfolistbyuser?userId={APIClient.User.Id}"));
        }
        [HttpPost]
        public void UpdateOrder(int order, string name, DateTime dateCreate, PaymentType paymentType, double sum)
        {
            if (APIClient.User == null)
            {
                throw new Exception("Вы как суда попали? Суда вход только авторизованным");
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("Название гарнитура не указано");
            }
            APIClient.PostRequest("api/orderinfo/updateorderinfo", new OrderInfoBindingModel
            {
                Id = order,
                CustomerName = name,
                DateCreate = dateCreate.Date,
				PaymentType = paymentType,
				UserId = APIClient.User.Id,
				Sum = sum
            });
            Response.Redirect("Orders");
        }
		[HttpGet]
		public IActionResult DeleteOrder()
		{
			if (APIClient.User == null)
			{
				return Redirect("~/Home/Enter");
			}
			return View(APIClient.GetRequest<List<OrderInfoViewModel>>($"api/orderinfo/getorderinfolistbyuser?userId={APIClient.User.Id}"));
		}
		[HttpPost]
		public void DeleteOrder(int order)
		{
			if (APIClient.User == null)
			{
				throw new Exception("Вы как суда попали? Суда вход только авторизованным");
			}
			APIClient.PostRequest("api/orderinfo/deleteorderinfo", new OrderInfoBindingModel
			{
				Id = order,
			});
			Response.Redirect("Orders");
		}
		[HttpGet]
		public IActionResult AddSetInOrder()
		{
			if (APIClient.User == null)
			{
				return Redirect("~/Home/Enter");
			}
			return View(Tuple.Create(APIClient.GetRequest<List<OrderInfoViewModel>>($"api/orderinfo/getorderinfolistbyuser?userId={APIClient.User.Id}"), APIClient.GetRequest<List<SetViewModel>>($"api/set/getsetlistbyuser?userId={APIClient.User.Id}")));
		}
		[HttpPost]
		public void AddSetInOrder(int order, int[] setIds, int[] counts)
		{
			if (APIClient.User == null)
			{
				throw new Exception("Вы как суда попали? Суда вход только авторизованным");
			}
			if (setIds != null && counts != null && counts.Length > 0 && counts.Length == setIds.Length)
			{
				for (int i = 0; i < counts.Length; i++)
				{
					if (counts[i] <= 0)
					{
						throw new Exception("Количество должно быть больше 0");
					}
					APIClient.PostRequest("api/order/addsetinorder", Tuple.Create(
						new OrderInfoSearchModel() { Id = order },
						new SetSearchModel() { Id = setIds[i] },
						counts[i]
					));
				}
			}
			Response.Redirect("Orders");
		}
        [HttpGet]
        public IActionResult OrdersReport()
        {
            if (APIClient.User == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View("OrdersReport");
        }
        [HttpPost]
        public void OrdersReport(DateTime dateFrom, DateTime dateTo, string customerEmail)
        {
            if (APIClient.User == null)
            {
                throw new Exception("Вы как суда попали? Суда вход только авторизованным");
            }
            if (string.IsNullOrEmpty(customerEmail))
            {
                throw new Exception("Email пуст");
            }
            APIClient.PostRequest("api/report/createreportorderstopdf", new ReportWorkerBindingModel
            {
                DateFrom = dateFrom,
                DateTo = dateTo,
                UserId = APIClient.User.Id
            });
            APIClient.PostRequest("api/report/sendpdftomail", new MailSendInfoBindingModel
			{
				MailAddress = customerEmail,
				Subject = "Отчет по заказам",
				Text = "Отчет по заказам с " + dateFrom.ToShortDateString() + " до " + dateTo.ToShortDateString()
			});
            Response.Redirect("OrdersReport");
        }
		[HttpGet]
		public string GetOrdersReport(DateTime dateFrom, DateTime dateTo)
		{
            if (APIClient.User == null)
            {
                throw new Exception("Вы как суда попали? Суда вход только авторизованным");
            }
            Tuple<List<ReportOrdersWorkerViewModel>,double> result;
            try
            {
                result = _report.GetOrders(new ReportWorkerBindingModel
                {
                    UserId = APIClient.User.Id,
                    DateFrom = dateFrom,
                    DateTo = dateTo
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка создания отчета");
                throw;
            }
            if (result == null)
            {
                return "error";
            }
            string html = "";
            foreach (var report in result.Item1)
            {
                html += $"<h2>{report.SetName}</h2>";
                html += $"<table>";
                html += "<thead>";
                html += "<tr>";
                html += $"<th>Номер</th>";
                html += $"<th>Дата заказа</th>";
                html += $"<th>Мебельный модуль</th>";
                html += $"<th>Сумма</th>";
                html += "</tr>";
                html += "</thead>";
                int i = 1;
                html += "<tbody>";
                foreach (var furnitureModule in report.FurnitureModules)
                {
                    html += "<tr>";
                    html += $"<td>{i}</td>";
                    html += $"<td>{report.DateCreate}</td>";
                    html += $"<td>{furnitureModule.Name}</td>";
                    html += $"<td>{furnitureModule.Cost}</td>";
                    html += "</tr>";
                    i++;
                }
                html += "</tbody>";
                html += "</table>";
                html += $"<p style=\"align-self: self-end;\">Итого: {report.Sum}</p>";
            }
            html += $"<h3 style=\"align-self: self-start;\">Итого: {result.Item2}</h3>";
            return html;
        }
        [HttpGet]
        public IActionResult GraphicOrdersByPreviousYear()
        {
            if (APIClient.User == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIClient.GetRequest<List<List<OrderInfoViewModel>>>($"api/orderinfo/getorderinfolistbydate"));
        }
        [HttpGet]
        public IActionResult Graphics()
        {
            if (APIClient.User == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View();
        }
        [HttpGet]
        public IActionResult GraphicUsersByPreviousMonth()
        {
            if (APIClient.User == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View();
        }
        [HttpGet]
        public List<Tuple<string, double>> GetGraphicUsersByPreviousMonth()
        {
            if (APIClient.User == null)
            {
                throw new Exception("Вы как суда попали? Суда вход только авторизованным");
            }
            return APIClient.GetRequest<List<Tuple<string, double>>>($"api/orderinfo/getgraphicusersbypreviousmonth");
        }
        [HttpGet]
        public IActionResult GraphicOrdersByPaymentType()
        {
            if (APIClient.User == null)
            {
                throw new Exception("Вы как суда попали? Суда вход только авторизованным");
            }
            return View();
        }
        [HttpGet]
        public List<Tuple<string, double>> GetGraphicOrdersByPaymentType()
        {
            if (APIClient.User == null)
            {
                throw new Exception("Вы как суда попали? Суда вход только авторизованным");
            }
            return APIClient.GetRequest<List<Tuple<string, double>>>($"api/orderinfo/getgraphicordersbypaymenttype") ;
        }
        [HttpGet]
        public IActionResult GraphicOrdersByPreviousDay()
        {
            if (APIClient.User == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View();
        }
        [HttpGet]
        public List<Tuple<string, double>> GetGraphicOrdersByPreviousDay()
        {
            if (APIClient.User == null)
            {
                throw new Exception("Вы как суда попали? Суда вход только авторизованным");
            }
            return APIClient.GetRequest<List<Tuple<string, double>>>($"api/orderinfo/GetGraphicOrdersByPreviousDay");
        }
    }
}