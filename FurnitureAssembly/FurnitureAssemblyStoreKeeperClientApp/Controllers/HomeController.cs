using FurnitureAssemblyBusinessLogic.BusinessLogics;
using FurnitureAssemblyContracts.BindingModels;
using FurnitureAssemblyContracts.SearchModels;
using FurnitureAssemblyContracts.StorageContracts;
using FurnitureAssemblyContracts.ViewModels;
using FurnitureAssemblyDataModels.Models;
using FurnitureAssemblyStoreKeeperClientApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FurnitureAssemblyStoreKeeperClientApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private readonly IFurnitureStorage _furnitureStorage;
		private readonly IReportStorekeeperLogic _report;
		public HomeController(ILogger<HomeController> logger, IFurnitureStorage furnitureStorage, IReportStorekeeperLogic report)
        {
            _logger = logger;
			_furnitureStorage = furnitureStorage;
			_report = report;
        }

        public IActionResult Index()
        {
            return View();
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
			else if(APIClient.User.RoleName != "Кладовщик")
            {
				throw new Exception("Данное приложение недоступно для вашей роли");
			}
			Response.Redirect("Index");
		}
		[HttpGet]
		public IActionResult Register()
		{
			return View(APIClient.GetRequest<List<RoleViewModel>>($"api/role/getrolelist"));
		}
		[HttpPost]
		public void Register(string login, string password, string name, int roleid)
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
				RoleId = roleid
			});
			Response.Redirect("Enter");
			return;
		}
        #region Scope
        [HttpGet]
		public IActionResult GetScopeList()
		{
			return View(APIClient.GetRequest<List<ScopeViewModel>>($"api/scope/GetScopeList"));
		}

		[HttpGet]
		public IActionResult CreateScope()
		{
			return View();
		}

		[HttpPost]
		public void CreateScope(string name)
		{
			if (APIClient.User == null)
			{
				throw new Exception("Вы как суда попали?");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new Exception("Название не указано");
			}
			
			APIClient.PostRequest("api/scope/addscope", new ScopeBindingModel
			{
				Name = name
			});
			Response.Redirect("GetScopeList");
		}

		[HttpGet]
		public IActionResult UpdateScope()
		{
			if (APIClient.User == null)
			{
				return Redirect("~/Home/Enter");
			}
			return View(APIClient.GetRequest<List<ScopeViewModel>>($"api/scope/GetScopeList"));
		}
		[HttpPost]
		public void UpdateScope(int scope, string name)
		{
			if (APIClient.User == null)
			{
				throw new Exception("Вы как суда попали?");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new Exception("Название не указано");
			}
			
			APIClient.PostRequest("api/scope/updatescope", new ScopeBindingModel
			{
				Id = scope,
				Name = name
			});
			Response.Redirect("GetScopeList");
		}
		[HttpGet]
		public IActionResult DeleteScope()
		{
			if (APIClient.User == null)
			{
				return Redirect("~/Home/Enter");
			}
			return View(APIClient.GetRequest<List<ScopeViewModel>>($"api/scope/GetScopeList"));
		}
		[HttpPost]
		public void DeleteScope(int scope)
		{
			if (APIClient.User == null)
			{
				throw new Exception("Вы как суда попали");
			}
			APIClient.PostRequest("api/scope/deletescope", new ScopeViewModel
			{
				Id = scope,
			});
			Response.Redirect("GetScopeList");
		}
