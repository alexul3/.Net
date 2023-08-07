using FurnitureAssemblyContracts.BindingModels;
using FurnitureAssemblyContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyContracts.BusinessLogicContracts
{
	public interface IReportWorkerLogic
	{
		/// <summary>
		/// Получение списка мебельных модулей с указанием, в каких гарнитурах используются
		/// </summary>
		/// <returns></returns>
		List<ReportSetFurnitureModuleWorkerViewModel> GetSetFurnitureModule(List<int> setIds);
        /// <summary>
        /// Получение списка заказов за определенный период
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Tuple<List<ReportOrdersWorkerViewModel>, double> GetOrders(ReportWorkerBindingModel model);
		/// <summary>
		/// Сохранение материалов в файл-Word
		/// </summary>
		/// <param name="model"></param>
		void SaveFurnitureModuleToWordFile(ReportWorkerBindingModel model);
		/// <summary>
		/// Сохранение материалов с указаеним мебельных модулей в файл-Excel
		/// </summary>
		/// <param name="model"></param>
		void SaveSetFurnitureModuleToExcelFile(ReportWorkerBindingModel model);
		/// <summary>
		/// Сохранение заказов в файл-Pdf
		/// </summary>
		/// <param name="model"></param>
		void SaveOrdersToPdfFile(ReportWorkerBindingModel model);
	}
}
