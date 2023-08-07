using FurnitureAssembly_WorkerContracts.BindingModels;
using FurnitureAssembly_WorkerContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssembly_WorkerContracts.BusinessLogicContracts
{
	public interface IReportLogic
	{
		/// <summary>
		/// Получение списка мебельных модулей с указанием, в каких гарнитурах используются
		/// </summary>
		/// <returns></returns>
		List<ReportSetFurnitureModuleViewModel> GetSetFurnitureModule();
		/// <summary>
		/// Получение списка заказов за определенный период
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		List<ReportOrdersViewModel> GetOrders(ReportBindingModel model);
		/// <summary>
		/// Сохранение материалов в файл-Word
		/// </summary>
		/// <param name="model"></param>
		void SaveFurnitureModuleToWordFile(ReportBindingModel model);
		/// <summary>
		/// Сохранение материалов с указаеним мебельных модулей в файл-Excel
		/// </summary>
		/// <param name="model"></param>
		void SaveSetFurnitureModuleToExcelFile(ReportBindingModel model);
		/// <summary>
		/// Сохранение заказов в файл-Pdf
		/// </summary>
		/// <param name="model"></param>
		void SaveOrdersToPdfFile(ReportBindingModel model);
	}
}