#endregion
		#region Material
		[HttpGet]
		public IActionResult GetMaterialsList()
		{

			return View(APIClient.GetRequest<List<MaterialViewModel>>($"api/material/getmateriallist"));
		}

		[HttpGet]
		public IActionResult CreateMaterial()
		{
			return View();
		}

		[HttpPost]
		public void CreateMaterial(string name, double cost, int scopeId,int UserId)
		{
			if (APIClient.User == null)
			{
				throw new Exception("Вы как суда попали?");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new Exception("Название не указано");
			}
			if (cost <= 0)
			{
				throw new Exception("Цена должна быть положительной");
			}
			APIClient.PostRequest("api/material/addmaterial", new MaterialBindingModel
			{
				Name = name,
				Cost = cost,
				ScopeId = scopeId,
				UserId = UserId
			});
			Response.Redirect("GetMaterialsList");
		}

		[HttpGet]
		public IActionResult UpdateMaterial()
		{
			if (APIClient.User == null)
			{
				return Redirect("~/Home/Enter");
			}
			return View(APIClient.GetRequest<List<MaterialViewModel>>($"api/material/GetMaterialList"));
		}
		[HttpPost]
		public void UpdateMaterial(int material, string name, double cost, int scope, int user)
		{
			if (APIClient.User == null)
			{
				throw new Exception("Вы как суда попали?");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new Exception("Название не указано");
			}

			APIClient.PostRequest("api/material/updatematerial", new MaterialBindingModel
			{
				Id = material,
				Name = name,
				Cost = cost,
				ScopeId = scope,
				UserId = user
			});
			Response.Redirect("GetMaterialsList");
		}
		[HttpGet]
		public IActionResult DeleteMaterial()
		{
			if (APIClient.User == null)
			{
				return Redirect("~/Home/Enter");
			}
			return View(APIClient.GetRequest<List<MaterialViewModel>>($"api/material/GetMaterialList"));
		}
		[HttpPost]
		public void DeleteMaterial(int material)
		{
			if (APIClient.User == null)
			{
				throw new Exception("Вы как суда попали");
			}
			APIClient.PostRequest("api/material/deletematerial", new MaterialBindingModel
			{
				Id = material,
			});
			Response.Redirect("GetMaterialsList");
		}
		#endregion

		#region Furniture
		[HttpGet]
		public IActionResult GetFurnitureList()
		{
			return View(_furnitureStorage.GetFullList());
			//return View(APIClient.GetRequest<List<FurnitureViewModel>>($"api/furniture/getfurniturelist"));
		}

		[HttpGet]
		public IActionResult CreateFurniture()
		{
			return View();
		}
		[HttpPost]
		public double Calc(int count, int furniture)
		{
			var prod = APIClient.GetRequest<FurnitureViewModel>($"api/main/getfurniture?furnitureId={furniture}"
			);
			return count * (prod?.Cost ?? 1);
		}
		[HttpPost]
		public void CreateFurniture(string name, int userId, int[] MaterialsIds, int[] counts)
		{
			if (APIClient.User == null)
			{
				throw new Exception("Вы как суда попали?");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new Exception("Название не указано");
			}
			if(counts == null || MaterialsIds.Length != counts.Length)
            {
				throw new Exception("Неверно заполнены компоненты");
			}
			var materials = APIClient.GetRequest<List<MaterialViewModel>>($"api/material/getmateriallist");
			Dictionary<int, (IMaterialModel, int)> FurnitureMaterials = new Dictionary<int, (IMaterialModel, int)>();
			double cost = 0;
			for(int i = 0; i<counts.Length; i++)
            {
				var elem = (materials.Where(m => m.Id == MaterialsIds[i]).FirstOrDefault(), counts[i]);
				FurnitureMaterials.Add(MaterialsIds[i], elem);
				cost += counts[i] * materials.Where(m => m.Id == MaterialsIds[i]).FirstOrDefault().Cost;

			}
			_furnitureStorage.Insert(new FurnitureBindingModel
			{
				Name = name,
				Cost = cost,
				UserId = userId,
				FurnitureMaterials = FurnitureMaterials,
				DateCreate = DateTime.UtcNow
			});
			//APIClient.PostRequest("api/furniture/addfurniture", new FurnitureBindingModel
			//{
			//	Name = name,
			//	Cost = cost,
			//	UserId = userId,
			//	DateCreate = DateTime.UtcNow,
			//	FurnitureMaterials = FurnitureMaterials
			//});
			Response.Redirect("GetMaterialsList");
		}

		[HttpGet]
		public IActionResult UpdateFurniture(int id)
		{
			if (APIClient.User == null)
			{
				return Redirect("~/Home/Enter");
			}
			var model = _furnitureStorage.GetElement(new FurnitureSearchModel
			{
				Id = id

			});
			if(model!= null)
            {
				return View(model);

			}
			else return Redirect("~/Home/GetFurnitureList");
			//var model = APIClient.GetRequest<FurnitureViewModel>($"api/furniture/GetFurniture?id={id}");
			//return model != null ? View(model) : RedirectToPage("GetFurnitureList");
		}
		[HttpPost]
		public void UpdateFurniture(int id, string name, double cost, int userId, int[] MaterialsIds, int[] counts)
		{
			if (APIClient.User == null)
			{
				throw new Exception("Вы как суда попали?");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new Exception("Название не указано");
			}
			var materials = APIClient.GetRequest<List<MaterialViewModel>>($"api/material/getmateriallist");
			Dictionary<int, (IMaterialModel, int)> FurnitureMaterials = new Dictionary<int, (IMaterialModel, int)>();
			double price = 0;
			for (int i = 0; i < counts.Length; i++)
			{
				var elem = (materials.Where(m => m.Id == MaterialsIds[i]).FirstOrDefault(), counts[i]);
				FurnitureMaterials.Add(MaterialsIds[i], elem);
				price += elem.Item1.Cost * elem.Item2;
			}
			_furnitureStorage.Update(new FurnitureBindingModel
			{
				Id = id,
				Name = name,
				Cost = price,
				UserId = userId,
				FurnitureMaterials = FurnitureMaterials
			});

			Response.Redirect("GetFurnitureList");
		}
		[HttpGet]
		public IActionResult DeleteFurniture()
		{
			if (APIClient.User == null)
			{
				return Redirect("~/Home/Enter");
			}
			return View(APIClient.GetRequest<List<FurnitureViewModel>>($"api/furniture/GetFurnitureList"));
		}
		[HttpPost]
		public void DeleteFurniture(int furniture)
		{
			if (APIClient.User == null)
			{
				throw new Exception("Вы как суда попали");
			}
			APIClient.PostRequest("api/furniture/deletefurniture", new MaterialBindingModel
			{
				Id = furniture,
			});
			Response.Redirect("GetFurnitureList");
		}
		#endregion

		#region Reports
		[HttpGet]
		public IActionResult CreateReport()
		{
			if (APIClient.User == null)
			{
				return Redirect("~/Home/Enter");
			}
			return View(_furnitureStorage.GetFullList());
		}

		public void CreateReport(int[] setIds, string type)
		{
			if (APIClient.User == null)
			{
				throw new Exception("Вы как суда попали? Суда вход только авторизованным");
			}
			if (string.IsNullOrEmpty(type))
			{
				throw new Exception("Неверный тип отчета");
			}
			if (type == "docx")
			{
				//_reportStorekeeper.SaveFurnituresToWordFile(new ReportBindingModel
				//{
				//	FileName = "DocFile",
				//	Ids = setIds,
				//});
                APIClient.PostRequest("api/ReportStorekeeper/CreateReportToDocx", new ReportBindingModel
                {
                    Ids = setIds,
                });
                Response.Redirect("GetDocxFile");
			}
			else
			{
                _report.SaveFurnituresToExelFile(new ReportBindingModel
                {
                    Ids = setIds,
                });
                //APIClient.PostRequest("api/ReportStorekeeper/SaveFurnituresToExelFile", new ReportBindingModel
                //{
                //    FileName = "ExelFile",
                //    Ids = setIds,
                //});
                Response.Redirect("GetXlsxFile");
			}
		}

		[HttpGet]
		public IActionResult GetDocxFile()
		{
			return new PhysicalFileResult("C:\\temp\\word_storekeeper.docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
		}
		public IActionResult GetXlsxFile()
		{
			return new PhysicalFileResult("C:\\temp\\excel_storekeeper.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
		}

		public IActionResult CreateListMaterials()
		{
			if (APIClient.User == null)
			{
				return Redirect("~/Home/Enter");
			}
			return View();
		}
		[HttpPost]
		public void CreateListMaterials(DateTime dateFrom, DateTime dateTo, string customerEmail)
		{
			if (APIClient.User == null)
			{
				throw new Exception("Вы как суда попали? Суда вход только авторизованным");
			}
			if (string.IsNullOrEmpty(customerEmail))
			{
				throw new Exception("Email пуст");
			}
			APIClient.PostRequest("api/reportStorekeeper/CreateReportOrdersToPdf", new ReportBindingModel
			{
				DateFrom = dateFrom.ToUniversalTime(),
				DateTo = dateTo.ToUniversalTime(),
			});
			APIClient.PostRequest("api/reportStorekeeper/sendpdftomail", new MailSendInfoBindingModel
			{
				MailAddress = customerEmail,
				Subject = "Отчет по материалам",
				Text = "Отчет по материалам с " + dateFrom.ToShortDateString() + " до " + dateTo.ToShortDateString()
			});
			
			Response.Redirect("CreateListMaterials");
		}
		[HttpGet]
		public string GetMaterialReport(DateTime dateFrom, DateTime dateTo)
		{
			if (APIClient.User == null)
			{
				throw new Exception("Вы как суда попали? Суда вход только авторизованным");
			}
			List<ReportMaterialsViewModel> result;
			try
			{
				result = _report.GetOrders(new ReportBindingModel
				{
					DateFrom = dateFrom.ToUniversalTime(),
					DateTo = dateTo.ToUniversalTime()
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
			//html += $"<h2>{report.MaterialName}</h2>";
			html += $"<table>";
			html += "<thead>";
			html += "<tr>";
			html += $"<th>Материал</th>";
			html += $"<th>Сумма</th>";
			html += "</tr>";
			html += "</thead>";

			html += "<tbody>";
			double sum = 0;
			foreach (var report in result)
			{
				int i = 1;
				sum += report.Sum;
					html += "<tr>";
					html += $"<td>{i}</td>";
					html += $"<td>{report.MaterialName}</td>";
					html += $"<td>{report.Sum}</td>";
					html += "</tr>";
					i++;
				}
				html += "</tbody>";
				html += "</table>";
			html += $"<h3 style=\"align-self: self-start;\">Итого: {sum}</h3>";
			return html;
		}
		public IActionResult GetPdfFile()
		{
			return new PhysicalFileResult("C:\\temp\\pdf_storekeeper.pdf", "application/pdf");
		}
		#endregion
	}
}