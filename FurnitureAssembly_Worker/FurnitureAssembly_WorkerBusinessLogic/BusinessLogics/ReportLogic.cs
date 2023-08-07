using FurnitureAssembly_WorkerBusinessLogic.OfficePackage;
using FurnitureAssembly_WorkerBusinessLogic.OfficePackage.HelperModels;
using FurnitureAssembly_WorkerContracts.BindingModels;
using FurnitureAssembly_WorkerContracts.BusinessLogicContracts;
using FurnitureAssembly_WorkerContracts.SearchModels;
using FurnitureAssembly_WorkerContracts.StorageContracts;
using FurnitureAssembly_WorkerContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssembly_WorkerBusinessLogic.BusinessLogics
{
	public class ReportLogic : IReportLogic
	{
		private readonly IFurnitureModuleStorage _furnitureModuleStorage;
		private readonly ISetStorage _setStorage;
		private readonly IOrderStorage _orderStorage;
		private readonly AbstractSaveToExcel _saveToExcel;
		private readonly AbstractSaveToWord _saveToWord;
		private readonly AbstractSaveToPdf _saveToPdf;
		public ReportLogic(ISetStorage setStorage, IFurnitureModuleStorage furnitureModuleStorage, IOrderStorage orderStorage,
			AbstractSaveToExcel saveToExcel, AbstractSaveToWord saveToWord, AbstractSaveToPdf saveToPdf)
		{
			_setStorage = setStorage;
			_furnitureModuleStorage = furnitureModuleStorage;
			_orderStorage = orderStorage;
			_saveToExcel = saveToExcel;
			_saveToWord = saveToWord;
			_saveToPdf = saveToPdf;
		}
		/// <summary>
		/// Получение списка мебельных модулей с указанием, в каких гарнитурах используются
		/// </summary>
		/// <returns></returns>
		public List<ReportSetFurnitureModuleViewModel> GetSetFurnitureModule()
		{
			var furnitureModules = _furnitureModuleStorage.GetFullList();
			var sets = _setStorage.GetFullList();
			var list = new List<ReportSetFurnitureModuleViewModel>();

			foreach (var set in sets)
			{
				var record = new ReportSetFurnitureModuleViewModel
				{
					SetName = set.Name,
					FurnitureModules = new List<(string, int)>(),
					TotalCount = 0
				};
				foreach (var furnitureModule in furnitureModules)
				{
					if (set.SetFurnitureModules.ContainsKey(furnitureModule.Id))
					{
						record.FurnitureModules.Add(new(furnitureModule.Name, set.SetFurnitureModules[furnitureModule.Id].Item2));
						record.TotalCount += set.SetFurnitureModules[furnitureModule.Id].Item2;
					}
				}
				list.Add(record);
			}

			return list;
		}
		/// <summary>
		/// Получение списка заказов за определенный период
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public List<ReportOrdersViewModel> GetOrders(ReportBindingModel model)
		{
			var orders = _orderStorage
				.GetFilteredList(new OrderSearchModel { DateFrom = model.DateFrom, DateTo = model.DateTo });
			var sets = _setStorage.GetFullList();
			var furnitureModules = _furnitureModuleStorage.GetFullList();
			var list = new List<ReportOrdersViewModel>();

			foreach (var order in orders)
			{
				var record = new ReportOrdersViewModel
				{
					Id = order.Id,
					SetName = order.SetName,
					DateCreate = order.DateCreate,
					FurnitureModules = new List<(string, double)>(),
					TotalCount = 0
				};
				foreach (var set in sets)
				{
					if (set.Id == order.SetId)
					{
						foreach (var furnitureModule in furnitureModules)
						{
							if (set.SetFurnitureModules.ContainsKey(furnitureModule.Id))
							{
								record.FurnitureModules.Add(new(furnitureModule.Name, furnitureModule.Cost));
								record.TotalCount += set.SetFurnitureModules[furnitureModule.Id].Item2;
							}
						}
					}
				}
				list.Add(record);
			}

			return list;
		}
		/// <summary>
		/// Сохранение компонент в файл-Word
		/// </summary>
		/// <param name="model"></param>
		public void SaveFurnitureModuleToWordFile(ReportBindingModel model)
		{
			_saveToWord.CreateDoc(new WordInfo
			{
				FileName = model.FileName,
				Title = "Список компонент",
				Sets = _setStorage.GetFullList()
			});
		}
		/// <summary>
		/// Сохранение компонент с указаеним продуктов в файл-Excel
		/// </summary>
		/// <param name="model"></param>
		public void SaveSetFurnitureModuleToExcelFile(ReportBindingModel model)
		{
			_saveToExcel.CreateReport(new ExcelInfo
			{
				FileName = model.FileName,
				Title = "Список компонент",
				SetFurnitureModules = GetSetFurnitureModule()
			});
		}
		/// <summary>
		/// Сохранение заказов в файл-Pdf
		/// </summary>
		/// <param name="model"></param>
		public void SaveOrdersToPdfFile(ReportBindingModel model)
		{
			_saveToPdf.CreateDoc(new PdfInfo
			{
				FileName = model.FileName,
				Title = "Список заказов",
				DateFrom = model.DateFrom!.Value,
				DateTo = model.DateTo!.Value,
				Orders = GetOrders(model)
			});
		}
	}
}
