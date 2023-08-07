using FurnitureAssemblyBusinessLogic.OfficePackage;
using FurnitureAssemblyBusinessLogic.OfficePackage.HelperModels;
using FurnitureAssemblyContracts.BindingModels;
using FurnitureAssemblyContracts.BusinessLogicContracts;
using FurnitureAssemblyContracts.SearchModels;
using FurnitureAssemblyContracts.StorageContracts;
using FurnitureAssemblyContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyBusinessLogic.BusinessLogics
{
	public class ReportWorkerLogic : IReportWorkerLogic
    {
		private readonly IFurnitureModuleStorage _furnitureModuleStorage;
		private readonly ISetStorage _setStorage;
		private readonly IOrderInfoStorage _orderInfoStorage;
		private readonly IOrderStorage _orderStorage;
		private readonly AbstractWorkerSaveToExcel _saveToExcel;
		private readonly AbstractWorkerSaveToWord _saveToWord;
		private readonly AbstractWorkerSaveToPdf _saveToPdf;
		public ReportWorkerLogic(ISetStorage setStorage, IFurnitureModuleStorage furnitureModuleStorage, IOrderInfoStorage orderInfoStorage,
			IOrderStorage orderStorage, AbstractWorkerSaveToExcel saveToExcel, AbstractWorkerSaveToWord saveToWord, AbstractWorkerSaveToPdf saveToPdf)
		{
			_setStorage = setStorage;
			_furnitureModuleStorage = furnitureModuleStorage;
			_orderInfoStorage = orderInfoStorage;
			_orderStorage = orderStorage;
			_saveToExcel = saveToExcel;
			_saveToWord = saveToWord;
			_saveToPdf = saveToPdf;
		}
		/// <summary>
		/// Получение списка мебельных модулей с указанием, в каких гарнитурах используются
		/// </summary>
		/// <returns></returns>
		public List<ReportSetFurnitureModuleWorkerViewModel> GetSetFurnitureModule(List<int> setIds)
		{
			if (setIds == null)
			{
				return new List<ReportSetFurnitureModuleWorkerViewModel>();
			}
			var furnitureModules = _furnitureModuleStorage.GetFullList();
			List<SetViewModel> sets = new List<SetViewModel>();
            foreach (var setId in setIds)
            {
				var res = _setStorage.GetElement(new SetSearchModel { Id = setId });
				if (res != null)
				{
					sets.Add(res);
				}
            }
            var list = new List<ReportSetFurnitureModuleWorkerViewModel>();

			foreach (var set in sets)
			{
				var record = new ReportSetFurnitureModuleWorkerViewModel
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
		public Tuple<List<ReportOrdersWorkerViewModel>, double> GetOrders(ReportWorkerBindingModel model)
		{
			var orderInfos = _orderInfoStorage
				.GetFilteredList(new OrderInfoSearchModel { DateFrom = model.DateFrom, DateTo = model.DateTo, UserId = model.UserId });
			List<int> orderInfoIds = new List<int>();
			foreach (var orderInfo in orderInfos)
			{
				orderInfoIds.Add(orderInfo.Id);
			}
			var orders = _orderStorage.GetFilteredList(new OrderSearchModel 
			{ 
				OrderInfoId = orderInfoIds
			});
			List<SetViewModel> sets = new List<SetViewModel>();
            foreach (var order in orders)
            {
                sets.Add(_setStorage.GetElement(new SetSearchModel
                {
                    Id = order.SetId
				}));
            }
            
			var furnitureModules = _furnitureModuleStorage.GetFullList();
			var list = new List<ReportOrdersWorkerViewModel>();

			foreach (var order in orders)
			{
                var record = new ReportOrdersWorkerViewModel
                {
                    Id = order.Id,
                    SetName = order.SetName,
                    FurnitureModules = new List<(string, double)>(),
                    TotalCount = 0
                };
                foreach (var orderInfo in orderInfos)
				{
					if (orderInfo.Id == order.OrderInfoId)
					{
						var currentSet = _setStorage.GetElement(new SetSearchModel { Id = order.SetId });
						string setName = "";
						if (order.Count > 1)
						{
							setName = order.SetName + " X" + order.Count;
						} else
						{
                            setName = order.SetName;
                        }
                        record = new ReportOrdersWorkerViewModel
                        {
                            Id = order.Id,
                            SetName = setName,
                            DateCreate = orderInfo.DateCreate,
                            FurnitureModules = new List<(string, double)>(),
                            TotalCount = order.Count,
							Sum = currentSet.Cost.ToString() + "x" + order.Count.ToString()
                        };
                    }
				}
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
			double fullSum = orderInfos.Sum(x => x.Sum);

            return Tuple.Create(list, fullSum);
		}
		/// <summary>
		/// Сохранение компонент в файл-Word
		/// </summary>
		/// <param name="model"></param>
		public void SaveFurnitureModuleToWordFile(ReportWorkerBindingModel model)
		{
			_saveToWord.CreateDoc(new WordInfo
			{
				Title = "Список компонент",
				SetsFurnitureModules = GetSetFurnitureModule(model.SetIds)
            });
		}
		/// <summary>
		/// Сохранение компонент с указаеним продуктов в файл-Excel
		/// </summary>
		/// <param name="model"></param>
		public void SaveSetFurnitureModuleToExcelFile(ReportWorkerBindingModel model)
		{
			_saveToExcel.CreateReport(new ExcelInfo
			{
				Title = "Список компонент",
				SetFurnitureModules = GetSetFurnitureModule(model.SetIds)
			});
		}
		/// <summary>
		/// Сохранение заказов в файл-Pdf
		/// </summary>
		/// <param name="model"></param>
		public void SaveOrdersToPdfFile(ReportWorkerBindingModel model)
		{
			_saveToPdf.CreateDoc(new PdfInfo
			{
				Title = "Список заказов",
				DateFrom = model.DateFrom!.Value,
				DateTo = model.DateTo!.Value,
				Orders = GetOrders(model)
			});
		}
    }
}
